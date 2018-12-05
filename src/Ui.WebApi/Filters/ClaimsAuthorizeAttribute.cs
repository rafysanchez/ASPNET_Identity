

using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace Ui.WebApi.Filters
{
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            var user = System.Web.HttpContext.Current.User as ClaimsPrincipal;

            if (user.Claims.Where(c => c.Type == ClaimTypes.Country)
                .Any(x => x.Value == "Brasil")
                && user.IsInRole("Administrador"))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}