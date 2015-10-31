using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SheepShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Sheep_Shop_Order",
                url: "sheep_shop/order/{id}",
                defaults: new { controller = "sheep_shop", action = "order", id = UrlParameter.Optional, milk = UrlParameter.Optional, skins = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Sheep_Shop_Stock",
                url: "sheep_shop/stock/{id}",
                defaults: new { controller = "sheep_shop", action = "stock", id = UrlParameter.Optional, milk = UrlParameter.Optional, skins = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "Sheep_Shop_Herd",
                url: "sheep_shop/herd/{id}",
                defaults: new { controller = "sheep_shop", action = "herd", id = UrlParameter.Optional, milk = UrlParameter.Optional, skins = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "sheep_shop", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
