// Knockout binding that binds the text field to a view model collection
// using the jquery autocomplete multiple value code.
(function () {
    ko.bindingHandlers.ArrayAutoCompleteBinding = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            // get the collection that in the view model
            var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor());
            
            // setup the keywords jquery ui search element
            $(element)
            .bind("keydown", function (event) {
                if (event.keyCode === $.ui.keyCode.TAB &&
						$(this).data("autocomplete").menu.active) {
                    event.preventDefault();
                }
                
                if(event.keyCode === $.ui.keyCode.ENTER) {
                    event.preventDefault();
                }
            })
            .autocomplete({
                minLength: 0,
                source: function (request, response) {
                    // delegate back to autocomplete, but extract the last term
                    response($.ui.autocomplete.filter(
						valueUnwrapped, extractLast(request.term)));
                },
                focus: function () {
                    // prevent value inserted on focus
                    return false;
                },
                select: function (event, ui) {
                    var terms = split(this.value);
                    // remove the current input
                    terms.pop();
                    // add the selected item
                    terms.push(ui.item.value);
                    // add placeholder to get the comma-and-space at the end

                    terms.push("");
                    this.value = terms.join(", ");
                    return false;
                }
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        }
    };

    // binding utility functions
    var split = function (val) {
        return val.split(/,\s*/);
    };

    var extractLast = function (term) {
        return split(term).pop();
    };
    
})();
