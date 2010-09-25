
$(document).ready(function () {

    $("a#editlink").fancybox({
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
            var url = image.attr('src');

            if (imageChanged != null && imageChanged.length > 0) {

                for (var index = 0; index < imageChanged.length; index++) {

                    if (parent.imageChanged[index] == url) {

                        var timestamp = new Date().getTime();
                        url += '?' + timestamp;
                    }
                }
            }

            image.attr('src', url);         
         }
    });

    var image = $('img#image');
    image.load(function () {
        sizeImageContainer(this);
    });

    image.attr('src', image.attr('src'));
});

function sizeImageContainer(element) {

    var resizer = new Resizer();
    resizer.resize(element.width, element.height, maxWidth, maxHeight);
    var image = $(element);
    var imageWidth = (resizer.width + 23);
   $('div#imagecontainer').width(imageWidth);
}


function Resizer(){

    this.resize = function (curentWidth, currentHeight, maxWidth, maxHeight) {

        var newWidth;
        var newHeight;

        if (curentWidth > maxWidth || currentHeight > maxHeight) {

            if (curentWidth > maxWidth && curentWidth > currentHeight) {

                var ratio = maxWidth / curentWidth;
                newWidth = curentWidth * ratio;
                newHeight = currentHeight * ratio;

            } else {

                var ratio = maxHeight / currentHeight;
                newHeight = currentHeight * ratio;
                newWidth = curentWidth * ratio;
            }

            this.width = newWidth;
            this.height = newHeight;

        } else {

            this.width = curentWidth;
            this.height = currentHeight;
        }
    };
}
