using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fusion.Startup))]
namespace Fusion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
