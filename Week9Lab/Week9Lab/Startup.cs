using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Week9Lab.Startup))]
namespace Week9Lab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
