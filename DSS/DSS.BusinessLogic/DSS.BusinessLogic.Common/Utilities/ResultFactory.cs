
using DSS.BusinessLogic.Objects.OperationResults;

namespace DSS.BusinessLogic.Common.Utilities
{
    /// <summary>
    /// A factory class used to create result objects
    /// </summary>
    public static class ResultFactory
    {
        public static BaseResult GetResultObject()
        {
            return new BaseResult();
        }
        
        public static DataResult<T> GetDataResult<T>()
        {
            return  new DataResult<T>();
        }
    }
}
