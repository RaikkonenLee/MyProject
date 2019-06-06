using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCPattern.Startup))]
namespace MVCPattern
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
