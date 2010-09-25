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

    $('input#tagfilter').keyup(function () {
        filterTags(this, 'a.tag', 'span.tagWrapper');
    });

    $('input#yearfilter').keyup(function () {
        filterTags(this, 'a.year', 'span.yearWrapper');
    });

    $('a.lightbox').lightBox({
        overlayBgColor: '#000',
        overlayOpacity: 0.8,
        imageLoading: '/content/images/lightbox/lightbox-ico-loading.gif',
        imageBtnClose: '/content/images/lightbox/lightbox-btn-close.gif',
        imageBtnPrev: '/content/images/lightbox/lightbox-btn-prev.gif',
        imageBtnNext: '/content/images/lightbox/lightbox-btn-next.gif',
        containerResizeSpeed: 350
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

    $('abbr.timeago').timeago();
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

