window.application.addView((function ($, application) {

    var setOptions = function (dropdown, newValue) {
        dropdown.children('option:selected').removeAttr('selected');
        dropdown.children('option').each(function () {
            if (this.value == newValue) {
                $(this).attr('selected', 'selected');
            }
        });
    };

    // I am the contact list view class.
    function EditView() {
        this.image = null;
        this.photoTitle = null;
        this.story = null;
        this.year = null;
        this.month = null;
        this.day = null;
        this.locationName = null;
        this.latitude = null;
        this.longitude = null;
        this.zoom = null;
        this.maptypeid = null;
        this.selectedalbums = null;
        this.displayAlbums = null;
        this.tags = null;
        this.mediaStatus = null;
        this.rotateLeft = null;
        this.rotateRight = null;
        this.rotateLinks = null;
        this.locationDisplay = null;
    }

    EditView.prototype.init = function () {
        var self = this;

        this.image = $('img#image');
        this.photoTitle = $('input#Title');
        this.story = $('textarea#Story');
        this.year = $('input#SelectedYear');
        this.month = $('select#SelectedMonth');
        this.day = $('select#SelectedDay');
        this.locationName = $('input#locationname');
        this.latitude = $('input#latitude');
        this.longitude = $('input#longitude');
        this.zoom = $('input#zoom');
        this.maptypeid = $('input#maptypeid');
        this.selectedalbums = $('input#selectedalbums');
        this.displayAlbums = $('span#albums');
        this.tags = $('input#Tags');
        this.mediaStatus = $('select#MediaStatus');
        this.rotateLeft = $('a#rotateLeft');
        this.rotateRight = $('a#rotateRight');
        this.rotateLinks = $('p#rotatelinks');
        this.form = $('form#editphotoform');
        this.locationDisplay = $('span#location');

        this.rotateLinks.hide();

    };

    EditView.prototype.setNavigation = function (prevId, nextId, currentPosition, totalCount) {
        //Set positionCount
        var positionText = currentPosition + ' of ' + totalCount;
        $('span#position').text(positionText);

        $('a#prev').attr('href', '#/photo/' + prevId);
        $('a#next').attr('href', '#/photo/' + nextId);

        $('p#photonavigation').fadeIn(400);
    };

    EditView.prototype.navigation = function (id, keys) {

        if (keys !== null && keys.length > 1) {

            var navigation = new NavigationService();
            var imageNav = navigation.getNavigation(id, keys);
            this.setNavigation(imageNav.prev, imageNav.next, imageNav.currentPosition, keys.length);
        }
        else {

            $('p#photonavigation').hide();
        }
    };

    EditView.prototype.setAlbums = function (albums) {
        this.displayAlbums.children('span').remove();

        if (albums != null) {

            for (var index = 0; index < albums.length; index++) {

                var id = albums[index].AlbumId;
                var text = albums[index].AlbumName;
                var found = this.displayAlbums.children('span#' + id);

                if (found[0] == null) {
                    var removeTag = $('<a href="#" title="remove \'untagged\'" class="removetag"></a>').click(remove);
                    var albumTag = $('<span></span>').attr('id', id).text(text).append(removeTag);

                    this.displayAlbums.append(albumTag);

                    var hiddenFieldValues = this.selectedalbums.val() + id + ',';
                    this.selectedalbums.val(hiddenFieldValues);
                }
            }

        }
    };

    EditView.prototype.setLatLong = function (lat, long) {

        this.locationDisplay.hide();
        if (lat !== null && lat !== '' && long !== null && long !== '') {

            $('span#lat').text(lat);
            $('span#long').text(long);
            this.locationDisplay.attr('style', 'display:block;');
        }
    };

    EditView.prototype.populate = function (id, username) {

        var self = this;
        var model = application.getModel("EditModel");

        var keys = model.getMediaKeys();
        self.navigation(id, keys);
        self.rotateRight.attr('href', '/' + username + '/photos/rotateright/' + id);
        self.rotateLeft.attr('href', '/' + username + '/photos/rotateleft/' + id);
        self.form.attr('action', '/' + username + '/photos/savephotodetails/' + id);


        model.getEditDetails(id, username, function (data) {

            var url = data.ImageUrl;

            if (parent.imageChanged != null && parent.imageChanged.length > 0) {

                for (var index = 0; index < parent.imageChanged.length; index++) {

                    if (parent.imageChanged[index] == data.ImageUrl) {

                        var timestamp = new Date().getTime();
                        url += '?' + timestamp;
                    }
                }
            }

            self.image.attr('src', url);
            self.image.fadeIn(200);


            self.photoTitle.val(data.Title);
            self.story.val(data.Story);
            self.year.val(data.Year);
            self.locationName.val(data.LocationName);
            self.latitude.val(data.Latitude);
            self.longitude.val(data.Longitude);
            self.setLatLong(data.Latitude, data.Longitude);

            self.zoom.val(data.Zoom);
            self.maptypeid.val(data.MapTypeId);
            self.tags.val(data.Tags);
            self.setAlbums(data.BelongsToAlbums);

            setOptions(self.mediaStatus, data.Status);

            if (data.Month !== null && data.Month !== '') {
                setOptions(self.month, data.Month);
            }

            if (data.Day !== null && data.Day !== '') {
                setOptions(self.day, data.Day);
            }

            self.rotateLinks.fadeIn(600);
        });

    };

    EditView.prototype.clearValues = function () {

        var self = this;
        self.selectedalbums.val('');
        self.locationDisplay.hide();
        self.photoTitle.val('');
        self.year.val('');
        self.story.val('');
        self.locationName.val('');
        self.latitude.val('');
        self.longitude.val('');
        self.zoom.val('');
        self.maptypeid.val('');
        self.tags.val('');
        self.image.fadeOut(200);
        self.rotateLinks.hide();
        self.image.hide();
        self.month.children('option:selected').removeAttr('selected');
        self.day.children('option:selected').removeAttr('selected');
    };

    EditView.prototype.showView = function (parameters) {

        this.populate(parameters.id, username);
    };

    EditView.prototype.hideView = function (event) {

        this.clearValues();
        //        var container = $('div#imagecontainer');
        //        container.fadeOut(400);
    };

    return (new EditView());

})(jQuery, window.application));