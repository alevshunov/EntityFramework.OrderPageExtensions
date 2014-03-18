using System.Web.Mvc;
using System.Web.Routing;

namespace KudesniK.EntityFramework.OrderPageExtensions.Demo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(name: "Countries", url: "{order}/{direction}/page/{page}",
                defaults: new
                {
                    controller = "Home",
                    action = "Countries",
                    order = UrlParameter.Optional,
                    direction = UrlParameter.Optional,
                    page = UrlParameter.Optional
                }
                );


            routes.MapRoute(name: "Default", url: "{*url}",
                defaults: new
                {
                    controller = "Home",
                    action = "Countries",
                    order = UrlParameter.Optional,
                    direction = UrlParameter.Optional,
                    page = UrlParameter.Optional
                }
                );

        }
    }
}
