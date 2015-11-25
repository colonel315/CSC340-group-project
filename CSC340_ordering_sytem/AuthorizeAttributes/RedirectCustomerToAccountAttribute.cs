using System.Web.Mvc;
using System.Web.Routing;

namespace CSC340_ordering_sytem.AuthorizeAttributes
{
    public class RedirectCustomerToAccountAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
            {
                { "controller", "Users" },
                { "action", "CustomerAccountOverview" }
            });
        }
    }
}