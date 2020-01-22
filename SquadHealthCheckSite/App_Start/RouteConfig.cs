using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SquadHealthCheck
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Join",
                url: "Join/{id}",
                defaults: new { controller = "Home", action = "Join"}
            );

            routes.MapRoute(
               name: "Leave",
               url: "Leave/{id}",
               defaults: new { controller = "Home", action = "Leave" }
           );

            routes.MapRoute(
                name: "Setvalue",
                url: "SetUserValue/{squadid}/{itemid}/{value}",
                defaults: new { controller = "Home", action = "SetUserValue" }
            );

            routes.MapRoute(
                name: "AddItem",
                url: "ManageSquad/AddItem/{squadId}/{itemId}",
                defaults: new { controller = "ManageSquad", action = "AddItem" }
            );

            routes.MapRoute(
               name: "RemoveItem",
               url: "ManageSquad/RemoveItem/{squadId}/{itemId}",
               defaults: new { controller = "ManageSquad", action = "RemoveItem" }
           );

            routes.MapRoute(
               name: "Default3",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );



           

        }
    }
}
