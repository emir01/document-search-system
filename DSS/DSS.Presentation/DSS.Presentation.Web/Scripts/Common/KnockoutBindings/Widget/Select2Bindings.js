(function (dss, $) {

    /*
        Widget initialization plugin for chosen. Relies on the exsisting options binding to populate a select element
        and then chosen is initialized
    */

    ko.bindingHandlers.simpleSelect2Binding = {
        init: function (element, valueAccessor, allBindingsAccessor, context) {

            var options = valueAccessor();

            var data = options.data;
            var selected = options.selected;

            // call the simple options functionality
            ko.bindingHandlers.options.init(element, data, allBindingsAccessor, context);

            // setup the event handlers that should update the observable array on changes.
            $(element)
                .on("change", function (e) {
                })
                .on('select2-selecting', function (e) {
                    selected.push(e.val);

                    // we return here after the update gets called and the value is added manually to array
                    // because of observable array change.

                    // but now we re-add the value again in the UI.
                    // manually close the selection and return false
                    $(element).select2("close");
                    return false;
                })
                .on('select2-removed', function (e) {
                    selected.remove(e.val);
                });

            $(element).select2();
        },

        /*
         * Params:
         * 
         * valueAccessor - is the value fors all the data/selectable elements for the binding.
         */
        update: function (element, valueAccessor, allBindingsAccessor, context) {

            var options = valueAccessor();
            var data = options.data;

            // do the options update to set the initial data when its loaded with ajax.
            ko.bindingHandlers.options.update(element, data, allBindingsAccessor, context);

            // Go over selected the selected observable array and set selected
            // values

            var selected = options.selected();

            var processedSelected = [];

            // need to clear selection in the select2 plugin
            $(element).select2("val", "");

            var optionValueAccessor = function (value) {
                return value;
            };

            var optionTextAccessor = function (value) {
                return value;
            };

            if ("selectedText" in options) {
                optionTextAccessor = function (value) {
                    return value[value.optionsText];
                };
            }

            if ("selectedValue" in options) {
                optionValueAccessor = function (value) {
                    return value[value.optionsValue];
                };
            }

            // set the data on the UI element from the data on the 
            // observable array.
            $.each(selected, function (key, value) {
                var id = optionValueAccessor(value);

                console.log("Value of selected: " + id);

                var text = optionTextAccessor(value);
                
                console.log("Text of selected: " + text);
                
                if (text == undefined) {
                    // get the value/id from the selected option
                    // and try to find it in the data
                    
                    for (var kk = 0, length = data.length; kk < length; kk++) {
                        var dataObject = data[kk];
                        var dataObjectValue = optionValueAccessor(dataObject);

                        console.log("Data object value iteration [" + kk + "]: " + dataObjectValue);
                    }
                }

                processedSelected.push({ id: id, text: text });
            });

            $(element).select2("data", processedSelected);
        }
    };

    /*
     * ========================================================================================
     * Third party binding code. Analyze at some other point.
     * ========================================================================================
     */

    ko.bindingHandlers.select2 = {
        init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
            ko.utils.domNodeDisposal.addDisposeCallback(el, function () {
                $(el).select2('destroy');
            });

            var allBindings = allBindingsAccessor(),
                select2 = ko.utils.unwrapObservable(allBindings.select2);

            $(el).select2(select2);
        },
        update: function (el, valueAccessor, allBindingsAccessor, viewModel) {
            var allBindings = allBindingsAccessor();

            if ("value" in allBindings) {
                $(el).select2("data", allBindings.value());
            } else if ("selectedOptions" in allBindings) {
                var converted = [];

                var textAccessor = function (value) { return value; };

                if ("optionsText" in allBindings) {

                    textAccessor = function (value) {
                        var valueAccessor = function (item) { return item; }

                        if ("optionsValue" in allBindings) {
                            valueAccessor = function (item) { return item[allBindings.optionsValue]; }
                        }

                        var items = $.grep(allBindings.options(), function (e) { return valueAccessor(e) == value });

                        if (items.length == 0 || items.length > 1) {
                            return "UNKNOWN";
                        }

                        return items[0][allBindings.optionsText];
                    }
                }

                $.each(allBindings.selectedOptions(), function (key, value) {
                    converted.push({ id: value, text: textAccessor(value) });
                });

                $(el).select2("data", converted);
            }
        }
    };

})(window.DSS = window.DSS || {}, jQuery)