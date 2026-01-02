using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IOMSystem.Web.Startup))]
namespace IOMSystem.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
