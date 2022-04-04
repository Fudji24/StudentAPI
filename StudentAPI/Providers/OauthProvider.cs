using Microsoft.Owin.Security.OAuth;
using StudentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;

namespace StudentAPI.Providers
{
    [EnableCors("*", "*", "*")]
    public class OauthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
           
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
           
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            using (var _context = new StudentDBContext())
            {
                if (_context != null)
                {
                    var user = _context.User.Where(u => u.UserName == context.UserName && u.Password == context.Password).FirstOrDefault();
                   
                    if (user != null)
                    {
                         
                        identity.AddClaim(new Claim("UserName", context.UserName));
                        identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("Pogrešan unos!", "Korisnično ime ili šifra su nevažeći!");
                        context.Rejected();

                    }
                }
                else
                {
                    context.SetError("Pogrešan unos!", "Korisnično ime ili šifra su nevažeći!");
                    context.Rejected();
                }
                return;
            }
        }
    }
}