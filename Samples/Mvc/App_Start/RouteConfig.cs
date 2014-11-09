using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vsxtend.Samples.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = 0 }
            );

            //routes.MapRoute(
            //    name: "TeamRoomDefault",
            //    url: "teamroom/{action}/{id}",
            //    defaults: new { controller = "TeamRoom", action = "Index", id = 0 }
            //);

        }
    }
}