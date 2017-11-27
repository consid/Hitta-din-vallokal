// Valdagen
var valdagenInfo = $('#valdagen-template').html();

var templateValdagen = Handlebars.compile(valdagenInfo);

function getValdagen() {
	if ($('html').hasClass('')) {

		var valdagenData = templateValdagen({
			title: "Hitta din vallokal",
			info: "Den här tjänsten fungerar endast för dig som är folkbokförd i Stockholms stad.",
			ingress: "Ange din folkbokföringsadress och se var du ska rösta på valdagen.",
			folkbokforingsadressinfo: "Med folkbokföringsadress menas den adress där du är skriven. Det vill säga den adress som Skatteverket har registrerat som din bostadsadress. Oftast är alltså adressen där du bor också din folkbokföringsadress.",
			label: "Ange din folkbokföringsadress",
			placeholder: "T.ex. Sveavägen 10",
			searchTitle: "Sökningen gav inget resultat.",
			searchText: "Hittar du inte din vallokal, var god kontakta oss så hjälper vi dig!",
			telefonnummer: "08-50829500",
			telefonnummerLink: "+46850829500",
			infoMail: "info.rostaistockholm@stockholm.se",
			oppettider: [ 
                {
                    oppettid: "Mån-Fre: 10.00-19.00" 
                },
                {
                    oppettid: "Lördag: 10.00-15.00" 
                },
                {
                    oppettid: "Söndag: Stängt" 
                }
            ],
			titleOppet: "Öppet",
			tooltipTitle: "Fler öppettider",
			autocompleteHelp: "Skriv och välj sedan från listan nedan med hjälp av piltangenterna"
		});
		$('#valdagen-data').html(valdagenData);

	} else if ($('html').hasClass('en'))  {

		var valdagenData = templateValdagen({
			title: "Find your polling station",
			info: "This service is only available if your residential address is located within the City of Stockholm.",
			ingress: "Enter your residential address to find out where to vote on the election day.",
			folkbokforingsadressinfo: "The residential address is the address where you are registered, i.e. your place of residence that’s registered at Skatteverket (the National Tax Agency). Your place of residence is usually your registered residential address.",
			label: "Enter your residential address",
			placeholder: "E.g. Sveavägen 10",
			searchTitle: "Your search yielded no results.",
			searchText: "If you do not find your polling station, please contact us for help.",
			telefonnummer: "(+46) 08-50829500",
			telefonnummerLink: "+46850829500",
			infoMail: "info.rostaistockholm@stockholm.se",
			oppettider: [ 
                {
                    oppettid: "Mon-Fri: 10.00-19.00" 
                },
                {
                    oppettid: "Saturday: 10.00-15.00"
                },
                {
                    oppettid: "Sunday: Closed" 
                }
            ],
			titleOppet: "Open",
			tooltipTitle: "Opening hours",
			autocompleteHelp: "Type and select from the list below using the arrow keys"
		});
		$('#valdagen-data').html(valdagenData);
	}
}
getValdagen();

$('#languageDropdown').click(function() {
	getValdagen();
});
