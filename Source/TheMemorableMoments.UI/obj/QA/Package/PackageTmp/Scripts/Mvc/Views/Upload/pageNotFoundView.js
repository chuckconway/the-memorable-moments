window.application.addView((function ($, application) {

    // I am the contact list view class.
    function pageNotFoundView() {

        this.Error = null;
    };

    pageNotFoundView.prototype.init = function () {
        var self = this;

        this.error = $('<h1>')
        .append("Oops! Something has gone a rye. Don't fret! Our people are on it.")
        .append("<br />")
        .append("Until we get it resolved, please try another selection.")
        .addClass('error');
        
    };

    pageNotFoundView.prototype.showView = function () {

        var container = $('div#mvccontainer');
        container.html('');
        container.append(this.error);
    };

    return (new pageNotFoundView());

})(jQuery, window.application));