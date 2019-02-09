using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Trims",
                url: "Trims/{action}/{id}",
                defaults: new { controller = "Trims", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PoEntry",
                url: "PoEntry/{action}/{id}",
                defaults: new { controller = "PoEntry", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProductDetails",
                url: "ProductDetails/{action}/{id}",
                defaults: new { controller = "ProductDetails", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BOM",
                url: "BOM/{action}/{id}",
                defaults: new { controller = "BOM", action = "BOMEntry", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "BuyerEnquiry",
                url: "BuyerEnquiry/{action}/{id}",
                defaults: new { controller = "BuyerEnquiry", action = "GetBuyerEnquiryRecord", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TechpackFileUpload",
                url: "TechpackFileUpload/{action}/{id}",
                defaults: new { controller = "TechpackFileUpload", action = "TechpackFileUpload", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
