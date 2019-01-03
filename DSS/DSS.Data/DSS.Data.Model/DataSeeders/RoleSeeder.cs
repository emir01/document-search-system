using System.Linq;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using DSS.Data.Model.SeedDataObjects;

namespace DSS.Data.Model.DataSeeders
{
    /// <summary>
    /// Contains Seed functionality for Code First, for database population
    /// with initial Roles
    /// </summary>
    public class RoleSeeder
    {
        /// <summary>
        /// Add the initial seed of roles used in the system on the given context
        /// </summary>
        /// <param name="context"></param>
        public static void SeedInitialRoles(DsContext context)
        {
            AddRoleToContext(context, "Administrator", SeededRoleAliases.Administrator,"The Administrators control and have access to management functionality for users and uploaded documents");
            AddRoleToContext(context, "User", SeededRoleAliases.User, "Regular application registered users that can perform actions regarding documents based on the set feature tier");
        }

        /// <summary>
        /// Add a single role to the system uniquely defined by the role alias, with the given role properties
        /// </summary>
        /// <param name="context"></param>
        /// <param name="roleTitle"></param>
        /// <param name="roleAlias"></param>
        /// <param name="roleDescription"></param>
        public static void AddRoleToContext(DsContext context, string roleTitle, string roleAlias, string roleDescription = "")
        {
            // Initially check if the role has been already added
            // only only add the role if its not been previously added
            // again based on the alias
            if (!context.Roles.Any(x => x.Alias == roleAlias))
            {
                var role = new Role();

                role.Alias = roleAlias;
                role.Title = roleTitle;
                role.Description = roleDescription;

                context.Roles.Add(role);

                context.SaveChanges();
            }
        }
    }
}
