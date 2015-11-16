using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Smart_test.Startup))]
namespace Smart_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
