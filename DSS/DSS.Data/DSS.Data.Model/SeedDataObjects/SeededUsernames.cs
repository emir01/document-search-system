namespace DSS.Data.Model.SeedDataObjects
{
    /// <summary>
    /// Represents a dictionary like object for seeded User Usernames, so we can reference them 
    /// in other seeder factories and/or configuration method.
    /// </summary>
    public static class SeededUsernames
    {
        /// <summary>
        /// Initially seeded username for the user with the admin role
        /// </summary>
        public static string AdminUser = "admin";

        /// <summary>
        /// The username for the user with the basic feature tier and the user role
        /// </summary>
        public static string TestBasic = "basic";

        /// <summary>
        /// The username for the user with the standard feature tier and the regular user role
        /// </summary>
        public static string TestStandard = "standard";

        /// <summary>
        /// The username for the user with the preimum feature set and the regular user role
        /// </summary>
        public static string TestPremium = "premium";
    }
}
