using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Platinum.Life.Web2.Startup))]
namespace Platinum.Life.Web2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
