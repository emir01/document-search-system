(function ($) {
    var keywordViewModel = function (id, name, alias) {
        this.id = ko.observable(id);
        this.name = ko.observable(name);
        this.alias = ko.observable(alias);
    };

    DSS.namespace("Ko.ViewModels.Keyword.KeywordViewModel", keywordViewModel);
})(jQuery);