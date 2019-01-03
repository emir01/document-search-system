using System;

namespace DSS.Common.Exceptions.DB
{
    public class OperationOverNonExistingEntity:ApplicationException
    {
        public OperationOverNonExistingEntity()
        {}

        public OperationOverNonExistingEntity(string message) : base(message)
        {
        }
    }
}
