using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5Pattern.Startup))]
namespace MVC5Pattern
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
