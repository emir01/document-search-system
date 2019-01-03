using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DSS.Bootstrap.UserTracking.Interface;
using DSS.Bootstrap.UserTracking.Principals;
using DSS.Presentation.Web.App_Start;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StructureMap;

namespace DSS.Presentation.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MiniProfilerEF6.Initialize();

            InitProfilerSettings();

            AreaRegistration.RegisterAllAreas();

            BindingsConfig.RegisterCustomBinders(ModelBinders.Binders);

            WebApiConfig.Register(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfiguration.ConfigureAutoMapper();
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }

        protected void Application_OnAuthenticateRequest(Object sender, EventArgs e)
        {
            // Create the domain adapter
            var adapter = ObjectFactory.GetInstance<IDomainUserAdapter>();

            // Create the principal
            var authenticatedUserModelPrincipal = new AuthenticatedUserModelPrincipal {DomainUserAdapter = adapter};

            if (Context.User != null)
            {
                if (Context.User.Identity.IsAuthenticated)
                {
                    authenticatedUserModelPrincipal.DomainUserAdapter.GetAdaptedDomainUser(Context.User.Identity.Name);
                }
            }
            else
            {
                // If context user is not defined we will populate the adapter model with an empty name
                authenticatedUserModelPrincipal.DomainUserAdapter.GetAdaptedDomainUser("");
            }

            // Set Current Principal and Context.User information
            System.Threading.Thread.CurrentPrincipal = Context.User = authenticatedUserModelPrincipal;
        }


        #region Mini Profiler Settings

        /// <summary>
        /// Customize aspects of the MiniProfiler.
        /// </summary>
        private void InitProfilerSettings()
        {
            // a powerful feature of the MiniProfiler is the ability to share links to results with other developers.
            // by default, however, long-term result caching is done in HttpRuntime.Cache, which is very volatile.
            // 
            // let's rig up serialization of our profiler results to a database, so they survive app restarts.
            //MiniProfiler.Settings.Storage = new Helpers.SqliteMiniProfilerStorage(ConnectionString);

            // different RDBMS have different ways of declaring sql parameters - SQLite can understand inline sql parameters just fine
            // by default, sql parameters won't be displayed
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();

            // these settings are optional and all have defaults, any matching setting specified in .RenderIncludes() will
            // override the application-wide defaults specified here, for example if you had both:
            //    MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Right;
            //    and in the page:
            //    @MiniProfiler.RenderIncludes(position: RenderPosition.Left)
            // then the position would be on the left that that page, and on the right (the app default) for anywhere that doesn't
            // specified position in the .RenderIncludes() call.
            MiniProfiler.Settings.PopupRenderPosition = RenderPosition.Left; // defaults to left
            MiniProfiler.Settings.PopupMaxTracesToShow = 10;                  // defaults to 15
            
            // optional settings to control the stack trace output in the details pane
            // the exclude methods are not thread safe, so be sure to only call these once per appdomain
            MiniProfiler.Settings.ExcludeType("SessionFactory"); // Ignore any class with the name of SessionFactory
            MiniProfiler.Settings.ExcludeAssembly("NHibernate"); // Ignore any assembly named NHibernate
            MiniProfiler.Settings.ExcludeMethod("Flush");        // Ignore any method with the name of Flush
            // MiniProfiler.Settings.ShowControls = true;
            MiniProfiler.Settings.StackMaxLength = 256;          // default is 120 characters

        }

        #endregion
    }
}