
window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function detailsModel(username, location, albums, year, month, day, tags, visibility) {
        this.username = (username || null);
        this.location = (location || "not set");
        this.albums = (albums || null);
        this.year = (year || "");
        this.month = (month || "");
        this.day = (day || "");
        this.tags = (tags || "");
        this.visibility = (visibility || "");
    };

    return (new detailsModel());

})(jQuery, window.application));