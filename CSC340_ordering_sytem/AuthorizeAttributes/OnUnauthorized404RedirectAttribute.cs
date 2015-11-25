using System.Web.Mvc;

namespace CSC340_ordering_sytem.AuthorizeAttributes
{
    public class OnUnauthorized404RedirectAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/ServerResponses/_404.cshtml"
            };
        }
    }
}