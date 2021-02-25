using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TrocCommunity.WebUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Catalogue",
              url: "Categories/Catalogue",
              defaults: new { controller = "Categories", action = "Catalogue"}
          );

            routes.MapRoute(
            name: "Categories",
            url: "Categories/Catalogue/{cat}/{category}",
            defaults: new { controller = "Categories", action = "Catalogue", category = UrlParameter.Optional, cat =UrlParameter.Optional}
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}