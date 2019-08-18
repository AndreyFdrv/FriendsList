using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FriendsList.Web.Startup))]
namespace FriendsList.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
