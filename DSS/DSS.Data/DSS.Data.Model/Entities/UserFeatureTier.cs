namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// Entities describing user functionality features, regarding document actions
    /// </summary>
    public class UserFeatureTier:BaseEntitiy
    {
        /// <summary>
        /// The Name for the feature tier that determines user feature availability
        /// </summary>
        public string TierName { get; set; }

        /// <summary>
        /// The alias used for design time reference to the tier
        /// </summary>
        public string TierAlias { get; set; }

        /// <summary>
        /// Short tier description used to shortly describe the feature tier
        /// </summary>
        public string TierDescription { get; set; }

        /// <summary>
        /// The document downloads available per day for the given tier
        /// </summary>
        public int DocumentDownloadsPerDay { get; set; }

        /// <summary>
        /// The document upvotes available per day for the given tier
        /// </summary>
        public int DocumentUpvotesPerDay { get; set; }

        /// <summary>
        /// The document  downvotes avaiable per day for the given tier
        /// </summary>
        public int DocumentDownvotesPerDay { get; set; }
    }
}