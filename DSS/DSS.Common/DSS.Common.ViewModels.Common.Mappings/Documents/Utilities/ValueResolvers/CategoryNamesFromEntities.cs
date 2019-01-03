using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.ValueResolvers
{
    /// <summary>
    /// AutoMapper resolver that transforms the List of category entities to a list of category
    /// string names from the Document object
    /// </summary>
    public class CategoryNamesFromEntities : ValueResolver<Document, IList<string>>
    {
        protected override IList<string> ResolveCore(Document source)
        {
            if (source.Categories == null)
            {
                return new List<string>();
            }
            else
            {
                var categoryNames = source.Categories.Select(x => x.Name).ToList();
                return categoryNames;
            }
        }
    }
}