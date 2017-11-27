$('.ui-autocomplete-input').keyup(function(){
	var input =  $(this).val();
	$(".empty-message").addClass('dummy');
	$('.ui-autocomplete-input').addClass('dummy');
	if ($(input).val().length === 0) {
        $(".empty-message").hide();
    }
});


$.widget( "app.autocomplete", $.ui.autocomplete, {

	// Which class get's applied to matched text in the menu items.
	options: {
		highlightClass: "ui-state-highlight",
	},

	select: function (ui, item) {
		var itemAdress = item.adress;
        $('#searchtags').val(item.label2);
    },

	_renderItem: function( ul, item ) {
	    // Replace the matched text with a custom span. This span uses the class found in the "highlightClass" option.
	    var itemAdress = item.adress;
		var re = new RegExp( "(" + this.term + ")", "gi" ),
		cls = this.options.highlightClass,
		template = "<span class='" + cls + "'>$1</span>",

		label2 = itemAdress.replace(re, template);

		return $( "<li>" )
		.attr('id', item.id)
		.attr( "data-value", item.value )
		.append( $( "<a>" ).html( label2 ) )
		.appendTo( ul );
	},



});

$.widget("ui.customautocomplete", $.extend({}, $.ui.autocomplete.prototype, {

  _response: function(contents){
      $.ui.autocomplete.prototype._response.apply(this, arguments);
      $(this.element).trigger("autocompletesearchcomplete", [contents]);
  }
}));

$( function() {
	$('#searchtags').focus(function() {
	    $(this).autocomplete({
	        minLength: 3,
			highlightClass: "searchHighlight",
			// source: adresser,			
			source: function (request, response) {
				
				 // request.term is the term searched for.
				 // response is the callback function you must call to update the autocomplete's 
				 // suggestion list.
				 $.ajax({
					 url: "https://api-url-here.azurewebsites.net/api/sok",
					 data: { q: request.term },
					 dataType: "json",
					 success: response,
					 error: function () {
						 response([]);
					 }
				 });
			 }
			,
			messages: {
				noResults: function(count) {
					$(".empty-message").show();
				},
				results: function(count) {
					$(".empty-message").hide();
				}
			},
			select: function( event , ui ) {
				
				$.ajax({
				  url: "https://api-url-here.azurewebsites.net/api/vallokal/valdagen/" + ui.item.lokalId,
				  dataType: 'json',
				  success: function (vallokal) {
					var vallokalInfo = $('#vallokal-template').html();
					var templateVallokal = Handlebars.compile(vallokalInfo);
					
					function getVallokal() {
						if ($('html').hasClass('')) {

							var vallokalData = templateVallokal({

								vallokalTitle: "Din vallokal",
								vallokal: vallokal.namn,
								valadress: vallokal.adress,
                                valadressGeo: vallokal.adress_geo,
								valort: vallokal.postOrt,
								vallokalGoogleMaps: "Hitta hit",

								valdistriktTitle: "Ditt valdistrikt",
								valdistriktsnamn: vallokal.distrikt,

								vallokalstiderTitle: "Vallokalen är öppen",
								valdatum: "Söndagen 9 september",
								valtid: "08:00-20:00",

								valLat: vallokal.lat,
								valLng: vallokal.lng


							});
							$('#vallokal-data').html(vallokalData);
							// Link to google maps
							$("#vallokal-data #latlng").attr("href", 'https://maps.google.com/?daddr=' + vallokal.adress + ',' + vallokal.postOrt + ',' + 'Sverige' + '/' + vallokal.lat + ',' + vallokal.lng + ',15z');

						} else if ($('html').hasClass('en'))  {

							var vallokalData = templateVallokal({

								vallokalTitle: "Your polling station",
								vallokal: vallokal.namn,
								valadress: vallokal.adress,
								valadressGeo: vallokal.adress_geo,
								valort: vallokal.postOrt,
								vallokalGoogleMaps: "Find us",

								valdistriktTitle: "Your electoral district is:",
								valdistriktsnamn: vallokal.distrikt,

								vallokalstiderTitle: "The polling station is open",
								valdatum: "Sunday 9 September",
								valtid: "08:00-20:00",

								valLat: vallokal.lat,
								valLng: vallokal.lng

							});
						
							// Link to google maps
							$("#vallokal-data #latlng").attr("href", 'https://maps.google.com/?daddr=' + vallokal.adress + ',' + vallokal.postOrt + ',' + 'Sverige' + '/' + vallokal.lat + ',' + vallokal.lng + ',15z');
						}

						$('#vallokal-data').html(vallokalData);

						setTimeout(
							function() {
							// scroll down to results
							$('html,body').animate({
						        scrollTop: $("#results").offset().top - 10},
						        'slow');
							}, 200);
					}
					getVallokal();

					$('#languageDropdown').click(function() {
						getVallokal();
					});
					
					// Choosed adress
					var selected = ui.item.adress; //+ ' ' + ui.item.postOrt;
					$('#searchtags').val(selected).blur();
				  }
				});
			}

		});
	}).keyup(function() {
		if($(this).val() === "") {
			$('.empty-message').hide();
			$('.search-input .block-icon').removeClass('icon-close').addClass('icon-search');
		} else {
			$('.search-input .block-icon').removeClass('icon-search').addClass('icon-close');
		}

		$('.icon-close').click(function() {
			$('.search-input input').focus();
			$('.search-input .block-icon').removeClass('icon-close').addClass('icon-search');
			$('.search-input input').val('');
			$(".empty-message").hide();
		});
		
	})
	
});