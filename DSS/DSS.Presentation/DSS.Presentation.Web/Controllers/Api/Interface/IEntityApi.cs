using System.Collections.Generic;
using System.Web.Http;

namespace DSS.Presentation.Web.Controllers.Api.Interface
{
    /// <summary>
    /// Interface describing basic functionality for an API controller for a given any given entitiy.
    /// </summary>
    /// <typeparam name="TEntityKey">The type of the unique identifier for the entitiy.</typeparam>
    /// <typeparam name="TEntity">The actual type of the entitiy. </typeparam>
    public interface IEntityApi<TEntityKey, TEntity> where TEntityKey : struct
    {
        /// <summary>
        /// GET api/{entitiy}
        /// HTTP Get Call that returns an IEnumerable list of all the entities
        /// </summary>
        /// <returns>IEnumerable collection of the entitiy objects</returns>
        IEnumerable<TEntity> Get();

        /// <summary>
        /// GET api/entity/id
        /// HTTP GET with an id URL parameter for getting a single entitiy.
        /// </summary>
        /// <param name="id">The id of entity with type TEntitiyKey</param>
        /// <returns>A single object of type TEntity</returns>
        TEntity Get(TEntityKey id);

        /// <summary>
        /// POST api/entity
        /// HTTP POST call to create a given entitiy
        /// </summary>
        /// <param name="value">The data describing ther entitiy based on the TEntitiy type</param>
        void Post([FromBody] TEntity value);

        /// <summary>
        /// PUT api/{entitiy}/{id}
        /// HTTP PUT call to update an entitiy with the given id
        /// </summary>
        /// <param name="id">The id of the parameter we are going to update</param>
        /// <param name="value">The information describing the new updated values for the entitiy with the given id</param>
        void Put(TEntityKey id, [FromBody] TEntity value);

        /// <summary>
        /// DELETE api/{entitiy}/{id}
        /// HTTP DELETE call to delete the entitiy with the given id.
        /// </summary>
        /// <param name="id">The id of the entity we want to delete.</param>
        void Delete(TEntityKey id);
    }
}
