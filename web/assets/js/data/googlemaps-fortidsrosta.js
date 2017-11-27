var markers = [];

window.onload = function () {

    if ($('html').hasClass('en')) {
        $.ajax({
            url: "https://api-url-here.azurewebsites.net/api/vallokal/fortid",
            data: { lang: 'en' },
            dataType: "json",
            success: function (response) {
                markers = response;
                LoadMap();
            },
            error: function () {

            }
        }); 
    }
    else
    {
        $.ajax({
            url: "https://api-url-here.azurewebsites.net/api/vallokal/fortid",
            data: { lang: 'sv' },
            dataType: "json",
            success: function (response) {
                markers = response;
                LoadMap();
            },
            error: function () {

            }
        });
    }
}

function LoadMap() {
    var map = new google.maps.Map(document.getElementById("map"), mapOptions);
    var mapOptions = {
        center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    //Create and open InfoWindow.
    var infoWindow = new google.maps.InfoWindow();

    for (var i = 0; i < markers.length; i++) {
        var data = markers[i];
        var myLatlng = new google.maps.LatLng(data.lat, data.lng);
        var iconDefault = {
            url: "assets/img/map-purple.svg", // url
            scaledSize: new google.maps.Size(40, 40), // scaled size
            origin: new google.maps.Point(0,0), // origin
            anchor: new google.maps.Point(19, 40) // anchor
        };
        var iconSelected = {
            url: "assets/img/map-pink.svg", // url
            scaledSize: new google.maps.Size(40, 40), // scaled size
            origin: new google.maps.Point(0,0), // origin
            anchor: new google.maps.Point(19, 40) // anchor
        };
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            optimized: false,
            title: data.title,
            title: data.namn,
            icon: iconDefault,
            zIndex: 999999
        });


        var myPlace = new google.maps.LatLng(59.32707, 18.056829);
        var Item_1 = new google.maps.LatLng(59.32707, 18.056829);

        var bounds = new google.maps.LatLngBounds();
        bounds.extend(myPlace);
        bounds.extend(Item_1);
        
        //set zoom
        google.maps.event.addListener(map, 'zoom_changed', function() {
            zoomChangeBoundsListener = 
                google.maps.event.addListener(map, 'bounds_changed', function(event) {
                    if (this.getZoom() > 14 && this.initialZoom == true) {
                        // Change max/min zoom here
                        this.setZoom(14);
                        this.initialZoom = false;
                    }
                google.maps.event.removeListener(zoomChangeBoundsListener);
            });
        });
        map.initialZoom = true;
        map.fitBounds(bounds);

        var activeMarker;


        //Attach click event to the marker.
        (function (marker, data) {
            google.maps.event.addListener(marker, "click", function (e) {
                //Wrap the content inside an HTML DIV in order to set height and width of InfoWindow.
                //infoWindow.setContent(data.title);
                //infoWindow.open(map, marker);


                // check to see if activeMarker is set
                // if so, set the icon back to the default
                activeMarker && activeMarker.setIcon(iconDefault);

                // set the icon for the clicked marker
                marker.setIcon(iconSelected);

                // update the value of activeMarker
                activeMarker = marker;


                // Visa mer #mapinfo
                $("#mapinfo").show();

                // Visa data-info
                $("#mapinfo .vallokal").html(data.namn);
                $("#mapinfo .valadress").html(data.adress);
                $("#mapinfo .valort").html(data.postOrt);
				$("#mapinfo .vallokalstiderIdag").empty();

                // Link to google maps
                $("#mapinfo #latlng").attr("href", 'https://maps.google.com/?daddr=' + data.adress_geo + ',' + data.postOrt + ',' + 'Sverige' + '/' + data.lat + ',' + data.lng + ',14z');

                $("#mapinfo .valoppettider").html(data.tooltipTitle);

                var tider = [];

                //Set close text as default
                if ($('html').hasClass('en')) {
                    $('.vallokalstiderIdag').prev().html('Closed today');
                }
                else {
                    $('.vallokalstiderIdag').prev().html('St&auml;ngt idag');
                }

                //If we have an open polling station today set the text to open and show the open hours
                for (var i = 0; i < data.oppettider.length; i++) {
                    
                    tider.push('<li>' + '<span>' + data.oppettider[i].datum + '</span><span>' + data.oppettider[i].tid + '</span></li>');

                    $(".oppet-list").html('<ul>' + tider.join('') + '</ul>');

                    if (data.oppettider[i].idag) {
                        $("#mapinfo .vallokalstiderIdag").html(data.oppettider[i].tid);
                        if ($('html').hasClass('en')) {
                            $('.vallokalstiderIdag').prev().html('Open today,');
                        }
                        else {
                            $('.vallokalstiderIdag').prev().html('&Ouml;ppet idag,');
                        }
						$("#mapinfo .vallokalstiderIdag").show();
					}
                }

                // close .oppet-list
                $('.oppet-list').hide();
                $('.toggle-list a').removeClass('open').blur();

                // Scroll down to results
                var wHeight = $(window).height(),
                divTop = $('#results').offset().top,
                divHeight = $('#results').outerHeight(),
                divBot = divTop + divHeight;

                $('html, body').animate({
                    scrollTop: (divBot - wHeight)
                }, 500);

                   
            });
        })(marker, data);
    }

    // // // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            infoWindow.setPosition(pos);
            infoWindow.setContent('<b>Din position.</b>');
            // map.setCenter(pos);
            //map.setZoom(10);
            var icon = {
                url: "assets/img/navigation.svg", // url
                scaledSize: new google.maps.Size(40, 40), // scaled size
                origin: new google.maps.Point(0,0), // origin
                anchor: new google.maps.Point(19, 40) // anchor
            };
            var marker = new google.maps.Marker({
                position: pos,
                map: map,
                optimized: false,
                title: String(pos.lat) + ", " + String(pos.lng),
                icon: icon
            });

            var lat1 = pos.lat;
            var lng1 = pos.lng;

            // Get closest marker
            function rad(x) {return x*Math.PI/180;}

            var lat = lat1;
            var lng = lng1;
            var R = 6371; // radius of earth in km
            var distances = [];
            var closest = -1;
            for( i=0;i< markers.length; i++ ) {
                var mlat = markers[i].lat;
                var mlng = markers[i].lng;
                var dLat  = rad(mlat - lat);
                var dLong = rad(mlng - lng);
                var a = Math.sin(dLat/2) * Math.sin(dLat/2) +
                    Math.cos(rad(lat)) * Math.cos(rad(lat)) * Math.sin(dLong/2) * Math.sin(dLong/2);
                var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1-a));
                var d = R * c;
                distances[i] = d;
                if ( closest == -1 || d < distances[closest] ) {
                    closest = i;
                }
            }

            //alert(markers[closest].lat + ', ' + lng);

            var myPlace = new google.maps.LatLng(pos.lat, pos.lng);
            var closestMarker = new google.maps.LatLng(markers[closest].lat, markers[closest].lng);
            

            var bounds = new google.maps.LatLngBounds();
           
            bounds.extend(myPlace);

            bounds.extend(closestMarker);

            // // Set Zoom of fitBounds, 
            // google.maps.event.addListener(map, 'zoom_changed', function() {
            //     zoomChangeBoundsListener = 
            //         google.maps.event.addListener(map, 'bounds_changed', function(event) {
            //             if (this.getZoom() > 14 && this.initialZoom == true) {
            //                 // Change max/min zoom here
            //                 this.setZoom(14);
            //                 this.initialZoom = false;
            //             }
            //         google.maps.event.removeListener(zoomChangeBoundsListener);
            //     });
            // });
            // map.initialZoom = true;
            // Set Zoom of fitBounds Ends here //

            
            // zoom out
            var listener = google.maps.event.addListener(map, "idle", function() { 
                if (map.getZoom() > 14) map.setZoom(14); 
                google.maps.event.removeListener(listener); 
            });

            map.fitBounds(bounds);

            google.maps.event.addListenerOnce(map, 'zoom_changed', function() {
                var oldZoom = map.getZoom();
                map.setZoom(oldZoom - 1); //Or whatever
            });
     
            
        }, function() {
            handleLocationError(true, infoWindow, map.getCenter());

        });
    } else {
        // Browser doesn't support Geolocation
        handleLocationError(false, infoWindow, map.getCenter());
    }

}