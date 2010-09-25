window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function EditModel(username) {
        this.username = username;
    }

    EditModel.prototype.getMediaKeys = function () {

        var splitKeys = null;
        var keys = $('input#MediaKeys').val();

        if (keys !== null && keys.length > 0) {

            splitKeys = keys.splitCSV();
        }

        return splitKeys;
    };

    EditModel.prototype.getEditDetails = function (id, username, onGetData) {

        var jsonPath = '/' + username + '/photos/geteditdetails/' + id;
        $.post(jsonPath, null, function (data) {
            onGetData(data);
        }, 'json');

    };
    return (new EditModel());

})(jQuery, window.application));