using Ruben.Books.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ruben.Books.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Timeline",
               url: "timeline",
               defaults: new { controller = "Reading", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "BooksList",
                url: "Books/Index/{filter}",
                defaults: new { controller = "Books", action = "Index", filter = new BookFilterVM { IsRead = false } });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Reading", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}