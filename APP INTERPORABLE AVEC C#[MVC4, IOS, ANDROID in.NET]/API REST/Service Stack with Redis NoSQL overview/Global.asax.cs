using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CalarieEtSportMVC.Api;
using ServiceStack;
using ServiceStack.Redis;



namespace CalarieEtSportMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            new CalorieSportAppHost().Init();
        }
    }

    //ProtienTracker = CalorieSport
    public class CalorieSportAppHost : AppHostBase
    {
        public CalorieSportAppHost() : base("Calorie Sport web Services", typeof(UserService).Assembly) { }

        //configuration de service stack
        public override void Configure(Funq.Container container)
        {
            SetConfig(new HostConfig { HandlerFactoryPath = "api" });

            //configuration de redis
            container.Register<IRedisClientsManager>(c => new PooledRedisClientManager("127.0.0.1:6379"));

            container.Register<CalarieEtSportMVC.Api.IRepository>(c => new Repository(c.Resolve<IRedisClientsManager>()));
        }


    }
}