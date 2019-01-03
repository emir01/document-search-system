namespace DSS.Data.Query.Enums
{
    /// <summary>
    /// sDetermines the general data type defined by a given filter node
    /// </summary>
    public enum FilterDataType
    {
        // Filter String values
        String,
        
        // Filter any whore number value
        Integer,
        
        // Filter decimal values
        Decimal,

        // Filter reference id values, FKs
        SimpleDropdown,

        // Filter a date value
        Date,
    }
}
