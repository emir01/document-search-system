// The DisplayDocument client side knockout view model used to display documents.
(function() {
    var displayDocumentViewModel = function(id, title, authorName, isIndexed, keywords, categories) {
        // the id of the document
        this.id = ko.observable(id);

        // the actual document title
        this.title = ko.observable(title);

        // the name of the document title
        this.authorName = ko.observable(authorName);
        
        // indicates if the document is indexed
        this.isIndexed = ko.observable(isIndexed);

        // the document keywords
        this.keywords = ko.observableArray(keywords);

        // the document categories
        this.categories = ko.observableArray(categories);
    };

    // Namespace the document display view model
    DSS.namespace("Ko.ViewModels.Document.DisplayDocumentViewModel", displayDocumentViewModel);
})();