$(document).ready(function () {

    $('ul.groupedphotos a').lightBox({
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

});