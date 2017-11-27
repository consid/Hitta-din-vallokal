// Ändra Språk till Svenska
$('.sv').click(function() {
	$('.langText').html('Sv');
	var lang = $( 'html:lang(sv)' ).removeClass( 'en' );
	localStorage.setItem('language', '');
});

// Ändra Språk till Engelska
$('.en').click(function() {
	$('.langText').html('En');
	var lang = $( 'html:lang(sv)' ).addClass( 'en' );
	localStorage.setItem('language', 'en');
});

// Hämta valt språk
$('html').addClass(localStorage.getItem('language'));

// Väljer ett språk
function getLang() {
	if ($( 'html:lang(sv)' ).hasClass("en")) {
		$('#languageDropdown li.sv').removeClass('active');
		$('#languageDropdown li.en').addClass('active');
		$('.langText').html('En');
	} else {
		$('#languageDropdown li.sv').addClass('active');
		$('#languageDropdown li.en').removeClass('active');
		$('.langText').html('Sv');
	}
}
getLang();

// Gör valt språk aktivt
$('.dropdown-menu li').click(function(){
	$('.dropdown-menu li').removeClass('active');
	$(this).addClass('active');
	$('#dropdownMenuLang').click();

	getLang();
});