using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.Keywords;
using DSS.Presentation.Web.Controllers.Api.Interface;

namespace DSS.Presentation.Web.Controllers.Api
{
    public class KeywordController : ApiController, IEntityApi<Guid, DisplayKeywordViewModel>
    {
        #region Properties

        private readonly IKeywordService _keywordService;

        #endregion

        #region Constructor

        public KeywordController(IKeywordService keywordService)
        {
            _keywordService = keywordService;
        }

        #endregion

        #region Actions

        /// <summary>
        /// GET api/{entitiy}
        /// HTTP Get Call that returns an IEnumerable list of all the entities
        /// </summary>
        /// <returns>IEnumerable collection of the entitiy objects</returns>
        public IEnumerable<DisplayKeywordViewModel> Get()
        {
            var result = _keywordService.GetAllKeywords();

            if(result.Status == ResultStatus.Success)
            {
                // get the data from the result
                var allKeywords = result.GetData();

                // translate the keyword domain list to the view model
                var viewModelKeywords = allKeywords.Select(Mapper.Map<DisplayKeywordViewModel>);

                // return the keywords
                return viewModelKeywords;
            }
            else
            {
                // return an empty list of keywords
                return new List<DisplayKeywordViewModel>();
            }
        }

        /// <summary>
        /// GET api/entity/id
        /// HTTP GET with an id URL parameter for getting a single entitiy.
        /// </summary>
        /// <param name="id">The id of entity with type TEntitiyKey</param>
        /// <returns>A single object of type TEntity</returns>
        public DisplayKeywordViewModel Get(Guid id)
        {
            var result = _keywordService.GetAllKeywords();

            if(result.Status == ResultStatus.Success)
            {
                var data = result.GetData();

                // filter on the keyword id
                var keyword = data.FirstOrDefault(x => x.Id == id);

                if(keyword == null)
                {
                    return null;
                }

                // Map the keyword to the view model and return it to the calling code
                var keywordViewModel = Mapper.Map<DisplayKeywordViewModel>(keyword);

                return keywordViewModel;
            }
            else
            {
                // If the service result was not a success
                return null;
            }
        }

        /// <summary>
        /// POST api/entity
        /// HTTP POST call to create a given entitiy
        /// </summary>
        /// <param name="value">The data describing ther entitiy based on the TEntitiy type</param>
        public void Post(DisplayKeywordViewModel value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PUT api/{entitiy}/{id}
        /// HTTP PUT call to update an entitiy with the given id
        /// </summary>
        /// <param name="id">The id of the parameter we are going to update</param>
        /// <param name="value">The information describing the new updated values for the entitiy with the given id</param>
        public void Put(Guid id, DisplayKeywordViewModel value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// DELETE api/{entitiy}/{id}
        /// HTTP DELETE call to delete the entitiy with the given id.
        /// </summary>
        /// <param name="id">The id of the entity we want to delete.</param>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
