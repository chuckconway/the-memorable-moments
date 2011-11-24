window.application.addView((function ($, application) {

    var loadedPhotoCount = 0;
    var timer;

    // I am the contact list view class.
    function View() {
        this.persistentKey = null;
        this.photoCount = null;
        this.loadedPhotos = null;
        this.batchId = null;
    }

    View.prototype.init = function () {
        var self = this;

        this.photoCount = $('input#PhotoCount').val();
        this.loadedPhotos = $('ul#groupedphotos');
        this.batchId = $('input#BatchId').val();
    };

    View.prototype.populate = function (id, username) {

        var self = this;
        var model = application.getModel("ViewModel");
        var persistentKey = model.getPersistentKey();

        model.getDetails(id, username, function (data) {

            if (data.length < parseInt(self.photoCount)) {
                timer = setTimeout("self.populate($('input#BatchId').val(), username)", 10000);
            }
            else {

                if (timer != null) {
                    clearTimeout(timer);
                }
            }

            self.loadedPhotos.empty();
            for (var index = 0; index < data.length; index++) {

                

                var li = '<li>' +
        					 '<span class="image">' +
        						'<a title="" class="showimage" href="/' + username + '/photos/edit/' + persistentKey + '/#/photo/' + data[index].Id + '" rel="' + data[index].Rel + '" name="' + data[index].Id + '">' +
        							'<img alt="" src="' + data[index].Path + '">' +
        						'</a>' +
        					  '</span>' +
        		         '</li>';

                self.loadedPhotos.append(li);
            }

            attachImgPreview('a.showimage');
            $("a.showimage").fancybox({
                'width': 900,
                'height': 750,
                'autoScale': false,
                'transitionIn': 'none',
                'transitionOut': 'none',
                'type': 'iframe',
                'overlayOpacity': 0.75,
                'overlayColor': '#000'
            });
        });
    };

    View.prototype.showView = function (parameters) {
        this.populate(this.batchId, username);
    };

    View.prototype.hideView = function (event) {
    };

    return (new View());

})(jQuery, window.application));