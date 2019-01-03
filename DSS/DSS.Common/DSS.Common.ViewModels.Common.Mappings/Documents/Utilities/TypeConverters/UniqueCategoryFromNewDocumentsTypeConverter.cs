using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSS.Common.ViewModels.Documents;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.TypeConverters
{
    /// <summary>
    /// Converts a list of Add Document View Model objects to a list of unique category objects.
    /// </summary>
    public class UniqueCategoryFromNewDocumentsTypeConverter : ITypeConverter<List<AddDocumentViewModel>, List<Category>>
    {
        #region Implementation of ITypeConverter<in List<AddDocumentViewModel>,out List<Category>>

        public List<Category> Convert(ResolutionContext context)
        {
            var listOfDocuments = context.SourceValue as List<AddDocumentViewModel>;

            if (listOfDocuments == null)
            {
                return new List<Category>();
            }

            var uniqueCategories = new List<Category>();

            // go through each of the documents and 
            foreach (var document in listOfDocuments)
            {
                var currentDocumentCategories = document.CategoryList;

                if (currentDocumentCategories == null || currentDocumentCategories.Count == 0)
                {
                    continue;
                }

                // check if any of the current document categories are inserted into 
                // the unique category list
                foreach (var currentDocumentCategoryId in currentDocumentCategories)
                {
                    if (uniqueCategories.All(x => x.Id != currentDocumentCategoryId))
                    {
                        var newUniqueCategory = new Category()
                                                    {
                                                        Id = currentDocumentCategoryId
                                                    };

                        uniqueCategories.Add(newUniqueCategory);
                    }
                }
            }

            return uniqueCategories;
        }

        #endregion
    }
}