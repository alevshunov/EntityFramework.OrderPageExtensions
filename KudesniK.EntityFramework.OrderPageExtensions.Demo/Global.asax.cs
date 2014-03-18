using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using KudesniK.EntityFramework.OrderPageExtensions.Demo.Migrations;
using KudesniK.EntityFramework.OrderPageExtensions.Demo.Models;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Db, Configuration>());
            SetupSorting.Setup();
        }
    }
}
