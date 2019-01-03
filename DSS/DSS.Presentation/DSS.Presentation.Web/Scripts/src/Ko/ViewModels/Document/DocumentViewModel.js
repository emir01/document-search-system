// The document view model used to upload documents.
(function (dss, $) {
    var documentViewModel = function (title, authorName) {

        this.title = ko.observable(title);
        this.tmpTitle = ko.observable();
        this.authorName = ko.observable(authorName);
        this.collaborators = ko.observableArray([]);

        this.keywords = ko.observableArray([]);

        this.categories = ko.observableArray([]).extend({ logChange: "Categories" });

        // debug decoration for categories.
    };

    documentViewModel.prototype.addColaborator = function (name) {
        this.collaborators.push(name);
    };

    documentViewModel.prototype.removeColaborator = function (name) {
        this.collaborators.remove(name);
    };

    documentViewModel.prototype.addKeyword = function (keyword) {
        this.keywords.push(keyword);
    };

    documentViewModel.prototype.removeKeyword = function (keyword) {
        this.keywords.remove(keyword);
    };

    documentViewModel.prototype.addCategory = function (category) {
        this.categories.push(category);
    };

    documentViewModel.prototype.removeCategory = function (category) {
        this.categories.push(category);
    };

    // Namespace the document view model
    dss.namespace("Ko.ViewModels.Document.DocumentViewModel", documentViewModel);

})(window.DSS = window.DSS || {}, jQuery)