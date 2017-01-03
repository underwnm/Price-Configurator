using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Price_Configurator.Startup))]
namespace Price_Configurator
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
