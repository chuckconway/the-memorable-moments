window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function uploadService(username) {
        this.username = username;
    };


    uploadService.prototype.getDetailsModel = function (username, renderView) {

        var self = this;

        var model = application.getModel("detailsModel");
        model.username = username;
        model.location = 'the moon';
        model.albums = 'camping';
        model.year = '1956';
        model.tags = '';

        renderView(model);
    };


    return (new uploadService());

})(jQuery, window.application));