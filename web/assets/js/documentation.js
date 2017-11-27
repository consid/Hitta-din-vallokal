function closePopover() {
    $('.popover').popover('hide');
};
$('body').on('touchend click', '[data-toggle=popover]', function () {
    $(this).popover('toggle');
    return false;
});
$('body').on('touchend click', function (e) {
    $('[data-toggle=popover]').each(function () {
        // hide any open popovers when the anywhere else in the body is clicked
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            $(this).popover('hide');
        }
    });
});

$('body').on('touchend click', '[data-toggle=popover]', function () {

	$this = $(this);
	setTimeout(function() {
		if ( $('.popover-content').visible() === false ) {
			$('[data-toggle=popover]').attr('data-placement', 'top');
		} else {
			$this.attr('data-placement', 'bottom');
		}
	}, 50);

    $this = $(this);
    setTimeout(function () {
        if ($('.popover-content').visible() === false) {
            $('[data-toggle=popover]').attr('data-placement', 'top');
        } else {
            $this.attr('data-placement', 'bottom');
        }
    }, 50);

});