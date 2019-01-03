using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSS.Common.ViewModels.Documents;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.TypeConverters
{
    /// <summary>
    /// Process a list of AddDocumentViewModel entities, extracting and returning only a list of the unique keywords
    /// </summary>
    public class UniqueKeywordsFromNewDocumentsTypeConverter : ITypeConverter<List<AddDocumentViewModel>, List<Keyword>>
    {
        #region Implementation of ITypeConverter<in List<AddDocumentViewModel>,out List<string>>

        public List<Keyword> Convert(ResolutionContext context)
        {
            var addDocumentViewModelList = context.SourceValue as List<AddDocumentViewModel>;

            if (addDocumentViewModelList == null)
            {
                return new List<Keyword>();
            }
            else
            {
                var uniqueKeywordList = new List<string>();

                foreach (var addDocumentViewModel in addDocumentViewModelList)
                {
                    if (addDocumentViewModel.KeywordsList == null)
                    {
                        continue;
                    }

                    foreach (var keyword in addDocumentViewModel.KeywordsList)
                    {
                        // if the keyword has not been added before
                        if(uniqueKeywordList.All(x => x != keyword.ToLower()))
                            uniqueKeywordList.Add(keyword.ToLower());
                    }
                }

                // The keywords are stored and compared with all the small letters
                // We return the keyword list created from the unique keyworl small caps names
                return uniqueKeywordList.Select(x=> new Keyword
                                                  {
                                                      Name =  x
                                                  }).ToList();
            }
        }

        #endregion
    }
}