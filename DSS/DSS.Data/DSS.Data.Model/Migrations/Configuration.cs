using System.Data.Entity.Migrations;
using DSS.Data.Model.Context;
using DSS.Data.Model.DataSeeders;

namespace DSS.Data.Model.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DsContext context)
        {
            RoleSeeder.SeedInitialRoles(context);

            FeatureTierSeeder.SeedInitialFeatureTiers(context);

            UserSeeder.SeedInitialUsers(context);

            var categories = CategorySeeder.SeedInitialCategories(context);

            var keywords = KeywordSeeder.SeedInitialKeywords(context);

            DocumentSeeder.AddTestDocuments(context, categories, keywords);
        }
    }
}
