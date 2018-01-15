// Förtidsrösta
var fortidsrostaInfo = $('#fortidsrosta-template').html();


var fortidsrostaTemplate = Handlebars.compile(fortidsrostaInfo);

function getFortidsrostaInfo() {

	if ($('html').hasClass('')) {

		var fortidsrostaData = fortidsrostaTemplate({
			title: "Här kan du förtidsrösta",
			ingress: "Hitta en närliggande lokal där du kan förtidsrösta",
			label: "Visa närliggande lokaler för förtidsröstning",
			autocompleteHelp: "Skriv och välj sedan från listan nedan med hjälp av piltangenterna",
			fortidsrostaInfo: 'Du kan förtidsrösta i din egen eller i en annan kommun, men bara i din vallokal på valdagen.',

			vallokalTitle: "Din vallokal",
			vallokalGoogleMaps: "Hitta hit",
			vallokalstiderTitle: "Öppettider",
			vallokalstiderFlerTider: "Fler öppettider",
			valoppettext: "Stängt idag"
		});
		$('#fortidsrosta-data').html(fortidsrostaData);

	} else if ($('html').hasClass('en'))  {
			
		var fortidsrostaData = fortidsrostaTemplate({
			title: "Voting in advance",
			ingress: "Find your nearest polling station for voting in advance",
			label: "Show local polling stations near me for voting in advance",
			autocompleteHelp: "Type and select from the list below using the arrow keys",
			fortidsrostaInfo: 'When voting in advance, you can vote in any municipality, but only in your local polling station on the election day.',

			vallokalTitle: "Your polling place",
			vallokalGoogleMaps: "Find place",
			vallokalstiderTitle: "Opening hours for this polling booth",
			vallokalstiderFlerTider: "More opening hours",
			valoppettext: "Closed today"
		});
		$('#fortidsrosta-data').html(fortidsrostaData);

	}

}
getFortidsrostaInfo();


$('#languageDropdown').click(function() {
	getFortidsrostaInfo();
});