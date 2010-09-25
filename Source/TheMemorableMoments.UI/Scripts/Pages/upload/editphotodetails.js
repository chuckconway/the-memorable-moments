
jQuery(document).ready(function ($) {

    $("#Tags").autocomplete(tagSearch, {
        width: 320,
        max: 10,
        highlight: false,
        multiple: true,
        multipleSeparator: " ",
        scroll: true,
        scrollHeight: 300
    });

    // bind to the form's submit event
    $('form.imagedetails').submit(function () {
        // inside event callbacks 'this' is the DOM element so we first 
        // wrap it in a jQuery object and then invoke ajaxSubmit
        $(this).ajaxSubmit();
        saveButton.blur();

        var notifyMessage = "'" + $('input#PhotoTitle').val() + "' details have been saved.";
        $('span#message').text(notifyMessage);
        showMessage(notifyMessage);
        // !!! Important !!! 
        // always return false to prevent standard browser submit and page navigation 
        return false;
    });

    // We only want these styles applied when javascript is enabled
    $('div.content').css('display', 'block');

});