using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin;
using SignalRDemo.Hubs;
using SignalRDemo.Models;

[assembly: OwinStartup(typeof(SignalRDemo.Startup))]
namespace SignalRDemo
{
    public class Startup
    {
        private IGameState _gameState;
        // Old school: was to call MapRoutes() extension method.

        public void Configuration(IAppBuilder app)
        {
            _gameState = new GameState();
            GlobalHost.DependencyResolver.Register(
            typeof(GameHub),
            () => new GameHub(_gameState));
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}