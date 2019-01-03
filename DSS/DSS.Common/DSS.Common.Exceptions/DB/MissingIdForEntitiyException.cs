using System;

namespace DSS.Common.Exceptions.DB
{
    /// <summary>
    /// Exception when trying to perform database access or operation
    /// with an entitiy that is missing the id value. The operation requires
    /// the entitiy have an id.
    /// </summary>
    public class MissingIdForEntitiyException:ApplicationException
    {
        public MissingIdForEntitiyException()
        {
        }

        public MissingIdForEntitiyException(string message):base(message)
        {
        }
    }
}
