using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Damacon.ClientWebApp.Startup))]
namespace Damacon.ClientWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
