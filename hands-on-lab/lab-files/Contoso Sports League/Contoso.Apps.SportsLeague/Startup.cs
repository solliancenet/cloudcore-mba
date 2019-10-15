using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Contoso.Apps.SportsLeague.Web.Startup))]

namespace Contoso.Apps.SportsLeague.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
