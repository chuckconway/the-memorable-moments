function Navigation () {
    this.setTab = function (tabText) {

        this.init();
        var self = this;
        self.items.each(function (index, item) {

            var anchor = $(item).children('a').removeClass('youarehere');

            if (anchor.text() == tabText) {
                anchor.addClass('youarehere');
            }
        });

        this.render();
    };

    this.render = function () {

        var ultabs = this.container.children('ul.tabs');
        ultabs.empty();
        ultabs.append(this.items);
    }

    this.init = function () {
        var self = this;

        this.template = $('#navigationTemplate');
        this.items = $(this.template.html()).find('ul.tabs li');
        this.container = $('div.nav');
    };
};