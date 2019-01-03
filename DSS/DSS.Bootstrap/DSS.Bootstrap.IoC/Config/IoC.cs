using DSS.Common.Infrastructure.Web.JsonModelConstruction;
using DSS.Common.Infrastructure.Web.JsonModelConstruction.Interfaces;
using DSS.Common.Infrastructure.Web.Objects;
using StructureMap;

namespace DSS.Bootstrap.IoC.Config
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x => x.Scan(scan =>
                                                     {
                                                         scan.TheCallingAssembly();

                                                         scan.LookForRegistries();

                                                         scan.WithDefaultConventions();

                                                         scan.AssemblyContainingType<JsonModel>();

                                                         scan.AddAllTypesOf(typeof(IBaseJsonModelFactory<>));

                                                         scan.AddAllTypesOf(typeof(IBaseJsonModelFactory<>));

                                                         scan.ConnectImplementationsToTypesClosing(typeof(IBaseJsonModelFactory<>));
                                                     }
                                              ));
            return ObjectFactory.Container;
        }
    }
}