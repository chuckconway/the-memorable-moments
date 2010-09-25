
$(document).ready(function () {

    $('a.showimage').live('click', function (e) {
        var username = $(this).attr('id');
        var id = $(this).attr('name');
        location.href = '/' + username + '/photos/show/' + id;

        return false;
    });

    $('div#randomphotos a.showimage').imgPreview({
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