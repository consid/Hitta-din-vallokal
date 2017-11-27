//föregående knapp
var button = $("#button-template").html(); 

var buttonTemplate = Handlebars.compile(button);

function getButton() {
	if ($('html').hasClass('')) {

		var buttonData = buttonTemplate({
		    button: "Föregående"
		});
		$('#button-data').html(buttonData);

	} else if ($('html').hasClass('en'))  {
		
		var buttonData = buttonTemplate({
		    button: "Previous"
		});
		$('#button-data').html(buttonData);
	}
}
getButton();

$('#languageDropdown').click(function() {
	getButton();
});