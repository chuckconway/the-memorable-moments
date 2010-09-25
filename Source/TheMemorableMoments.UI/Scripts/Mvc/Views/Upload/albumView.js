window.application.addView((function ($, application) {

    // I am the contact list view class.
    function albumView() {
        this.albumTree = null;
        this.selectedalbumns = null;

        //this.load = function (albumId) { };
    };

    albumView.prototype.init = function () {

        var self = this;

        this.template = $('#albumTemplate');
        this.temp = $(this.template.html());
        //this.mapContainer = $(templateHtml);

        this.albumTree = this.temp.find('ul#albums');
        this.selectedalbumns = this.temp.find('ul#selectedalbums');


    };

    albumView.prototype.showView = function (event) {
        this.populate();
    };

    albumView.prototype.hideView = function (event) {

    };

    albumView.prototype.render = function () {

        var navigation = new Navigation();
        navigation.setTab('Add Albums');

        var container = $('div#mvccontainer');
        container.hide();
        container.empty();
        container.append(this.temp);
        container.fadeIn(300);

        this.attach();

    };

    albumView.prototype.populate = function () {

        this.render();
    };

    albumView.prototype.attach = function () {

        this.albumTree.treeview({
            url: "/" + username + "/albums/nodes"
        });

    };

    return (new albumView());

})(jQuery, window.application));