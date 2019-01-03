namespace DSS.BusinessLogic.Objects.OperationResults
{
    /// <summary>
    /// The base data result object used to return BL results that contain data.
    /// </summary>
    /// <typeparam name="T">The type of the data object wrapped by the result object. </typeparam>
    public class DataResult<T> : BaseResult
    {

        #region Properties

        /// <summary>
        /// The data property containing the data wrapped by the data result.
        /// </summary>
        private T _data;

        #endregion

        #region Constructor

        public DataResult() : base()
        {
            // For reference types : null
            // For value types : default value for the type as expected
            _data = default(T);
        }
   
        #endregion

        #region Public Methods

        /// <summary>
        /// Retreves the data object wrapped by the data result.
        /// </summary>
        /// <returns></returns>
        public T GetData()
        {
            return _data;
        }

        /// <summary>
        /// Sets the wrapped data object.
        /// </summary>
        /// <param name="data">The client set data object.</param>
        public void SetData(T data)
        {
            _data = data;
        }

        #endregion
    }
}
