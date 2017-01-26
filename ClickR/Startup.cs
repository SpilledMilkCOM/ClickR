using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(SignalRDemo.Startup))]
namespace SignalRDemo
{
    public class Startup
    {
        // Old school: was to call MapRoutes() extension method.

        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}