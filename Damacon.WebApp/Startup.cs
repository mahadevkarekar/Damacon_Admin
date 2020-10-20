using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Damacon.WebApp.Startup))]
namespace Damacon.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
