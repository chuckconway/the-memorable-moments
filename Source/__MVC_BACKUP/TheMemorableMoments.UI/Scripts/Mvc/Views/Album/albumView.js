window.application.addView((function ($, application) {

    // I am the contact list view class.
    function AlbumView() {
        this.title = null;
        this.adminLinks = null;
        this.description = null;
        this.photoCount = null;
        this.photos = null;

        //this.load = function (albumId) { };
    };

    AlbumView.prototype.init = function () {
        var self = this;

        this.title = $('<h2>').addClass('grouptitle');
        this.adminLinks = $('<span>').addClass('adminlinks');
        this.description = $('<p>').addClass('albumdescription');
        this.photoCount = $('<label>').addClass('count');
        this.photos = $('<ul>').addClass('groupedphotos');
    };

    AlbumView.prototype.populate = function (id) {

        this.init();
        var self = this;

        application.getModel("AlbumService").getAlbumView(id, function (model) {

            if (model.isAdmin) {

                var adminLinkHref = '/' + username + '/albums/add';
                self.adminLinks = $('<span>').addClass('adminlinks').append("[ ")
                                                            .append(model.adminLinks)
                                                            .append(" ]");
              }

            if (model.photos != null) {
                self.photos.html(model.photos);
            }

            self.description.text(model.description);
            self.title.text(model.title);
            self.photoCount.text(model.photoCount);

            //A little hack to see if photo is already there.
            if (model.photoCount.indexOf("photo") == -1) {

                self.photoCount.text(model.photoCount + " " + (model.photoCount == 1 ? "album" : "albums"));
            }
            
            self.render(id);
        });

    };

    AlbumView.prototype.render = function (id) {
        //Order matters
        var container = $('div#photos');
        container.hide();
        container.empty();

        container.append(this.title);

        if (this.adminLinks) {
            container.append(this.adminLinks);
        }

        if (this.description && this.description.text()) {
            container.append(this.description);
        }
        container.append(this.photoCount);
        container.append(this.photos);

        //Attach lightbox, fancy box and delete confirmation
        this.attach(this.photos != null, this.title.text(), id);
        container.fadeIn(300);
    };

    AlbumView.prototype.showView = function (event) {
        this.populate(event.id);
    };

    AlbumView.prototype.hideView = function (event) {
        var container = $('div#photos');
        container.fadeOut(300);
    };

    AlbumView.prototype.attach = function (hasPhotos, title, id) {


        $("a.fanceyboxmodal").fancybox({
            'width': 460,
            'height': 440,
            'autoScale': false,
            'transitionIn': 'none',
            'transitionOut': 'none',
            'type': 'iframe',
            overlayColor: "#000",
            overlayOpacity: 0.8
        });

        $('a.showimage').imgPreview({
            containerID: 'imgPreviewWithStyles',
            imgCSS: {
                // Limit preview size:
                height: 300
            },
            // When container is shown:
            onShow: function (link) {
                var title = $(link).attr('title');
                if (title) {
                    $('<span>' + title + '</span>').appendTo(this);
                }
            },
            // When container hides: 
            onHide: function (link) {
                $('span', this).remove();
            }
        });

        $("a.removealbum").click(function () {
            $('<p>Are you sure you wish to <strong>delete</strong> the "' + title + '" album? Once deleted, it and it\'s sub-albums <strong>can not</strong> be recovered. </p>')
                        .dialog({
                            width: 460,
                            modal: true,
                            title: "Delete Confirmation",
                            buttons: { "Cancel": function () { $(this).dialog("close"); }, "Yes, delete it!": function () {

                                $.post("/" + username + "/albums/delete/" + id, function (data) { });
                                $(this).dialog("close");
                                window.location = "/" + username + "/albums";
                            } 
                            }
                        });
        });

    }


    return (new AlbumView());

})(jQuery, window.application));

