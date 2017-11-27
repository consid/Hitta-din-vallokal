// Index - Startpage
var startpage = $('#startpage-template').html();

var startpageTemplate = Handlebars.compile(startpage);

function getStartpage() {

	if ($('html').hasClass('')) {

		var startpageData = startpageTemplate({
			title: "Hur vill du rösta vid allmänna valen 9 september?",
			ingress: "Hur vill du rösta?",
			link1: "Jag vill rösta i min vallokal på valdagen",
			link2: "Jag vill förtidsrösta innan valdagen"
		});
		$('#startpage-data').html(startpageData);

	} else if ($('html').hasClass('en')) {

		var startpageData = startpageTemplate({
			title: "How to vote in the parliamentary elections on 9th of September?",
			ingress: "How do you want to vote?",
			link1: "Find my polling station",
			link2: "Vote before election day"
		});
		$('#startpage-data').html(startpageData);
	}
}
getStartpage();

$('#languageDropdown').click(function() {
	getStartpage();
});