using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Ui.WebApi.Contexts;
using Ui.WebApi.Managers;
using Ui.WebApi.Providers;


namespace Ui.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            ConfigureRotas(configuration);

            app.CreatePerOwinContext(Contexto.Create);
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new DeviMediaAuthorizationServerProvider()
            };
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseWebApi(configuration);
        }

        private static void ConfigureRotas(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //name: "DefaultApi",
            //routeTemplate: "api/{controller}/{id}",
            //defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
