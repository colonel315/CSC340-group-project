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

            /** Customer account overview page **/
            routes.MapRoute("CustomerAccountOverview", "account",
                new { controller = "Users", action = "CustomerAccountOverview" });

            /** Customer edit page **/
            routes.MapRoute("CustomerEditAccount", "account/edit", new { controller = "Users", action = "Edit" });

            /** Address routes **/
            routes.MapRoute("Addresses", "account/addresses/{action}/{id}", new { controller = "Addresses", action = "Index", id = UrlParameter.Optional });

            /** Credit card routes **/
            routes.MapRoute("CreditCards", "account/credit-cards/{action}/{id}", new { controller = "CreditCards", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
