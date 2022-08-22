using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ecommerce_TVHB
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Product Details",
                "san-pham/{metatitle}-{id}",
                new { controller = "Product", action = "Details", id = UrlParameter.Optional },
                new[] { "Ecommerce_TVHB.Controllers" }
            );
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Ecommerce_TVHB.Controllers" }
            );
        }
    }
}
