function NavigationService() {

    this.getNavigation = function (currentId, ids) {

        var navigation = null;

        if (ids !== null && ids.length > 0) {

            var found = false;

            //if the current is the first id
            if (ids[0] == currentId) {

                navigation = {
                    prev: ids[ids.length - 1],
                    next: ids[1],
                    currentPosition: 1
                };

                found = true;
            }

            //If it's the last one
            if (ids[ids.length - 1] == currentId) {
                navigation = {
                    prev: ids[ids.length - 2],
                    next: ids[0],
                    currentPosition: ids.length
                };
                found = true;
            }

            if (!found) {

                for (var index = 0; index < ids.length; index++) {
                    if (ids[index] == currentId) {
                        navigation = {
                            prev: ids[index - 1],
                            next: ids[index + 1],
                            currentPosition: index + 1
                        };

                        break;
                    }
                }

            }
        }

        return navigation;
    };
}