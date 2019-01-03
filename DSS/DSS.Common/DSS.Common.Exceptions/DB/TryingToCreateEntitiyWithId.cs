using System;

namespace DSS.Common.Exceptions.DB
{
    /// <summary>
    /// Exception thrown when trying to create an entitiy
    /// with an object that has an id already set.
    /// </summary>
    public class TryingToCreateEntitiyWithId : ApplicationException
    {
        public TryingToCreateEntitiyWithId()
        {
        }

        public TryingToCreateEntitiyWithId(string message)
            : base(message)
        {
        }
    }
}
