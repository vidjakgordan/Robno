using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Robno.Startup))]
namespace Robno
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
