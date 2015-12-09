using System.Web.Mvc;
using System.Web.Routing;

namespace CSC340_ordering_sytem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /** Login/Logout Routes **/
            routes.MapRoute("Authentication", "account/{action}",
                new {controller = "Authentication", action = "Login|Logout"},
                new { action = @"Login|Logout"});

            /** Registration Route **/
            routes.MapRoute("Register", "account/register", new {controller = "Users", action = "Create"});

            /** Admin Customer listing page **/
            routes.MapRoute("AdminListCustomers", "admin/customers",
                new { controller = "Users", action = "Index" });

            /** Admin Customer edit page **/
            routes.MapRoute("AdminEditCustomer", "admin/customers/edit/{id}",
                new { controller = "Users", action = "AdminEditCustomer" });

            /** Admin Customer delete page **/
            routes.MapRoute("AdminDeleteCustomer", "admin/customers/delete/{id}",
                new { controller = "Users", action = "Delete" });

            /** Admin List Menu Categories **/
            routes.MapRoute("AdminMenu", "admin/menu",
                new { controller = "Categories", action = "Index", id = UrlParameter.Optional }
            );

            /** Admin List Menu Categories **/
            routes.MapRoute("AdminMenuCategories", "admin/menu/category/{action}/{id}",
                new { controller = "Categories", action = "Index", id = UrlParameter.Optional }
            );

            /** Admin list items in category **/
            routes.MapRoute("AdminMenuCategoryItems", "admin/menu/{slug}",
                new { controller = "MenuItems", action = "Index" },
                new { action = "Index" }
            );

            /** Admin Manage Menu Items **/
            routes.MapRoute("AdminMenuItems", "admin/menu/item/{action}/{id}",
                new {controller = "MenuItems", action = "Index", id = UrlParameter.Optional}
            );

            /** Admin Orders **/
            routes.MapRoute("AdminOrders", "admin/orders/{action}/{id}",
                new { controller = "AdminOrders", action = "ListOrders", id = UrlParameter.Optional }
            );

            /** Customer account overview page **/
            routes.MapRoute("CustomerAccountOverview", "account",
                new { controller = "Users", action = "CustomerAccountOverview" });

            /** Customer edit page **/
            routes.MapRoute("CustomerEditAccount", "account/edit", new { controller = "Users", action = "Edit" });

            /** Address routes **/
            routes.MapRoute("Addresses", "account/addresses/{action}/{id}", new { controller = "Addresses", action = "Index", id = UrlParameter.Optional });

            /** Credit card routes **/
            routes.MapRoute("CreditCards", "account/credit-cards/{action}/{id}", new { controller = "CreditCards", action = "Index", id = UrlParameter.Optional });

            /** Customer Categories List **/
            routes.MapRoute("Menu", "menu", new { controller = "FrontMenu", action = "ListCategories" });

            /** List products in category **/
            routes.MapRoute("MenuCategoryProducts", "menu/{slug}", new { controller = "FrontMenu", action = "CategoryItems" });

            /** Customer Cart **/
            routes.MapRoute("CustomerCart", "cart/{action}/{id}",
                new {controller = "Carts", action = "ListCartItems", id = UrlParameter.Optional}
            );

            /** Customer Order **/
            routes.MapRoute("CustomerCheckout", "checkout/{action}",
                new {controller = "FrontOrders", action = "Checkout"}
            );

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}