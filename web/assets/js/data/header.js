//punktlista rubrik
var headerTitle = $("#logotype-template").html(); 

var headerTitleTemplate = Handlebars.compile(headerTitle);

function getheaderTitle() {
	if ($('html').hasClass('')) {

		var logotypeData = headerTitleTemplate({
		    headerTitle: "Rösta i Stockholms stad"
		});
		$('#logotype-data').html(logotypeData);

	} else if ($('html').hasClass('en'))  {
		
		var logotypeData = headerTitleTemplate({
		    headerTitle: "Vote in Stockholm city"
		});
		$('#logotype-data').html(logotypeData);
	}
}
getheaderTitle();

$('#languageDropdown').click(function() {
	getheaderTitle();
});