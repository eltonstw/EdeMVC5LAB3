using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LAB.Startup))]
namespace LAB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
