using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MachineAccessControl.Startup))]
namespace MachineAccessControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
