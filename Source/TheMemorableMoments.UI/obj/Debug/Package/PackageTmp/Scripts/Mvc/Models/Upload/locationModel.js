
window.application.addModel((function ($, application) {

    // I am the contacts service class.
    function locationModel(username, location) {
        this.username = (username || null);
        this.location = (location || "not set");
    };

    return (new locationModel());

})(jQuery, window.application));