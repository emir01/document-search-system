using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSS.Common.ViewModels.Documents;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.ValueResolvers
{
    public class KeywordsFromStringResolver : ValueResolver<AddDocumentViewModel, IList<Keyword>>
    {
        protected override IList<Keyword> ResolveCore(AddDocumentViewModel source)
        {

            if (source.KeywordsList != null && source.KeywordsList.Count > 0)
            {
                // transfrom the list name to a list of objects
                return (from s in source.KeywordsList
                        select s.Trim()
                            into trimedName
                            where !string.IsNullOrWhiteSpace(trimedName)
                            select new Keyword()
                                       {
                                           Name = trimedName
                                       }).ToList();
            }
            else
            {
                return new List<Keyword>();
            }
        }
    }
}