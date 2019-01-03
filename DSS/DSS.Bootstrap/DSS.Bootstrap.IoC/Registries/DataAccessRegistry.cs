using System.Data.Entity;
using DSS.Data.Access.Interfaces;
using DSS.Data.Access.Repositories;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using StructureMap.Configuration.DSL;

namespace DSS.Bootstrap.IoC.Registries
{
    public class DataAccessRegistry:Registry
    {
        public DataAccessRegistry()
        {
            For<IRepository<Category>>().Use<CategoryRepository>();
            For<IRepository<Keyword>>().Use<KeywordRepository>();
            For<IRepository<Document>>().Use<DocumentRepository>();

            For<IUserRepository>().Use<UserRepository>();

            // Get a single Db Shared Context
            For<DbContext>().HybridHttpOrThreadLocalScoped().Use<DsContext>();
        }
    }
}
