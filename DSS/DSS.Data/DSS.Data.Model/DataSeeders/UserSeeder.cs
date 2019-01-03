using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using DSS.Data.Model.SeedDataObjects;

namespace DSS.Data.Model.DataSeeders
{
    /// <summary>
    /// User data seeder used to create users either during the database creation process
    /// </summary>
    public class UserSeeder
    {

        /// <summary>
        /// Seeds the initial system users, including the Administrator and the Users for each of the 
        /// tiers
        /// </summary>
        /// <param name="context"></param>
        public static void SeedInitialUsers(DsContext context)
        {
            // Add the administrator user with the admin role
            AddUser(context, SeededUsernames.AdminUser, "developer", "admin@dss.com.mk", SeededTierAliases.PremiumTier, SeededRoleAliases.Administrator, SeededRoleAliases.User);

            // Add the basic user with the user role and the basic feature tier
            AddUser(context, SeededUsernames.TestBasic, "developer", "basic@dss.com.mk", SeededTierAliases.BasicTier, SeededRoleAliases.User);

            // Add the standard user with the user role and the standard feature tier
            AddUser(context, SeededUsernames.TestStandard, "developer", "standard@dss.com.mk", SeededTierAliases.StandardTier, SeededRoleAliases.User);

            // Add the premium user with the user role and the premium feature tier
            AddUser(context, SeededUsernames.TestPremium, "developer", "premium@dss.com.mk", SeededTierAliases.PremiumTier, SeededRoleAliases.User);
        }

        /// <summary>
        /// Add a User Entitiy with the given user properties
        /// </summary>
        /// <param name="context">The context to which we will be adding User Data </param>
        /// <param name="username">The username for the created User</param>
        /// <param name="password">The password for the created User</param>
        /// <param name="email">The email for the created User</param>
        /// <param name="featureTierAlias">The alias for the feature tier that will be linked with the user.</param>
        /// <param name="roleAliases">The collection of role aliases for the roles that will be linked with the user.</param>
        public static void AddUser(DsContext context, string username, string password, string email, string featureTierAlias = "", params string[] roleAliases)
        {
            // only add the user if there is no such user based on the username
            if (context.Users.FirstOrDefault(x => x.Username == username) == null)
            {
                var newUser = new User()
                {
                    Username = username,
                    Password = password,
                    Email = email
                };

                // Add the feature tier based on the alias if such tier exsists
                // Note the tier must be created before atempting to link it to the User
                var tier = context.UserFeatureTiers.FirstOrDefault(x => x.TierAlias == featureTierAlias);

                if (tier != null)
                {
                    newUser.UserFeatureTier = tier;
                }

                // Link users to existing  roles based on the aliases if the role exists
                newUser.UserRoles = new List<Role>();

                foreach (var roleAlias in roleAliases)
                {
                    var role = context.Roles.FirstOrDefault(x => x.Alias == roleAlias);

                    if (role != null)
                    {
                        newUser.UserRoles.Add(role);
                    }
                }

                // add the user to the context
                context.Users.AddOrUpdate(newUser);

                // Save the user changes after adding each user entitiy
                context.SaveChanges();
            }
        }
    }
}
