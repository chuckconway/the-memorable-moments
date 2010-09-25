$(document).ready(function () {

    jQuery.ajaxSettings.traditional = true;

    $('a.lightbox').lightBox({
        overlayBgColor: '#000',
        overlayOpacity: 0.8,
        containerResizeSpeed: 350
    });

    $("#Text").autocomplete('/'+ username +'/photos/TagSearch', {
        width: 550,
        max: 10,
        highlight: false,
        multiple: true,
        multipleSeparator: " ",
        scroll: true,
        scrollHeight: 300
    });

    $('table a.add').click(function () {

        var url = this.href;
        $.post(url);

        $(this).parent().parent().fadeOut('slow').remove();

        showMessage('Photo added to album.');
        return false;

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