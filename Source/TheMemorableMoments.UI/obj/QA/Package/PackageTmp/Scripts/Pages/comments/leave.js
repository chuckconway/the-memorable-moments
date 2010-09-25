var name;
var email;
var website;

(function ($) {
    $.fn.clearDefault = function () {
        return this.each(function () {
            var default_value = $(this).val();
            $(this).focus(function () {
                if ($(this).val() == default_value) $(this).val("");
            });
            $(this).blur(function () {
                if ($(this).val() == "") $(this).val(default_value);
            });
        });
    };
})(jQuery);

$(document).ready(function () {
    $('input.commentinput').clearDefault();
    setDefaultValues();
    $('input#CommentButton').click(clearDefaultValues);
    fadeIn('div.commentimage');
    fadeIn('div.comment');
    fadeIn('div.postedcomment.editphoto');


    showMessage(message);

});

function fadeIn(name) {

    var sidebar = $(name);

    sidebar.removeClass("hidden");
    sidebar.hide();
    sidebar.fadeIn("def");

}

function setDefaultValues() {

    name = $('input#Name').val();
    email = $('input#Email').val();
    website = $('input#Website').val();

}

function clearValue(controlName, defaultValue) {

    if (defaultValue == $(controlName).val()) {

        $(controlName).val('');
    }

}

function clearDefaultValues() {

    clearValue('input#Name', name);
    clearValue('input#Email', email);
    clearValue('input#Website', website);
}