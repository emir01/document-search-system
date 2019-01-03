(function ($) {
    var categoryViewModel = function (id, name, alias) {
        this.id = ko.observable(id);
        this.name = ko.observable(name);
        this.alias = ko.observable(alias);
    };
    
    // Add the category view model to a namespace
    DSS.namespace("Ko.ViewModels.Category.CategoryViewModel", categoryViewModel);
})(jQuery);