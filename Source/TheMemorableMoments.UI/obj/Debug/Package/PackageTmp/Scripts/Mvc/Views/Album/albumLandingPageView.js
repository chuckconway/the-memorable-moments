window.application.addView((function ($, application) {




    // I am the contact list view class.
    function AlbumLandingPageView() {
        this.title = null; // $('<h2>').addClass('albumtitle');
        this.username = null;
        this.isAdmin = false;
        this.photoCount = null; // $('<strong>').addClass('photocount');
        this.albums = null; // $('<ul>').addClass('groupedphotos albums');
        this.adminLinks = null;

        //this.load = function (albumId) { };
    };

    AlbumLandingPageView.prototype.init = function () {

        var self = this;

        this.title = $('<h2>').addClass('grouptitle');
        this.username = null;
        this.isAdmin = false;
        this.photoCount = $('<label>').addClass('count');
        this.albums = $('<ul>').addClass('groupedphotos albums');
        this.adminLinks = null;

    };

    //Populates the elements
    AlbumLandingPageView.prototype.populate = function () {

        this.init();
        var self = this;

        application.getModel("AlbumService").getLandingPage(function (model) {

            if (model.isAdmin) {

                var adminLinkHref = '/' + username + '/albums/add';
                self.adminLinks = $('<span>').addClass('adminlinks').append("[ ")
                                                            .append($('<a>').addClass('fancyboxmodal').attr('href', adminLinkHref).text('add album'))
                                                            .append(" ]");
            }

            if (model.albums != null) {
                self.albums.html(model.albums);
            }

            self.title.text(model.title);
            self.photoCount.text(model.photoCount + " " + (model.photoCount == 1 ? "album" : "albums"));
            self.render();
        });
    };


    AlbumLandingPageView.prototype.render = function () {

        //Order matters
        var container = $('div#photos');
        container.hide();
        container.empty();
        container.append(this.title);

        if (this.adminLinks) {
            container.append(this.adminLinks);
        }

        container.append(this.photoCount);
        container.append(this.albums);
        this.attach();

        container.fadeIn(300);
    };


    AlbumLandingPageView.prototype.showView = function () {

        this.populate();
    };

    AlbumLandingPageView.prototype.hideView = function (event) {
        var container = $('div#photos');
        container.fadeOut(300);
    };

    AlbumLandingPageView.prototype.attach = function () {

        $("a.fancyboxmodal").fancybox({
            'width': 460,
            'height': 440,
            'autoScale': false,
            'transitionIn': 'none',
            'transitionOut': 'none',
            'type': 'iframe',
            overlayColor: "#000",
            overlayOpacity: 0.8
        });
        
    };

    return (new AlbumLandingPageView());

})(jQuery, window.application));