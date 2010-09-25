$(window).bind("load", function () {
    $('a.tag').tagCloud();
    $('a.year').tagCloud();
    //showMessage();
});

$(document).ready(function () {

    $("ul#albums").treeview({
        url: "/" + username + "/albums/nodes"
    });

    $("a.fanceyboxmodal").fancybox({
        'width': 460,
        'height': 440,
        'autoScale': false,
        'transitionIn': 'none',
        'transitionOut': 'none',
        'type': 'iframe',
        'overlayOpacity': 0.75,
        'overlayColor': '#000'
    });

});

function filterTags(element, anchor, spanWrapper) {

    var val = $(element).val();

    $(spanWrapper).each(function () {
        var tagWrapper = $(this);
        var tag = tagWrapper.find(anchor);
        var tagText = tag.text();
        if (!tagText.startsWith(val)) {
            tagWrapper.hide();
        }
        else {
            tagWrapper.show();
        }

    });
}