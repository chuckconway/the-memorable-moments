window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function AlbumService(username) {
        this.username = username;
    };


    AlbumService.prototype.getLandingPage = function (onSuccess) {

        //The relative url on the server.
        var jsonPath = '/' + username + '/albums/getlandingpage/';

        //Title of the view. It's hardcoded for the landing page. Other albums will have values from the database.
        var title = "Albums";
        $.getJSON(jsonPath, function (data) {

            //count of albums returned.
            var photoCount = data.count; // +" " + (data.count == 1 ? "photo" : "photos");
            var model = application.getModel("AlbumLandingPage", [title, photoCount, data.albums, data.isAdmin, username, data.adminlinks]);
            onSuccess(model);

        });
    };

    AlbumService.prototype.getAlbumView = function (id, onSuccess) {

        var self = this;
        var jsonPath = '/' + username + '/albums/getdetails/' + id;

        $.getJSON(jsonPath, function (data) {

            var photoCount = data.count + " " + (data.count == 1 ? "photo" : "photos");
            var model = application.getModel("AlbumView", [data.title, data.description, photoCount, data.photos, data.isAdmin, username, data.adminlinks]);
            onSuccess(model);

        });
    };

    return (new AlbumService());

})(jQuery, window.application));