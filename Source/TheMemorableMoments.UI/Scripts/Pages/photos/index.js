$(document).ready(function () {

    showMessage(message);

    $("#tags").autosearch('/' + username + '/photos/TagSearch', {
        width: 550,
        max: 10,
        highlight: false,
        multiple: true,
        multipleSeparator: " ",
        scroll: true,
        scrollHeight: 300
    });

    $('a.showimage').imgPreview({
        containerID: 'imgPreviewWithStyles',
        imgCSS: {
            // Limit preview size:
            height: 300
        },
        // When container is shown:
        onShow: function (link) {
            var title = $(link).attr('title');
            if (title) {
                $('<span>' + title + '</span>').appendTo(this);
            }
        },
        // When container hides: 
        onHide: function (link) {
            $('span', this).remove();
        }
    });

    $('a.removetag').click(function () {

        var url = this.href;
        $.post(url);

        var parentElement = $($(this).parent()); //$('a.removetag').closest('span').css('background-color', 'red');
        parentElement.remove();
        return false;
    });

    $("a.edit").fancybox({
        'width': 900,
        'height': 640,
        'autoScale': false,
        'transitionIn': 'fade',
        'transitionOut': 'fade',
        'speedIn': 400,
        'type': 'iframe',
        'overlayOpacity': 0.75,
        'overlayColor': '#000'
    });

    $("a.delete").click(function () {

        var link = $(this);
        $('<p>Are you sure you wish to <strong>delete</strong> this image? Once deleted, it can <strong>not</strong> be <strong>recovered</strong>. All associated data (comments, tags...etc) will be <strong>lost</strong>.</p>')
        .dialog({
            width: 460,
            modal: true,
            title: "Delete Confirmation",
            buttons: { "Cancel": function () { $(this).dialog("close"); }, "Yes, delete it!": function () {

                var id = link.attr("name");
                $.post("/" + username + "/photos/delete/" + id, function (data) { });
                $(this).dialog("close");
                link.closest('tr').fadeOut();
            }
            }
        });

        return false;
    });

    fadeIn('div#photoarea');

    $("input#all").click(function () {
        var checked_status = this.checked;
        $("table tr td input[name=mediaid]").each(function () {
            this.checked = checked_status;
        });
    });

});

function fadeIn(name) {

    var sidebar = $(name);

    sidebar.removeClass("hidden");
    sidebar.hide();
    sidebar.fadeIn("slow");
}