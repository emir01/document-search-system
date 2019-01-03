using System;
using DSS.Lucene.Common.Entities.Enums;

namespace DSS.Lucene.Common.Entities
{
    /// <summary>
    /// DTO used to return Lucene Indexing Services Query/Action Results
    /// </summary>
    public class IndexQueryResult<T>
    {
        #region Properties

        /// <summary>
        /// Response object regarding the Action the user performed on the index
        /// </summary>
        public T ResponseObject;

        /// <summary>
        /// A boolean flag indicating the actual status of the operation and wether it failed or succeeded.
        /// </summary>
        /// <remarks> Do not use to directly access the status, instead use the GetStatusFunction</remarks>
        protected LuceneIndexingStatus ActionStatus;

        /// <summary>
        /// A message asosiated with the result of the lucene indexing operation
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// An exception object containing the exception if any occured during the lucene action.
        /// </summary>
        public Exception Exception { get; set; }

        #endregion

        #region Actions

        public void SetException(Exception ex, string message = "")
        {
            Exception = ex;
            ActionStatus = LuceneIndexingStatus.Exception;
            Message = message;
        }

        public void SetSuccess(string message = "", T responseObject = default(T))
        {
            Message = message;
            ResponseObject = responseObject;
            ActionStatus = LuceneIndexingStatus.Success;
        }

        public void SetFailiure(string message = "")
        {
            ActionStatus = LuceneIndexingStatus.Failed;
            Message = message;
        }

        /// <summary>
        /// Expose and use this as the Status indicator might change in the future
        /// </summary>
        /// <returns></returns>
        public LuceneIndexingStatus GetStatus()
        {
            return ActionStatus;
        }

        #endregion
    }
}
