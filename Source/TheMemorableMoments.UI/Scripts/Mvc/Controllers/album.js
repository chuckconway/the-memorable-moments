window.application.addController((function ($, application) {

    // I am the controller class.
    function Controller() {
        // Route URL events to the controller's event handlers.
        this.route("/", this.index);
        this.route("/show/:id", this.showAlbum);
        this.route("/404", this.pageNotFound);

        // Set default properties.
        this.landingPageView = null;
        this.albumView = null;
        this.pageNotFound = null;
    };

    // Extend the core application controller (REQUIRED).
    Controller.prototype = new application.Controller();

    Controller.prototype.init = function () {
        this.landingPageView = application.getView("AlbumLandingPageView");
        this.albumView = application.getView("AlbumView");
        this.pageNotFound = application.getView("PageNotFoundView");
    };

    //Show the album when a hash is present.
    Controller.prototype.showAlbum = function (event) {

        this.showView(this.albumView, event);
    };

    //Show the landing page. No hash is present.
    Controller.prototype.index = function (event) {

        this.showView(this.landingPageView, event);
    };

    //Show the landing page. No hash is present.
    Controller.prototype.pageNotFound = function (event) {

        this.showView(this.pageNotFound, event);
    };

    // I show the given view; but first, I hide any existing view.
    Controller.prototype.showView = function (view, event) {

        // Check to see if there is a current view. If so, then hide it.
        if (this.currentView && this.currentView.hideView) {
            this.currentView.hideView();
        }

        // Show the given view.
        view.showView(event.parameters);

        // Store the given view as the current view.
        this.currentView = view;
    };

    return (new Controller());

})(jQuery, window.application));