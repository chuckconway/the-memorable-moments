$(document).ready(function () {

    jQuery.ajaxSettings.traditional = true;


    $("table.results").tableDnD({
        onDragClass: "ondrag",
        onDrop: function (table, row) {
            var rows = table.tBodies[0].rows;

            var ids = new Array();
            for (var i = 0; i < rows.length; i++) {
                rows[i].cells[0].innerHTML = (i + 1);

                ids[i] = $('input.mediaid', rows[i]).val();
            }

            $.post('/' + username + '/albums/UpdatePhotoPosition/' + albumId, { ids: ids });
        }

    });


    $('table a.redhover').click(function () {

        var url = this.href;
        $.post(url);

        $(this).parent().parent().remove();
        return false;

    });

    $('table a.setcoverimage').click(function () {

        var url = this.href;
        $.post(url);

        var mediaId = $('table tr td span#coverimage').parent().parent().find('input.mediaid').val();
        var setCoverImageLink = '<a class=\"setcoverimage\" href=\"/' + username + '/albums/SetCoverImage/?mediaid=' + mediaId + '&albumid=' + albumId + '\" >set as cover image</a>';
        $('table tr td span#coverimage').replaceWith(setCoverImageLink);
        $(this).replaceWith('<span id=\"coverimage\" >cover image</span>');
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

    fadeIn('div#fadeinarea');



});