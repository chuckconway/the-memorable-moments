
$(document).ready(function () {

    $('abbr.timeago').timeago();
    showMessage(message);
});

function fadeIn(name) {

    var sidebar = $(name);

    sidebar.removeClass("hidden");
    sidebar.hide();
    sidebar.fadeIn("def");

}
