
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Owin;
using UI.Mvc.Contexts;
using UI.Mvc.Managers;

using Microsoft.Owin;

namespace UI.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
              app.CreatePerOwinContext(Contexto.Create);
              app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
              app.CreatePerOwinContext<AppSignInManager>(AppSignInManager.Create);
   
              app.UseCookieAuthentication(new CookieAuthenticationOptions
              {
                  AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                  LoginPath = new PathString("/Home/Login"),
                  CookieName = "Devimedia",
                  CookiePath = "/"
              });
            
        }
    }
}
