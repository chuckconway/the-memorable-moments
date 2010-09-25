
// Add model to the application.
window.application.addModel((function ($, application) {

    // I am the contact class.
    function AlbumView(title, description, photoCount, photos, isAdmin, username, adminLinks) {
        this.title = (title || "");
        this.description = (description || "");
        this.photoCount = (photoCount || 0);
        this.photos = (photos || "");
        this.isAdmin = isAdmin;
        this.username = (username || null);
        this.adminLinks = (adminLinks || null);
    };

    return (AlbumView);

})(jQuery, window.application));