$(document).ready(function () {

    $('a.addfriend').click(function () {
        var tr = $(this).parents('tr');
        var name = tr.find('span.albumname').text();
        tr.remove();
        $.get(this.href);
        return false;
    });

});