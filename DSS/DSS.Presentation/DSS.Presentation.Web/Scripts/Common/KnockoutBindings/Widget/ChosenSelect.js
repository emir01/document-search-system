(function (dss, $) {
    
    /*
        Widget initialization plugin for chosen. Relies on the exsisting options binding to populate a select element
        and then chosen is initialized
    */

    ko.bindingHandlers.chosenSimpleSelect = {
        init: function (element, valueAccessor, allBindingsAccessor, context) {
            // call the simple options functionality
            ko.bindingHandlers.options.init(element, valueAccessor, allBindingsAccessor, context);
        },
        
        update: function (element, valueAccessor, allBindingsAccessor, context) {
            // call the simple options default functionality
            ko.bindingHandlers.options.update(element, valueAccessor, allBindingsAccessor, context);
            
            // We will fire chosen when the bound observable gets updated with data ? 
            // Call chosen on the element after setting up the options binding
            $(element).chosen();
        }
    };
    
})(window.DSS = window.DSS || {} , jQuery)