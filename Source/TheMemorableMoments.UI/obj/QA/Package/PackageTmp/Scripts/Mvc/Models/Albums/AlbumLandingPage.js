// I represent a contact object.

// Add model to the application.
window.application.addModel((function ($, application) {

    // I am the contact class.
    function AlbumLandingPage(title, photoCount, albums, isAdmin, username, adminLinks) {
        this.title = (title || "");
        this.photoCount = (photoCount || 0);
        this.albums = (albums || "");
        this.isAdmin = isAdmin;
        this.username = (username || null);
        this.adminLinks = (adminLinks || null);
    };

    // Return a new model class.
    return (AlbumLandingPage);

})(jQuery, window.application));