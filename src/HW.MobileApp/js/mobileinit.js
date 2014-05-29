// configure jquery mobile
$(document).on("mobileinit", function () {
    // turn off transition
    $.mobile.defaultPageTransition = 'none';
    $("body .button_right").buttonMarkup({ iconpos: "right" });
    $('a').buttonMarkup({ corners: false , theme: "b"});
    $.mobile.changePage.defaults.allowSamePageTransitions = true;
    $.mobile.page.prototype.options.domCache = true;
});