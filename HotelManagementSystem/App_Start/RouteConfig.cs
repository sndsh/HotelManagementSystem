using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HotelManagementSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                          name: "FEAccomdations",
                          url: "Accomodations",
                          defaults: new { area = "", controller = "Accomodations", action = "Index"},
                          namespaces: new[] { "HotelManagementSystem.Controllers" }
              );

            routes.MapRoute(
                         name: "AccomdationPackageDetails",
                         url: "accomodation-package/{accomodationPackageID}",
                         defaults: new { area = "", controller = "Accomodations", action = "Details" },
                         namespaces: new[] { "HotelManagementSystem.Controllers" }
             );

            routes.MapRoute(
                        name: "CheckAvailability",
                        url: "accomodation-check-availability",
                        defaults: new { area = "", controller = "Accomodations", action = "CheckAvailability" },
                        namespaces: new[] { "HotelManagementSystem.Controllers" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
