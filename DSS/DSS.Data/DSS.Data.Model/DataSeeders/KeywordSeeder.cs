using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;

namespace DSS.Data.Model.DataSeeders
{
    public class KeywordSeeder
    {
        private static List<Keyword> _internalSeededCategories = new List<Keyword>();

        public static List<Keyword> SeedInitialKeywords(DsContext context)
        {
            SeedKeyword(context, "word", "word");
            SeedKeyword(context, "app", "app");
            SeedKeyword(context, "web", "web");
            SeedKeyword(context, "desktop", "desktop");
            SeedKeyword(context, "api", "api");
            SeedKeyword(context, "document", "document");
            SeedKeyword(context, "indexing", "indexing");

            return _internalSeededCategories;
        }

        /// <summary>
        /// Seed a single keyword with the given display name and keyword alias
        /// </summary>
        /// <param name="context"></param>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        public static void SeedKeyword(DsContext context, string name, string alias)
        {
            // only add the category if there is no previousy isnerted category
            // based on the categoru alias

            if (!context.Keywords.Any(x => x.Alias == alias))
            {
                var newKeyword = new Keyword()
                {
                    Alias = alias,
                    Name = name
                };

                context.Keywords.AddOrUpdate(newKeyword);

                _internalSeededCategories.Add(newKeyword);
            }
        }
    }
}
