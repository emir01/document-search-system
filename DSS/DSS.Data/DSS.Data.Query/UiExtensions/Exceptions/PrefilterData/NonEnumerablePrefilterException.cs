using System;

namespace DSS.Data.Query.UiExtensions.Exceptions.PrefilterData
{
     /// <summary>
     /// The exception is thrown when the prefilter data object on the leaf nodes is expected to be 
     /// an Enumerable collection of objects.
     /// </summary>
    public class NonEnumerablePrefilterException : Exception
    {
        public NonEnumerablePrefilterException()
        {
        }

        public NonEnumerablePrefilterException(string message)
            : base(message)
        {
        }
    }
}
