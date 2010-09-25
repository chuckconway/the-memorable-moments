var batchId;
$(document).ready(function () {

    document.onkeypress = stopRKey; 

    $('input#useExifDate').click(function () {

        if (this.checked) {

            $('p.editdatesection').fadeOut(100);
        }
        else {
            $('p.editdatesection').fadeIn(100);
        }
    });

    $('#fileInput').uploadify(
	        {
	            'uploader': '/Content/uploadify.swf',
	            'script': '/MediaUpload/Services/<%= Model.Id %>/<%= Model.BatchId  %>',
	            'folder': '/uploads',
	            'multi': true,
	            'cancelImg': '/Content/images/cancel.png',
	            onAllComplete: function (event, data) {
	                $('a#nextlink').click(function() { window.location = this.href;}).attr('href', '/' + username + '/upload/adddetails/' + batchId + '/#/show').fadeIn(400);
	            }
	        });

    $("input#tags").autosearch("/" + username + "/photos/TagSearch", {
        width: 320,
        max: 10,
        highlight: false,
        multiple: true,
        multipleSeparator: " ",
        scroll: true,
        scrollHeight: 300
    });

    $('input#locationname').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/' + username + '/photos/SearchLocations',
                dataType: "json",
                data: {
                    featureClass: "P",
                    style: "full",
                    maxRows: 12,
                    name_startsWith: request.term
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.LocationName,
                            value: item.LocationName,
                            data: item
                        }
                    }))
                }
            })
        },

        select: function (event, ui) {

            $(this).val(ui.item.value);
            setHiddenFields(ui.item.data.Latitude, ui.item.data.Longitude, ui.item.data.Zoom, ui.item.data.MapTypeId);
            return false;
        }
    });


    $("#albumstree").jstree({

        "json_data": {
            // I chose an ajax enabled tree - again - as this is most common, and maybe a bit more complex
            // All the options are the same as jQuery's except for `data` which CAN (not should) be a function
            "ajax": {
                // the URL to fetch the data
                "url": "/chuckconway/albums/GetNodes",
                // this function is executed in the instance's scope (this refers to the tree instance)
                // the parameter is the node being loaded (may be -1, 0, or undefined when loading the root nodes)
                "data": function (n) {
                    // the result is fed to the AJAX request `data` option
                    return {
                        "operation": "get_children",
                        "id": n.attr ? n.attr("id").replace("node_", "") : 0
                    };
                }
            }
        },


        "themes": {
            "theme": "default",
            "dots": true,
            "icons": false,
            "url": "/Content/jsTree/themes/default/style.css"
        },

        "plugins": ["themes", "json_data", "ui", "crrm", "cookies", "dnd", "search", "types", "hotkeys", "contextmenu"]

    });

    $('a#uploadbutton').live("click", function (e) {

        $.post('/' + username + '/upload/createbatchid', function (data) {
            batchId = data.id;
            $('input#batchId').val(batchId);

            $('form#uploadform').ajaxSubmit();
            $('#fileInput').uploadifySettings('script', '/' + username + '/Services/Photos/' + batchId + '/');
            $('#fileInput').uploadifyUpload();          
        }, "json");

        return false;
    });

    $('a#clearqueue').live("click", function (e) {

        $('#fileInput').uploadifyClearQueue();
        return false;
    });

    $('a#nextlink').live("click", function (e) {

        $('#fileInput').uploadifySettings('script', '/' + username + '/Upload/Photos/');
        $('#fileInput').uploadifyUpload();

        return false;
    });

    $('div#setlocationdropdown label a#setlocation').live("click", function (e) {

        var latitude = $('input#latitude').val();
        var longitude = $('input#longitude').val();
        var zoom = $('input#zoom').val();
        var mapType = $('input#maptypeid').val();

        var maps;
        if (!maps) {

            maps = new GoogleMaps();
            var divMap = $('div#map');
            maps.SetMarker(divMap[0], latitude, longitude, zoom, mapType);
        }

        $('input#searchButton').click(function () {

            var address = $('input#searchText').val();
            if (geocoder) {
                geocoder.geocode({ 'address': address }, function (results, status) {

                    latitude = results[0].geometry.location.lat();
                    longitude = results[0].geometry.location.lng();
                    zoom = map.getZoom();

                    setHiddenFields(latitude, longitude, zoom, map.getMapTypeId());

                    if (status == google.maps.GeocoderStatus.OK) {
                        mapMarkers.addMarker(results[0].geometry.location);
                        map.setCenter(results[0].geometry.location);

                    } else {
                        alert("Geocode was not successful for the following reason: " + status);
                    }
                });
            }

            return false;
        });

        return false;

    });

    $("div#albumstree a").live("click", function (e) {
        var self = $(this);
        var hiddenField = $('input#selectedalbums');
        var display = $('span#albums');
        var id = self.parent().attr('id').replace("node_", "");
        var found = display.children('span#' + id);

        if (found[0] == null) {
            var text = self.text().replace(/\(\d*\)/g, "");
            var removeTag = $('<a href="#" title="remove \'untagged\'" class="removetag"></a>').click(remove);
            var albumTag = $('<span></span>').attr('id', id).text(text).append(removeTag);

            display.append(albumTag);

            var hiddenFieldValues = hiddenField.val() + id + ',';
            hiddenField.val(hiddenFieldValues);
        }

        return false;
    });


    /* for keeping track of what's "open" */
    var activeClass = 'dropdown-active', showingDropdown, showingMenu, showingParent;
    /* hides the current menu */
    var hideMenu = function () {
        if (showingDropdown) {
            showingDropdown.removeClass(activeClass);
            showingMenu.hide();
        }
    };

    /* recurse through dropdown menus */
    $('.dropdown').each(function () {
        /* track elements: menu, parent */
        var dropdown = $(this);
        var menu = dropdown.parent().next('div.dropdown-menu'), parent = dropdown.parent().parent(); //$('div.dropdowncontainer');
        /* function that shows THIS menu */
        var showMenu = function () {
            hideMenu();
            showingDropdown = dropdown.addClass('dropdown-active');
            showingMenu = menu.show();
            showingParent = parent;
        };
        /* function to show menu when clicked */
        dropdown.bind('click', function (e) {
            //if (e) e.stopPropagation();
            if (e) e.preventDefault();
            showMenu();
        });
        /* function to show menu when someone tabs to the box */
        dropdown.bind('focus', function () {
            showMenu();
        });
    });

    /* hide when clicked outside */
    $(document.body).bind('click', function (e) {
        if (showingParent) {
            var parentElement = showingParent[0];
            if (!$.contains(parentElement, e.target) || !parentElement == e.target) {
                hideMenu();
            }
        }
    });

});


function setHiddenFields(latitude, longitude, zoom, mapTypeId) {

    $('span#location').css('display', 'block');

    $('span#lat').text(latitude);
    $('span#long').text(longitude);

    $('input#latitude').val(latitude);
    $('input#zoom').val(zoom);
    $('input#longitude').val(longitude);
    $('input#maptypeid').val(mapTypeId);

};





