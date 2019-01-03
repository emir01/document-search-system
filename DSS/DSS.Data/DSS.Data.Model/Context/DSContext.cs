using System.Data.Entity;
using DSS.Data.Model.Entities;

namespace DSS.Data.Model.Context
{
    public class DsContext : DbContext
    {
        public DsContext()
            : base("Name=DSContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// The DB Context set for the User entitiy.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// The DB Context set for the Category entitiy.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// The DB Context set for the Docuemnt entitiy.
        /// </summary>
        public DbSet<Document> Documents { get; set; }

        /// <summary>
        /// The DB Context set for the Keyword entitiy.
        /// </summary>
        public DbSet<Keyword> Keywords { get; set; }

        /// <summary>
        /// The Db Context set for the Roles
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// The db Context set for the User Feature Tiers
        /// </summary>
        public DbSet<UserFeatureTier> UserFeatureTiers { get; set; }

        /// <summary>
        /// The db Context set for the Download Logs
        /// </summary>
        public DbSet<DownloadLog> DownloadLogs { get; set; }

        /// <summary>
        /// The db Context set for the Upvote Logs
        /// </summary>
        public DbSet<UpvoteLog> UpvoteLogs { get; set; }

        /// <summary>
        /// The db context set for the downvote logs
        /// </summary>
        public DbSet<DownvoteLog> DownvoteLogs { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        ///                 before the model has been locked down and used to initialize the context.  The default
        ///                 implementation of this method does nothing, but it can be overridden in a derived class
        ///                 such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        ///                 is created.  The model for that context is then cached and is for all further instances of
        ///                 the context in the app domain.  This caching can be disabled by setting the ModelCaching
        ///                 property on the given ModelBuidler, but note that this can seriously degrade performance.
        ///                 More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        ///                 classes directly.
        /// </remarks>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map the document
            modelBuilder.Entity<Document>().HasMany(ent => ent.Categories)
                .WithMany()
                .Map(conf =>
                     {
                         conf.ToTable("CategoriesForDocuments");
                         conf.MapLeftKey("CategoryId");
                         conf.MapRightKey("DocumentId");
                     });

            modelBuilder.Entity<Document>().HasMany(ent => ent.Keywords)
                .WithMany()
                .Map(conf =>
                {
                    conf.ToTable("KeywordsForDocuments");
                    conf.MapLeftKey("KeywordId");
                    conf.MapRightKey("DocumentId");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
