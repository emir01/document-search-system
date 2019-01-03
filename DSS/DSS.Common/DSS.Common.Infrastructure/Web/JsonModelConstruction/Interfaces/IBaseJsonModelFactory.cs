using System;
using DSS.Common.Infrastructure.Web.Objects;

namespace DSS.Common.Infrastructure.Web.JsonModelConstruction.Interfaces
{
    /// <summary>
    /// Define an interface for a factory class for building web Json Model objects from business reusult objects
    /// </summary>
    public interface IBaseJsonModelFactory<T>
    {
        /// <summary>
        /// Defines the most generic method of building Json Model object by setting all the
        /// JSON Model properties individually. 
        /// 
        /// The properties are set using callback functions from client code.
        /// 
        /// Some of the callback functions are optional as they are used to set non critical Json Model Properties
        /// </summary>
        /// <param name="businessResult">The business result object containing all the information including data to build the Json Model object</param>
        /// <param name="getStatus">Should return the True/False status of the business result object</param>
        /// <param name="getMessage">Should return the base message from the Json Result object </param>
        /// <param name="getData">Should return the data to be serialized and passed to the client in Json Format</param>
        /// <param name="getIsException">Should return if the business resulkt returned with an exception result</param>
        /// <param name="getExceptionMessage">Should return the base exception message</param>
        /// <param name="getExceptionObject">Should return the possible optional exception object</param>
        /// <returns></returns>
        JsonModel Build(T businessResult,
            Func<T, bool> getStatus = null,
            Func<T, string> getMessage = null,
            Func<T, object> getData = null,
            Func<T, bool> getIsException = null,
            Func<T, string> getExceptionMessage = null,
            Func<T, object> getExceptionObject = null
            );
    }
}
