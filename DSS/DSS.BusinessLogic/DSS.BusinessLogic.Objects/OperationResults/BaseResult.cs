using System;
using DSS.BusinessLogic.Objects.Enums;

namespace DSS.BusinessLogic.Objects.OperationResults
{
    public class BaseResult
    {
        #region Properties

        /// <summary>
        ///  The Status asociated with the result
        /// </summary>
        public ResultStatus Status { get; set; }

        /// <summary>
        ///  If an exception occurs this property will contain the exception object
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// The message related to the Base Result
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Constructor

        public BaseResult()
        {
            Status = ResultStatus.InitialNotSet;
        }

        #endregion

        #region Setters

        public virtual void SetSuccess(string message = null)
        {
            Status = ResultStatus.Success;

            if (message != null)
            {
                Message = message;
            }
        }

        public virtual void SetException(Exception ex, string message = null)
        {
            Status = ResultStatus.Exception;
            _exception = ex;

            if (message != null)
            {
                Message = message;
            }
        }

        public virtual void SetFailiure(string message = null)
        {
            Status = ResultStatus.Failiure;

            if (message != null)
            {
                Message = message;
            }
        }

        public virtual void SetMessage(string message)
        {
            Message = message;
        }

        #endregion

        #region Getters

        public virtual Exception GetException()
        {
            return _exception;
        }

        public virtual string GetMessage()
        {
            return Message;
        }

        #endregion

    }
}
