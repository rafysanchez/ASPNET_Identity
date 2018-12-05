using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Ui.WebApi.Managers
{
    public class AppSignInManager : SignInManager<IdentityUser, string>
    {
        public AppSignInManager(AppUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }

        public static AppSignInManager Create(IdentityFactoryOptions<AppSignInManager> option, IOwinContext context)
        {
            var manager = context.GetUserManager<AppUserManager>();

            var sign = new AppSignInManager(manager, context.Authentication);

            return sign;
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user)
        {
            var claimIdentity = await base.CreateUserIdentityAsync(user);

            claimIdentity.AddClaim(new Claim(ClaimTypes.Country, "Brasil"));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Gender, "Masculino"));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "Administrador"));

            return claimIdentity;
        }
    }
}