using DSS.Bootstrap.UserTracking.Interface;
using DSS.Bootstrap.UserTracking.Services;
using DSS.Bootstrap.Utilities;
using DSS.Bootstrap.Utilities.Interface;
using DSS.Bootstrap.Utilities.Json;
using DSS.Bootstrap.Utilities.Json.Interface;
using DSS.Common.Infrastructure.Web.JsonModelConstruction.Interfaces;
using StructureMap.Configuration.DSL;

namespace DSS.Bootstrap.IoC.Registries
{
    /// <summary>
    /// Basic registry for the web project
    /// </summary>
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<IServerFileUtility>().Use<ServerFileUtility>();

            For<IAuthenticator>().Use<WebFormsAuthenticator>();

            For<IDomainUserAdapter>().Use<DssTrackedUserAdapter>();

            For<IDssBaseResultJsonFactory>().Use<DssBaseResultJsonFactory>();

            For<IDssDataResultJsonFactory>().Use<DssDataResultJsonFactory>();
        }
    }
}