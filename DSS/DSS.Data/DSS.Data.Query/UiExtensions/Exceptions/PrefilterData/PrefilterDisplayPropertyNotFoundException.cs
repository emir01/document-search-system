using System;

namespace DSS.Data.Query.UiExtensions.Exceptions.PrefilterData
{
    /// <summary>
    /// The exception is thrown when the display property with the set name is not found
    /// on the prefilter data object
    /// </summary>
    public class PrefilterDisplayPropertyNotFoundException : Exception
    {
        public PrefilterDisplayPropertyNotFoundException()
        {
        }

        public PrefilterDisplayPropertyNotFoundException(string message)
            : base(message)
        {
        }
    }
}