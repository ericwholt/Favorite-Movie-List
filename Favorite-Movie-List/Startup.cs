using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Favorite_Movie_List.Startup))]
namespace Favorite_Movie_List
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
