using AutoMapper;
using CommonComponents.DTOs;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        internal static MapperConfiguration MapperConfiguration { get; private set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //instanciamos el metodo construido en app_start.unityconfig para inyectar las dependencias
            UnityConfig.RegisterDependencies();

            //Automapper
            MapperConfiguration = MapperConfig.Configuration();
        }
    }
}
