
var ImageHelper = new JS.Class({
    extend: {
        resize: function (curentWidth, currentHeight, maxWidth, maxHeight) {

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
        }
    }
});

$(document).ready(function () {

    $("a#editlink").fancybox({
        'width': 900,
        'height': 640,
        'autoScale': false,
        'transitionIn': 'none',
        'transitionOut': 'none',
        'type': 'iframe',
        'overlayOpacity': 0.75,
        'overlayColor': '#000'
    });

    var image = $('img#image');
    image.load(function () {

        sizeImageContainer(this);
    });

    image.attr('src', image.attr('src'));
});

function sizeImageContainer(element) {

    var image = $(element);   
    var imageWidth = image.width();
//    var imageHeight = image.height();

//   ImageHelper.resize(imageWidth, imageHeight, maxWidth, maxHeight);

   $('div#imagecontainer').width(imageWidth);

}