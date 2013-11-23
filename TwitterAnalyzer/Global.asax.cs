using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using TwitterAnalyzer.Controllers;
using TwitterAnalyzer.Models;

namespace TwitterAnalyzer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        void ConfigureApi(HttpConfiguration config)
        {
            var unity = new UnityContainer();
            unity.RegisterType<TweetAnalayzerController>();
            unity.RegisterType<IIntegrationService, IntegrationService>(new HierarchicalLifetimeManager());
            unity.RegisterType<ITweetRepository, TweetRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new IoCContainer(unity);
        }
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureApi(GlobalConfiguration.Configuration);
        }
    }
}
