using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.Categories;
using DSS.Presentation.Web.Controllers.Api.Interface;

namespace DSS.Presentation.Web.Controllers.Api
{
    public class CategoryController : ApiController, IEntityApi<Guid,DisplayCategoryViewModel>
    {
        #region Properties

        private readonly ICategoryService _categoryService;

        #endregion

        #region Constructor

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Actions

        /// <summary>
        /// GET api/{entitiy}
        /// HTTP Get Call that returns an IEnumerable list of all the entities
        /// </summary>
        /// <returns>IEnumerable collection of the entitiy objects</returns>
        public IEnumerable<DisplayCategoryViewModel> Get()
        {
            var result = _categoryService.GetAllCategories();

            if (result.Status == ResultStatus.Success)
            {
                var data = result.GetData();

                if (data != null)
                {
                    // transform the domain data to the view model data
                    var viewModelData = data.Select(Mapper.Map<DisplayCategoryViewModel>);
                    return viewModelData;
                }
                else
                {
                    return new List<DisplayCategoryViewModel>();
                }
            }
            else
            {
                return new List<DisplayCategoryViewModel>();
            }
        }

        /// <summary>
        /// GET api/entity/id
        /// HTTP GET with an id URL parameter for getting a single entitiy.
        /// </summary>
        /// <param name="id">The id of entity with type TEntitiyKey</param>
        /// <returns>A single object of type TEntity</returns>
        public DisplayCategoryViewModel Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// POST api/entity
        /// HTTP POST call to create a given entitiy
        /// </summary>
        /// <param name="value">The data describing ther entitiy based on the TEntitiy type</param>
        public void Post(DisplayCategoryViewModel value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// PUT api/{entitiy}/{id}
        /// HTTP PUT call to update an entitiy with the given id
        /// </summary>
        /// <param name="id">The id of the parameter we are going to update</param>
        /// <param name="value">The information describing the new updated values for the entitiy with the given id</param>
        public void Put(Guid id, DisplayCategoryViewModel value)
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
