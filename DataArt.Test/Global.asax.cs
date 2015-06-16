using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Mvc;

namespace DataArt.Test
{
    public class MvcApplication : HttpApplication
    {
        private static IWindsorContainer _container;
        private static void BootstrapContainer()
        {
            _container = new WindsorContainer().Install(FromAssembly.This());
            IControllerFactory factory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(factory);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MvcApplication.BootstrapContainer();
        }
    }
}
