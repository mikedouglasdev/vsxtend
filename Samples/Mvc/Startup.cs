using Microsoft.Owin;
using Owin;

namespace Vsxtend.Samples.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}