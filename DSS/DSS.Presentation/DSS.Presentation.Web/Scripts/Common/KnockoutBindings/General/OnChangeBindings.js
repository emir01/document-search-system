/*
    Collection of custom knockout bindings related to property change processing
*/

(function () {
    /*
        Automaticly update a model propety when the input element we are binding to changes for each key press.

        Do not set the modelPropertyToChange to the model property that is value/text bound to the element we are binding
        this to.
    */
    
    ko.bindingHandlers.onChangeUpdateModelProperty = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            // get the default value for the model property we will be updating on change
            // from the allBindingsAccessor
            var defaultValue = allBindingsAccessor().defaultChangeUpdateValue || "";
            
            // get the name of the view model property that will be updated on change
            var modelPropertyToChange = valueAccessor();
        
            //setup a jquery change event on the element
            $(element).on('keyup', function () {

                var newValue = $(this).val();
                
                if(newValue == "") {
                    viewModel[modelPropertyToChange](defaultValue);
                }
                else {
                    viewModel[modelPropertyToChange](newValue);
                }
            });
        },
        
        /*
            Set update so the bound property is set to the default value.
            This will be called only once as the property to which we are bound will not change.
        */
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var defaultValue = allBindingsAccessor().defaultChangeUpdateValue || "";
            
            // get the name of the view model property that will be updated on change
            var modelPropertyToChange = valueAccessor();
            
            var newValue = $(element).val();
            
            if (newValue == "") {
                viewModel[modelPropertyToChange](defaultValue);
            }
            else {
                viewModel[modelPropertyToChange](newValue);
            }
        }
    };
})();