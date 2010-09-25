function GoogleMaps() {


    this.SetMarker = function (element, lat, long, zoom, mapTypeId) {

        var latlng = new google.maps.LatLng(-34.397, 150.644)
        var internalZoom = 8;
        var internalMapType = google.maps.MapTypeId.ROADMAP;

        if (lat && long && zoom && mapTypeId) {
            latlng = new google.maps.LatLng(parseFloat(lat), parseFloat(long))
            internalZoom = parseInt(zoom);
            internalMapType = mapTypeId;
        }

        geocoder = new google.maps.Geocoder();
        var myOptions = {
            zoom: internalZoom,
            center: latlng,
            mapTypeId: internalMapType
        }

        map = new google.maps.Map(element, myOptions);
        mapMarkers.addMarker(latlng);

        google.maps.event.addListener(map, 'zoom_changed', function () {
            $('input#zoom').val(map.getZoom());
        });

        google.maps.event.addListener(map, 'maptypeid_changed', function () {
            $('input#maptypeid').val(map.getMapTypeId());
        });

        google.maps.event.addListener(map, 'click', function (location) {

            mapMarkers.deleteOverlays();
            mapMarkers.addMarker(location.latLng);

            setHiddenFields(location.latLng.lat(), location.latLng.lng(), map.getZoom(), map.getMapTypeId());
        });
    }
};

function MapMarkers() {

    var markersArray = [];

    this.addMarker = function(location) {
        marker = new google.maps.Marker({
            position: location,
            map: map
        });
        markersArray.push(marker);
    }

    // Removes the overlays from the map, but keeps them in the array
   this.clearOverlays = function () {
        if (markersArray) {
            for (i in markersArray) {
                markersArray[i].setMap(null);
            }
        }
    }

    // Shows any overlays currently in the array
   this.showOverlays = function() {
        if (markersArray) {
            for (i in markersArray) {
                markersArray[i].setMap(map);
            }
        }
    }

    // Deletes all markers in the array by removing references to them
    this.deleteOverlays = function() {
        if (markersArray) {
            for (i in markersArray) {
                markersArray[i].setMap(null);
            }
            markersArray.length = 0;
        }
    }

};