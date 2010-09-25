String.prototype.startsWith = function (prefix) {
    return (this.substr(0, prefix.length) === prefix);
}

$(window).bind("load", function () {
    $('a.tag').tagCloud();
    $('a.year').tagCloud();
    showMessage();
});

function fadeIn(name) {

    var sidebar = $(name);

    sidebar.removeClass("hidden");
    sidebar.hide();
    sidebar.fadeIn("fast");
}

$(document).ready(function () {


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

    $('abbr.timeago').timeago();
});
