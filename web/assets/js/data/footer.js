//Footer
var punktlista = $("#punktlista").html(); 
var punktlistaTemplate = Handlebars.compile(punktlista); 

function getPunktlista() {
    if ($('html').hasClass('')) {

        var punktlistaData = punktlistaTemplate({
            title: "Bra att veta",
            punktlista: [ 
                {
                    punkt: "9 september är det val till riksdag, kommun- och landstingsfullmäktige" 
                },
                {
                    punkt: "Du behöver ta med giltigt ID" 
                },
                {
                    punkt: "Du behöver ditt röstkort när du ska förtidsrösta" 
                },
                {
                    punkt: "För mer information om val, se <a target='_blank' href='http://www.val.se/'>val.se</a>" 
                },
                {
                    punkt: "Kontakta valnämnden i Stockholms stad<br>Telefonnummer: <a href='tel:+46850829500'>08-50829500</a><br>E-postadress: <a href='mailto:info.rostaistockholm@stockholm.se'>info.rostaistockholm@stockholm.se</a><br>"
                }
            ]
        });
        $('#secondary').html(punktlistaData);

    } else if ($('html').hasClass('en'))  {
                
        var punktlistaData = punktlistaTemplate({
            title: "Useful information",
            punktlista: [ 
                {
                    punkt: "9th of  September is election day for the Swedish Parliament, municipal and county councils" 
                },
                {
                    punkt: "You need to bring a valid ID to vote" 
                },
                {
                    punkt: "For advance voting you need to bring your voting card" 
                },
                {
                    punkt: "For more information, please visit <a target='_blank' href='http://www.val.se/'>val.se</a>"
                },
                {
                    punkt: "Contact: Valnämnden i Stockholms stad<br>Phone: <a href='tel:+46850829500'>08-50829500</a><br>E-mail: <a href='mailto:info.rostaistockholm@stockholm.se'>info.rostaistockholm@stockholm.se</a><br>"
                }
            ]
        });
        $('#secondary').html(punktlistaData);
    }
}
getPunktlista();

$('#languageDropdown').click(function() {
    getPunktlista();
});