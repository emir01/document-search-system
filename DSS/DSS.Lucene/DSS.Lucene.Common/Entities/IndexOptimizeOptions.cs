namespace DSS.Lucene.Common.Entities
{
    /// <summary>
    /// An instance of optimization options for index optimization calls
    /// </summary>
    public class IndexOptimizeOptions
    {
        public bool? DoWait { get; set; }

        public int? MaxNumberOfSegments { get; set; }
    }
}
