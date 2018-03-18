using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Membresias.Startup))]
namespace Membresias
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
