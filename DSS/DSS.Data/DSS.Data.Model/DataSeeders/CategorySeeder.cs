using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;

namespace DSS.Data.Model.DataSeeders
{
    /// <summary>
    /// Utility EF Code first class used for seeding inintial document category data.
    /// </summary>
    public class CategorySeeder
    {
        private static  List<Category> _internalSeededCategories = new List<Category>();

        /// <summary>
        /// Seed initial document categories used for categorizing uploaded documents.
        /// </summary>
        /// <param name="context"></param>
        public static List<Category> SeedInitialCategories(DsContext context)
        {
            SeedCategory(context, "Web", "web");
            
            SeedCategory(context, "Graduation Paper", "graduationPaper");
            
            SeedCategory(context, "Databases", "databases");
            
            SeedCategory(context, "Games", "games");
            
            SeedCategory(context, "Educational", "educational");
            
            SeedCategory(context, "Desktop", "desktop");
            
            SeedCategory(context, "Mobile", "mobile");
            
            SeedCategory(context, "Design", "design");
            
            SeedCategory(context, "AI", "ai");

            SeedCategory(context, "Compilers", "compilers");

            SeedCategory(context, "Probablility", "probability");

            SeedCategory(context, "Software Design", "softwareDesign");

            SeedCategory(context, "Design Patterns", "designPatterns");

            SeedCategory(context, "Algebra", "algebra");

            SeedCategory(context, "Java", "java");
            
            SeedCategory(context, "ASP.NET", "aspNet");
            
            SeedCategory(context, "MVC", "mvc");
            
            SeedCategory(context, "Javascript", "javascript");
            
            SeedCategory(context, "Front-End", "frontEnd");
            
            SeedCategory(context, "Back-End", "backEnd");

            return _internalSeededCategories;

        }

        /// <summary>
        /// Seed a single category with the given display name and category alias
        /// </summary>
        /// <param name="context"></param>
        /// <param name="categoryDisplayName"></param>
        /// <param name="categoryAlias"></param>
        public static void SeedCategory(DsContext context, string categoryDisplayName, string categoryAlias)
        {
            // only add the category if there is no previousy isnerted category
            // based on the categoru alias

            if (!context.Categories.Any(x => x.Alias == categoryAlias))
            {
                var newCategory = new Category()
                                      {
                                          Alias = categoryAlias,
                                          Name = categoryDisplayName
                                      };
                
                context.Categories.AddOrUpdate(newCategory);
                _internalSeededCategories.Add(newCategory);}
        }
    }
}
