using System;

namespace DSS.Data.Query.UiExtensions.Exceptions.PrefilterData
{
    /// <summary>
    /// Exception thrown when the data module is trying to access and use the display name property on a filter node
    /// when the display name property has not been set.
    /// </summary>
    public class PrefilterMisingDisplayNameException:Exception
    {
        public PrefilterMisingDisplayNameException()
        {
        }

        public PrefilterMisingDisplayNameException(string message) : base(message)
        {
        }
    }
}