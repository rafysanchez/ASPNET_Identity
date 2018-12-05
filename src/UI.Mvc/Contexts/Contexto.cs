using Microsoft.AspNet.Identity.EntityFramework;

namespace UI.Mvc.Contexts
{
    public class Contexto : IdentityDbContext<IdentityUser>
    {
        public Contexto()
            : base(@"Data Source=(localdb)\mssqllocaldb;Integrated Security=True; Initial Catalog=DeviMediaClaims; Connect Timeout=15;
                              Encrypt=False;TrustServerCertificate=False")
        { }
        public static Contexto Create()
        {
            return new Contexto();
        }
    }
}