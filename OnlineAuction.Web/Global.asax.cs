using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using OnlineAuction.BLL.Interfaces;
using OnlineAuction.BLL.Managers;
using OnlineAuction.DAL.Interfaces;
using OnlineAuction.DAL.Repositories;

namespace OnlineAuction
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = MakeBuilder();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private ContainerBuilder MakeBuilder()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(Manager<>)).As(typeof(IManager<>));

            builder.RegisterType<UserManager>().As<IUserManager>();
            builder.RegisterType<WinLotManager>().As<IWinLotManager>();
            
            return builder;
        }
    }
}
