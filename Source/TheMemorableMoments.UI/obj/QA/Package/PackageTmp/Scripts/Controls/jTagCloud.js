; (function($) {

    $.fn.tagCloud = function(options) {
        options = options || {};

        var maxPercent = options['maxPercent'] || 200;
        var minPercent = options['minPercent'] || 100;
        var retrieveCount = options['retrieveCount'] || function(element) {
            var tagCount = $(element).attr('rel')
            return (tagCount != null && tagCount.length > 0 ? parseInt(tagCount) : 1);
        };

        var apply = options['apply'] || function(element, size) { $(element).attr('style', 'font-size:' + size + '%;'); };

        var max = null;
        var min = null;

        var tagElements = this;

        tagElements.each(function(index) {
            var count = retrieveCount(this);
            max = (max == null || count > max ? count : max);
            min = (min == null || min > count ? count : min);
        });

        // var multiplier = (maxPercent - minPercent) / (max - min);

        tagElements.each(function(index) {
            var count = retrieveCount(this);
            size = ((maxPercent - minPercent) * count / max) + minPercent;
            apply(this, size);
        });
    }
})(jQuery);