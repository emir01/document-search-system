using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.Documents;
using DSS.Presentation.Web.Controllers.Api.Interface;

namespace DSS.Presentation.Web.Controllers.Api
{
    public class DocumentController : ApiController, IEntityApi<Guid, DisplayDocumentViewModel>
    {
        #region Propertiers

        private readonly IDocumentsService _documentService;

        #endregion

        #region Constructor

        public DocumentController(IDocumentsService documentService)
        {
            _documentService = documentService;
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// GET api/{entitiy}
        /// HTTP Get Call that returns an IEnumerable list of all the entities
        /// </summary>
        /// <returns>IEnumerable collection of the entitiy objects</returns>
        public IEnumerable<DisplayDocumentViewModel> Get()
        {
            var results = _documentService.GetDocuments();

            if(results.Status == ResultStatus.Success)
            {
                var data = results.GetData();

                if(data!= null)
                {
                    var viewModelData = data.Select(Mapper.Map<DisplayDocumentViewModel>);
                    return viewModelData;
                }
                else
                {
                    return new List<DisplayDocumentViewModel>();
                }
            }
            else
            {
                return  new List<DisplayDocumentViewModel>();
            }
        }

        /// <summary>
        /// GET api/entity/id
        /// HTTP GET with an id URL parameter for getting a single entitiy.
        /// </summary>
        /// <param name="id">The id of entity with type TEntitiyKey</param>
        /// <returns>A single object of type TEntity</returns>
        public DisplayDocumentViewModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST api/entity
        /// HTTP POST call to create a given entitiy
        /// </summary>
        /// <param name="value">The data describing ther entitiy based on the TEntitiy type</param>
        public void Post(DisplayDocumentViewModel value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PUT api/{entitiy}/{id}
        /// HTTP PUT call to update an entitiy with the given id
        /// </summary>
        /// <param name="id">The id of the parameter we are going to update</param>
        /// <param name="value">The information describing the new updated values for the entitiy with the given id</param>
        public void Put(Guid id, DisplayDocumentViewModel value)
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
