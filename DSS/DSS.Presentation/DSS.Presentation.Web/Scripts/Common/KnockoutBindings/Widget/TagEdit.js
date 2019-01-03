(function (dss, $) {
    /*
      Widget initialization pluguin for tag edit. Initialize tag edit for the bound element
  */

    ko.bindingHandlers.simpleTagEdit = {
        init: function (element, valueAccessor, allBindingsAccessor, context) {
            ko.bindingHandlers.simpleTagEdit.initSimpleTagEdit(element, valueAccessor, allBindingsAccessor, context);
        },

        // Workaround and simple hack around the name not beeing initialized issue
        initSimpleTagEdit: function (element, valueAccessor, allBindingsAccessor, context) {

            if ($(element).attr("name") != undefined) {

                var listOfCurrentKeywords = ko.utils.unwrapObservable(valueAccessor());

                var labelField = allBindingsAccessor().tagEditLabelField;

                // create the appropriate list of local keywords 

                var mappedListOfKeywords = _.map(listOfCurrentKeywords, function (object, num) {
                    return {
                        id: num,
                        value: object.Alias,
                        label: object.Alias
                    };
                });

                $(element).tagedit({
                    autocompleteOptions: {
                        source: false
                    },

                    breakKeyCodes: [13, 44, 32, 46, 59]
                });
            } else {
                setTimeout(
                    function () {
                        ko.bindingHandlers.simpleTagEdit.initSimpleTagEdit(element, valueAccessor, allBindingsAccessor, context);
                    }, 100);
            }
        }
    };
})(window.DSS = window.DSS || {}, jQuery);