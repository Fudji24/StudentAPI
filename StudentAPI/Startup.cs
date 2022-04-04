using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using StudentAPI.Providers;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(StudentAPI.Startup))]

namespace StudentAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);   

            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2), 
                Provider = new OauthProvider()
            };

            app.UseOAuthBearerTokens(OAuthOptions);
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
       
    }
}
