$(document).ready(function () {

    showMessage(message);

    jQuery.ajaxSettings.traditional = true;

    $('a.lightbox').lightBox({
        overlayBgColor: '#000',
        overlayOpacity: 0.8,
        containerResizeSpeed: 350
    });

   

    fadeIn('div#fadeinarea');

});
