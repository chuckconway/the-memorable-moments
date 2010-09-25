window.application.addView((function ($, application) {

    function detailsView() {
        this.location = null;
        this.albums = null;
        this.year = null;
        this.month = null;
        this.day = null
        this.tags = null;
        this.visibility = null;
        this.template = null;

    };

    detailsView.prototype.showView = function (event) {
        this.populate(username);
    };

    detailsView.prototype.hideView = function (event) {

    };


    detailsView.prototype.init = function () {
        var self = this;

        this.template = $($('#detailsTemplate').html());
        this.location = this.template.find('#locationvalue');
        this.albums = this.template.find('span#albumsvalue');
        this.year = this.template.find('input#year');
        this.month = this.template.find('#selectedmonth');
        this.day = this.template.find('#selectedday');
        this.tags = this.template.find('#tags');
        this.visibility = this.template.find('#visibilityselect');

        $.post('/home/getmonths', null, function (data) {
            populateSelect(self.month, data);
        }, 'json');

        $.post('/home/getdays', null, function (data) {
            populateSelect(self.day, data);
        }, 'json');

        $.post('/home/getvisibility', null, function (data) {
            populateSelect(self.visibility, data);
        }, 'json');
    };


    detailsView.prototype.populate = function (username) {

        this.init();
        var self = this;

        application.getModel("uploadService").getDetailsModel(username, function (model) {

            self.location.text(model.location);
            self.albums.text(model.albums);
            self.year.val(model.year);
            self.tags.val(model.tags);

            self.render();
        });

    }

    function populateSelect(element, values) {

        for (var i = 0; i < values.length; i++) {

            element.append($("<option>").val(values[i].Value).text(values[i].Text));
        }
    }


    detailsView.prototype.render = function () {

        var navigation = new Navigation();
        navigation.setTab('Details');

        var container = $('div#mvccontainer');
        container.hide();
        container.empty();
        container.append(this.template);
        container.fadeIn(300);
    }


    detailsView.prototype.attach = function () {

    }

    return (new detailsView());

})(jQuery, window.application));