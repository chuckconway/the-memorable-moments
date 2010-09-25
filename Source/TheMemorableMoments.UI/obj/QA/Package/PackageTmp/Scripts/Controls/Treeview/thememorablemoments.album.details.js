var albumDetails = new JS.Class({
    extend: {

        load: function (element) {

            var div = $('div#photos');
            div.fadeOut(300, function () {

                var jsonPath = '/' + username + '/albums/details/' + element.name;
                $.getJSON(jsonPath, function (data) {

                    //possible memory leak.
                    div.html('');
                    div.append($('<h2>')
                                      .text(data.title)
                                      .addClass('albumtitle'));

                    if (data.adminlinks.length > 0) {

                        div.append($('<span>')
                                      .html(data.adminlinks)
                                      .addClass('adminlinks'));
                    }

                    div.append($('<p>')
                                    .text((data.description == null ? '' : data.description))
                                    .addClass('albumdescription'));

                    var countText = (data.count == 1 ? "photo" : "photos");

                    div.append($('<strong>')
                                          .text(data.count + " " + countText)
                                          .addClass('photocount'));
                                          


                    div.fadeIn(300);

                });

            });

        }
    }
});