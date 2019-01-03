using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.ValueResolvers
{
    /// <summary>
    /// AutoMapper resolver that transforms the List of keyword entities to a list of category
    /// string names from the document object
    /// </summary>
    public class KeywordNamesFromEntities : ValueResolver<Document, IList<string>>
    {
        protected override IList<string> ResolveCore(Document source)
        {
            if (source.Keywords == null)
            {
                return new List<string>();
            }
            else
            {
                var keywordNames = source.Keywords.Select(x => x.Name).ToList();
                return keywordNames;
            }
        }
    }
}