namespace DSS.BusinessLogic.Common.Utilities
{
    /// <summary>
    /// Class consisting of key values for configuration properties in the web configuration file
    /// </summary>
    public static class ConfigurationSettingKeys
    {
        /// <summary>
        /// The key for the folder where the files will be uploaded
        /// </summary>
        public const string FileUploadLocation = "MainServerFilesPath";

        /// <summary>
        /// The key for the file system location for the lucene index
        /// </summary>
        public const string LuceneIndexLocation = "LuceneIndexPath";

        /// <summary>
        /// The folder name for the lucene index
        /// </summary>
        public const string LuceneIndexFolderName = "LuceneIndexFolderName";
    }
}
