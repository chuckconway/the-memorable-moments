
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
