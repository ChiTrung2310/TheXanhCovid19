using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TheXanhCovid19
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
      new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] { "TheXanhCovid19.Controllers" }
            );

            routes.MapRoute(
               name: "Login",
               url: "dang-nhap",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
               namespaces: new[] { "TheXanhCovid19.Controllers" }
           );

            routes.MapRoute(
              name: "TinhThanh",
              url: "tinh-thanh",
              defaults: new { controller = "TinhThanh", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "TheXanhCovid19.Controllers" }
          );

            routes.MapRoute(
             name: "DuLieu",
             url: "du-lieu",
             defaults: new { controller = "DuLieu", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "TheXanhCovid19.Controllers" }
         );

            routes.MapRoute(
            name: "User",
            url: "them-moi",
            defaults: new { controller = "User", action = "UploadExcel", id = UrlParameter.Optional },
            namespaces: new[] { "TheXanhCovid19.Controllers" }
        );


           routes.MapRoute(
           name: "Information",
           url: "thong-tin",
           defaults: new { controller = "Information", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "TheXanhCovid19.Controllers" }
       );
        }
    }
}
