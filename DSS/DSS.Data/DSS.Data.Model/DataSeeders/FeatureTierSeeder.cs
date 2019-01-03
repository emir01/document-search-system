using System.Linq;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using DSS.Data.Model.SeedDataObjects;

namespace DSS.Data.Model.DataSeeders
{
    /// <summary>
    /// Feature tier data seeder usable for seeding initial feature tiers for users
    /// </summary>
    public class FeatureTierSeeder
    {
        /// <summary>
        /// Add the initial feature tiers to the system
        /// </summary>
        /// <param name="dsContext"></param>
        public static void SeedInitialFeatureTiers(DsContext dsContext)
        {
            AddFeatureTier(dsContext, 
                "Basic Feature Tier", 
                SeededTierAliases.BasicTier, 
                "The Basic Feature tier has the lowes counts for document downloads and votes for each user", 
                4, 
                4, 
                4);

            AddFeatureTier(dsContext,
                "Standard Feature Tier",
                SeededTierAliases.StandardTier,
                "The Standard Feature Tier is the medium tier, providing a higher document and voting count",
                15,
                15,
                15);

            AddFeatureTier(dsContext,
                "Premium Feature Tier",
                SeededTierAliases.PremiumTier,
                "The Premium Tier provides the highest count of downloads and document activity per day",
                30,
                30,
                30);
        }

        /// <summary>
        /// Add a single Feature Tier with the given properties.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tierName"></param>
        /// <param name="tierAlias"></param>
        /// <param name="tierDescription"></param>
        /// <param name="downloadsPerDay"> </param>
        /// <param name="upvotesPerDay"> </param>
        /// <param name="downvotesPerDay"> </param>
        public static void AddFeatureTier(DsContext context, string tierName, string tierAlias, string tierDescription = "", int downloadsPerDay = 5, int upvotesPerDay = 5, int downvotesPerDay = 5)
        {
            // we will only add a tier if its not been added before
            // based on the tier alias

            if (!context.UserFeatureTiers.Any(x => x.TierAlias == tierAlias))
            {
                var userFeatureTier = new UserFeatureTier();

                userFeatureTier.TierAlias = tierAlias;
                userFeatureTier.TierDescription = tierDescription;
                userFeatureTier.TierName = tierName;

                userFeatureTier.DocumentDownloadsPerDay = downloadsPerDay;
                userFeatureTier.DocumentUpvotesPerDay = upvotesPerDay;
                userFeatureTier.DocumentDownvotesPerDay = downvotesPerDay;

                context.UserFeatureTiers.Add(userFeatureTier);

                context.SaveChanges();
            }
        }
    }
}
