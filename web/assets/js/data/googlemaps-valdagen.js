$.getScript("https://maps.googleapis.com/maps/api/js?key=AIzaSyAeZE6vHYMK_x5l8OPVnjoKgg27YtCpUk8&callback=initMap", function(data, textStatus, jqxhr) {
// console.log(data); //data returned
// console.log(textStatus); //success
// console.log(jqxhr.status); //200
// console.log('Load was performed.');
});

function initMap() {

    var valLat = parseFloat($('#lat').html());
    var valLng = parseFloat($('#lng').html());
    var uluru = {lat: valLat, lng: valLng };

    var iconDefault = {
        url: "assets/img/map-purple.svg", // url
        scaledSize: new google.maps.Size(40, 40), // scaled size
        origin: new google.maps.Point(0,0), // origin
        anchor: new google.maps.Point(19, 40) // anchor
    };


    var map = new google.maps.Map(document.getElementById("map"));


    var iconDefault = {
        url: "assets/img/map-purple.svg", // url
        scaledSize: new google.maps.Size(40, 40), // scaled size
        origin: new google.maps.Point(0,0), // origin
        anchor: new google.maps.Point(19, 40) // anchor
    };

    var beachMarker = new google.maps.Marker({
      position: {lat: valLat, lng: valLng},
      map: map,
      optimized: false,
      icon: iconDefault
    });
    //Create and open InfoWindow.
    var infoWindow = new google.maps.InfoWindow();
    var myPlace = new google.maps.LatLng(59.32707, 18.056829);
    var Item_1 = new google.maps.LatLng(valLat, valLng);

    var bounds = new google.maps.LatLngBounds();
    bounds.extend(myPlace);
    bounds.extend(Item_1);
    map.fitBounds(bounds);

    // zoom out
    var listener = google.maps.event.addListener(map, "idle", function() { 
        if (map.getZoom() > 14) map.setZoom(14); 
        google.maps.event.removeListener(listener); 
    });

    // // // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };
            infoWindow.setPosition(pos);
            infoWindow.setContent('<b>Din position.</b>');
            map.setCenter(pos);
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
                icon: icon,
            });
            var myPlace = new google.maps.LatLng(pos.lat, pos.lng);
            var Item_1 = new google.maps.LatLng(valLat, valLng);

            var bounds = new google.maps.LatLngBounds();
            bounds.extend(myPlace);
            bounds.extend(Item_1);
            map.fitBounds(bounds);
            
        }, function() {
            handleLocationError(true, infoWindow, map.getCenter());

        });
    } else {
    // Browser doesn't support Geolocation
    handleLocationError(false, infoWindow, map.getCenter());

    }
}