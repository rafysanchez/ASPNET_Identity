using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;


namespace UI.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Claim claim2 = new Claim(ClaimTypes.Name, "Fabio Rodrigues Fonseca");
            Claim claim3 = new Claim(ClaimTypes.Role, "Administrador");
            Claim claim4 = new Claim(ClaimTypes.Email, "fabison@ig.com.br");

            IList<Claim> Claims = new List<Claim>() {
                  claim2,
                  claim3,
                  claim4
              };

            //Criando uma Identidade e associando-a ao ambiente.
            ClaimsIdentity identity = new ClaimsIdentity(Claims, "Devimedia");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            Thread.CurrentPrincipal = principal;

            

            System.Console.Write("\n\n");
            System.Console.WriteLine("Usuário Autenticado:" + Thread.CurrentPrincipal.Identity.IsAuthenticated);
            System.Console.WriteLine("Identidade:" + Thread.CurrentPrincipal.Identity.Name);
            System.Console.Write("\n");

            //Criando uma Identidade e associando-a ao ambiente.
            ClaimsIdentity identity2 = new ClaimsIdentity(Claims, "Devimedia", ClaimTypes.Email, ClaimTypes.Role);
            ClaimsPrincipal principal2 = new ClaimsPrincipal(identity2);
            Thread.CurrentPrincipal = principal2;

            System.Console.WriteLine("Usuário Autenticado:" + Thread.CurrentPrincipal.Identity.IsAuthenticated);
            System.Console.WriteLine("Identidade:" + Thread.CurrentPrincipal.Identity.Name);
            System.Console.Write("\n");

            ClaimsPrincipal currentPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

            System.Console.WriteLine("Claims do Usuário:\n");

            foreach (Claim ci in currentPrincipal.Claims)
                System.Console.WriteLine(ci.Value);

            System.Console.Write("\n");
            System.Console.WriteLine(currentPrincipal.Identity.Name + " Pertence a role Administrador? \n" + currentPrincipal.IsInRole("Administrador"));

            System.Console.ReadKey();
        }
    }
}
