using System;
using System.Collections.Generic;
using System.Linq;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Services
{
    /// <summary>
    /// The basic implementation for the keyword service interface
    /// </summary>
    public class KeywordService: IKeywordService
    {
        #region Properties

        private readonly IRepository<Keyword> _keywordRepository;

        #endregion

        #region Constructor 

        public KeywordService (IRepository<Keyword> keywordRepository)
        {
            _keywordRepository = keywordRepository;
        }

        #endregion

        #region Interface Implementation

        public DataResult<IList<Keyword>> GetAllKeywords()
        {
            var result = new DataResult<IList<Keyword>>();
            try
            {
                var keywords = _keywordRepository.ReadAll().ToList();
            
                result.SetSuccess("Successfully read all keywords");
                result.SetData(keywords);
            }
            catch (Exception ex)
            {
                result.SetException(ex,"Exception when retrieving all the keywords");
            }

            return result;
        }

        #endregion
    }
}