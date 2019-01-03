using System;

namespace DSS.Data.Query.UiExtensions.Exceptions.PrefilterData
{
    /// <summary>
    /// The exception is thrown when the value property with the set name is not found
    /// on the prefilter data object
    /// </summary>
    public class PrefilterValuePropertyNotFoundException : Exception
    {
        public PrefilterValuePropertyNotFoundException()
        {
        }

        public PrefilterValuePropertyNotFoundException(string message)
            : base(message)
        {
        }
    }
}