using System.ComponentModel;

namespace DSS.Common.Infrastructure.Web.Objects
{
    /// <summary>
    /// Json Object for returning structured json results
    /// </summary>
    public class JsonModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public JsonModel()
        {
            // set default properties

            Status = false;
            Data = null;
            Message = "";

            IsException = false;
            ExceptionMessage = "";
            ExceptionObject = null;
        }

        #endregion

        #region Data

        /// <summary>
        /// The data object returned with the json result
        /// </summary>
        public object Data { get; set; }
        
        #endregion
        
        #region General Status 

        /// <summary>
        /// The status of the request processing on the server side.
        /// </summary>
        public bool Status { get; set; }
        
        /// <summary>
        /// A message related to the server side processing of an ajax request.
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Exception Handling

        /// <summary>
        /// Flag indicating if the Json Model is holding an exception
        /// </summary>
        public bool IsException { get; set; }
        
        /// <summary>
        /// Specific exception related message
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Aditional exception related data.
        /// </summary>
        public object ExceptionObject { get; set; }

        #endregion
    }
}
