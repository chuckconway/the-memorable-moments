window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function ViewModel(username) {
        this.username = username;
    }

    ViewModel.prototype.getMediaKeys = function () {

        var splitKeys = null;
        var keys = $('input#MediaKeys').val();

        if (keys !== null && keys.length > 0) {

            splitKeys = keys.splitCSV();
        }

        return splitKeys;
    };

    ViewModel.prototype.getPersistentKey = function () {

        var keys = $('input#PersistentKey').val();
        return keys;
    };

    ViewModel.prototype.getPhotoCount = function () {

        var keys = $('input#PhotoCount').val();
        return keys;
    };

    ViewModel.prototype.getDetails = function (batchId, username, onGetData) {

        var jsonPath = '/' + username + '/upload/getdetails/' + batchId;
        $.post(jsonPath, null, function (data) {
            onGetData(data);
        }, 'json');

    };
    return (new ViewModel());

})(jQuery, window.application));