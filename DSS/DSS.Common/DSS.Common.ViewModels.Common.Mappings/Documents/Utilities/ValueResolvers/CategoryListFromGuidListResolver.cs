using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSS.Common.ViewModels.Documents;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.ValueResolvers
{
    /// <summary>
    /// Category resolver that transforms the concatanated string of categories
    /// to a list of category objects
    /// </summary>
    public class CategoryListFromGuidListResolver : ValueResolver<AddDocumentViewModel, IList<Category>>
    {
        protected override IList<Category> ResolveCore(AddDocumentViewModel source)
        {
            if (source.CategoryList != null && source.CategoryList.Count > 0)
            {
                // transform the category Guid list to a list of category objects

                var categoryEntitiyList = source.CategoryList.Select(x => new Category()
                                                                              {
                                                                                  Id = x
                                                                              }).ToList();
                return categoryEntitiyList;
            }
            else
            {
                return new List<Category>();
            }
        }
    }
}