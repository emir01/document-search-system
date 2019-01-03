namespace DSS.BusinessLogic.Objects.Enums
{
    public enum ResultStatus
    {
        // Operation completed as expected
        // Found records. Updated record
        Success, 
        
        // Operation faled to complete as expected
        // No record found. Failed to update a record because of smoe BL reasons.
        Failiure,
        
        // Exception occured  while performing the operation
        // Could not conect to database. Null exception etc
        Exception,
        
        // No changes since the result object has been created
        InitialNotSet
    }
}