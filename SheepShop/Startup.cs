using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SheepShop.Startup))]
namespace SheepShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
