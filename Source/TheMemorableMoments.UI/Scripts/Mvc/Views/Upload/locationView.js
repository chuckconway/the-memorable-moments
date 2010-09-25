window.application.addView((function ($, application) {

    // I am the contact list view class.
    function locationView() {
        this.searchText = null;
        this.locationName = null;
        this.template = null;

        //this.load = function (albumId) { };
    };

    locationView.prototype.showView = function (event) {
        this.populate(username);
    };

    locationView.prototype.hideView = function (event) {

    };

    locationView.prototype.init = function () {
        var self = this;

        this.template = $('#locationTemplate');
        this.temp = $(this.template.html());
        //this.mapContainer = $(templateHtml);

        //hacky as all hell... 
        this.leftColumn = $(this.temp[0]);
        this.rightColumn = $(this.temp[2]);

        this.searchText = this.rightColumn.find('input#searchText');
        this.searchButton = this.rightColumn.find('input#searchButton');
        this.locationName = this.leftColumn.find('input#locationName');
        this.map = $('<div>').attr('id', 'map');
    };


    locationView.prototype.populate = function (username) {

        var self = this;

        this.searchButton.click(function () {
            var address = self.searchText.val();
            if (geocoder) {
                geocoder.geocode({ 'address': address }, function (results, status) {
                    if (status == google.maps.GeocoderStatus.OK) {
                        map.setCenter(results[0].geometry.location);
                        var marker = new google.maps.Marker({
                            map: map,
                            position: results[0].geometry.location
                        });
                    } else {
                        alert("Geocode was not successful for the following reason: " + status);
                    }
                });
            }
        });

        self.render();
    };

    locationView.prototype.render = function () {

        var navigation = new Navigation();
        navigation.setTab('Set Location');

        var container = $('div#mvccontainer');
        this.rightColumn.append(this.map);

        container.hide();
        container.empty();
        container.append(this.leftColumn).append(this.rightColumn);
        container.append("<br class=\"clearboth\" />");
        container.fadeIn(300);

        var maps = new GoogleMaps();
        maps.SetMarker(this.map[0]);
    };

    return (new locationView());

})(jQuery, window.application));