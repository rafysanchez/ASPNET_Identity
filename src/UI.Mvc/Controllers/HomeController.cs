using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Mvc.Managers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading;
using System.Collections.ObjectModel;
using UI.Mvc.Filters;

namespace UI.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create()
        {
            var usuario = new IdentityUser()
            {
                UserName = "ffonseca"
            };

            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            IdentityResult result = await userManager.CreateAsync(usuario, "123Tr0car@@");

            if (result.Succeeded)
                ViewBag.Resultado = "Usuario Criado com sucesso";
            else
                ViewBag.Resultado = string.Join(",", result.Errors);
                //AddErros(result);

            return View();
        }

        public async Task<ActionResult> Login()
        {
            var appAsign = HttpContext.GetOwinContext().Get<AppSignInManager>();

            var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

            var user = await userManager.FindAsync("ffonseca", "123Tr0car@@");

            if (user != null)
                await appAsign.SignInAsync(user, true, true);

            return View();
        }

        [Authorize]
        public ActionResult VerificarClaims()
        {
            ClaimsPrincipal currentPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            var claims = new Collection<string>();

            foreach (Claim ci in currentPrincipal.Claims)
                claims.Add("Tipo: " + ci.Type + " < ----- > Valor:" + ci.Value);

            return View(claims);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult SoAdministrador()
        {
            return View();
        }


        [ClaimsAuthorizeAttribute]
        public ActionResult TestarClaimFilter()
        {
            return View();
        }

        private void AddErros(IdentityResult result)
        {
            foreach (var erros in result.Errors)
            {
                ModelState.AddModelError("", erros);
            }
        }
    }
}