using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IOMSystem.UI.Startup))]
namespace IOMSystem.UI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
