using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ui.WebApi.Filters;
using Ui.WebApi.Managers;

namespace Ui.WebApi.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        [Route("SoAdministrador")]
        //Url: http://localhost:63944/api/home/SoAdministrador
        //Obs: Não esquecer de passar o token!!!
        public string SoAdministrador()
        {
            return "Usuário Administrador.";
        }

        [Authorize]
        [HttpGet]
        [Route("TesteLogin")]
        //Url: http://localhost:63944/api/home/TesteLogin
        public string TesteLogin()
        {
            return "O Usuário esta logado.";
        }

        public async Task<HttpResponseMessage> Create()
        {
            HttpResponseMessage response;
            string resultado = string.Empty;

            try
            {
                var usuario = new IdentityUser()
                {
                    UserName = "ffonseca"
                };

                var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

                IdentityResult result = await userManager.CreateAsync(usuario, "123Tr0car@@");

                if (result.Succeeded)
                    resultado = "Usuario Criado com sucesso";
                else
                    resultado = string.Join(",", result.Errors);

                response = Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.BadRequest, resultado);
            }

            return await Task.FromResult(response);
        }
        
        [HttpGet]
        [Route("TesteFilter")]
        [ClaimsAuthorizeAttribute]
        //Url: http://localhost:63944/api/home/TesteFilter
        public string TesteFilter()
        {
            return "Usuário passou no filtro.";
        }
    }
}
