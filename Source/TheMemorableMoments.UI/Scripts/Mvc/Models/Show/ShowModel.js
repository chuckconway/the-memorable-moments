window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function ShowModel(username) {
        this.username = username;
    }

    ShowModel.prototype.getMediaKeys = function () {

        var splitKeys = null;
        var keys = $('input#MediaKeys').val();

        if (keys !== null && keys.length > 0) {

            splitKeys = keys.splitCSV();
        }

        return splitKeys;
    };

    ShowModel.prototype.getMediaDetails = function (id, username, onGetData) {

        var jsonPath = '/' + username + '/photos/getdetails/' + id;
        $.post(jsonPath, null, function (data) {
            onGetData(data);
        }, 'json');

    };
    return (new ShowModel());

})(jQuery, window.application));