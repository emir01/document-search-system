using System;

namespace DSS.Data.Query.UiExtensions.Exceptions.PrefilterData
{
    /// <summary>
    /// Exception thrown when the data module is trying to access and use the value name property on a filter node
    /// when the value name property has not been set.
    /// </summary>
    public class PrefilterMisingValueNameException : Exception
    {
        public PrefilterMisingValueNameException()
        {
        }

        public PrefilterMisingValueNameException(string message)
            : base(message)
        {

        }
    }
}