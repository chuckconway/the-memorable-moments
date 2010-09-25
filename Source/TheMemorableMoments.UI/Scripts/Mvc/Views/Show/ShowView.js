function NavigationService() {

    this.getNavigation = function (currentId, ids) {

        var navigation = null;

        if (ids !== null && ids.length > 0) {

            var found = false;

            //if the current is the first id
            if (ids[0] == currentId) {

                navigation = {
                    prev: ids[ids.length - 1],
                    next: ids[1],
                    currentPosition: 1
                };

                found = true;
            }

            //If it's the last one
            if (ids[ids.length - 1] == currentId) {
                navigation = {
                    prev: ids[ids.length - 2],
                    next: ids[0],
                    currentPosition: ids.length
                };
                found = true;
            }

            if (!found) {

                for (var index = 0; index < ids.length; index++) {
                    if (ids[index] == currentId) {
                        navigation = {
                            prev: ids[index - 1],
                            next: ids[index + 1],
                            currentPosition: index + 1
                        };

                        break;
                    }
                }

            }
        }

        return navigation;
    };
}


window.application.addView((function ($, application) {

    // I am the contact list view class.
    function ShowView() {
    }

    ShowView.prototype.init = function () {

    };

    ShowView.prototype.setNavigation = function (prevId, nextId, currentPosition, totalCount) {
        //Set positionCount
        var positionText = currentPosition + ' of ' + totalCount;
        $('span#position').text(positionText);

        $('a#prev').attr('href', '#/photo/' + prevId);
        $('a#next').attr('href', '#/photo/' + nextId);

        $('p#photonavigation').fadeIn(400);
    };

    ShowView.prototype.navigation = function (id, keys) {

        if (keys !== null && keys.length > 1) {

            var navigation = new NavigationService();
            var imageNav = navigation.getNavigation(id, keys);
            this.setNavigation(imageNav.prev, imageNav.next, imageNav.currentPosition, keys.length);
        }
        else {

            this.hideNavigation();
        }
    };

    ShowView.prototype.setTitle = function (title, displayName) {
        var newTitle = (title !== null && title !== '' ? title : "Untitled") + ' - ' + displayName + ' - the memorable moments';

        document.title = newTitle;
    };

    ShowView.prototype.hideNavigation = function () {
        $('p#photonavigation').hide();
    };

    ShowView.prototype.setTitleDescription = function (title, description) {
        $('h3#mediatitle').text(title).fadeIn(1500);
        $('p#mediadescription').text(description).fadeIn(1500);
    };

    ShowView.prototype.setDetails = function (id, photodetails) {
        var self = this;
        $('div#photolinks').fadeIn(1500);
        var editLink = $('div#photolinks').html(photodetails).find('a#editlink');
        var setId = $('input#SetId').val();

        var url = '/' + username + '/photos/edit' + (setId !== null ? '/' + setId : '') + '/#/photo/' + id;
        editLink.attr('href', url);

        editLink.fancybox({
            'width': 900,
            'height': 750,
            'autoScale': false,
            'transitionIn': 'none',
            'transitionOut': 'none',
            'type': 'iframe',
            'overlayOpacity': 0.75,
            'overlayColor': '#000',
            onClosed: function () {

                var image = $('img#image');
                var url = image.attr('src').split('?')[0];

                if (imageChanged != null && imageChanged.length > 0) {

                    for (var index = 0; index < imageChanged.length; index++) {

                        if (parent.imageChanged[index] == url) {

                            var timestamp = new Date().getTime() + Math.random();
                            url += '?' + timestamp;
                            image.attr('src', url);
                        }
                    }
                }

                var model = application.getModel("ShowModel");
                var id = $('div#imagecontainer').attr('name');

                model.getMediaDetails(id, username, function (data) {
                    self.setTitleDescription(data.Title, data.Description);
                    self.setDetails(id, data.details);
                    self.setTitle(data.Title, data.DisplayName);
                });

            }
        });
    };

    ShowView.prototype.setImageSrc = function (imagesrc) {
        var image = $('img#image');
        image.attr('src', imagesrc);
        image.load(function () {
            var container = $('div#imagecontainer');
            container.fadeIn(200);
            var resizer = new Resizer();
            resizer.resize(this.width, this.height, maxWidth, maxHeight);
            var image = $(this);
            var imageWidth = (resizer.width + 23);            
            container.width(imageWidth);
        });

        image.attr('src', imagesrc);
    };

    ShowView.prototype.setSizes = function (id, username) {
        $('a#small').attr('href', '/' + username + '/photos/small/' + id);
        $('a#medium').attr('href', '/' + username + '/photos/medium/' + id);
        $('a#fullsize').attr('href', '/' + username + '/photos/fullsize/' + id);
        $('span#sizes').fadeIn(1500);
    };

    ShowView.prototype.populate = function (id, username) {

        var self = this;
        var model = application.getModel("ShowModel");
        var keys = model.getMediaKeys();

        this.setSizes(id, username);
        this.navigation(id, keys);

        $('div#imagecontainer').attr('name', id);

        model.getMediaDetails(id, username, function (data) {
            self.setTitleDescription(data.Title, data.Description);
            self.setDetails(id, data.details);
            self.setImageSrc(data.imageSrc);
            self.setTitle(data.Title, data.DisplayName);
        });
    };

    ShowView.prototype.showView = function (parameters) {
        var container = $('div#imagecontainer');
        container.hide();
        this.populate(parameters.id, username);

    };

    ShowView.prototype.hideView = function (event) {
        var container = $('div#imagecontainer');
        container.fadeOut(400);
    };

    return (new ShowView());

})(jQuery, window.application));



