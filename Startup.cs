using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Icenet.Service.Logging.UI.Startup))]
namespace Icenet.Service.Logging.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
