using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRDemo.Hubs;

namespace SignalRDemo.Models
{
    // A Singleton to contain the game state.
    public class GameState
    {
        // Only instanciate the game state when needed.
        private readonly static Lazy<GameState> _instance = new Lazy<GameState>(() => new GameState(GlobalHost.ConnectionManager.GetHubContext<GameHub>()));

        // Instance variables
        // Only support a single game for now.
        private readonly Game _game;

        private GameState(IHubContext context)
        {
            _game = new Game();

            Clients = context.Clients as IHubConnectionContext<GameHub>;
            Groups = context.Groups;
        }

        public static GameState Instance => _instance.Value;

        public IHubConnectionContext<GameHub> Clients { get; set; }

        //public GameModel GameInfo => new GameModel(_game);
        public Game GameInfo => _game;

        public IGroupManager Groups { get; set; }

        public Player CreatePlayer(string userName)
        {
            var player = new Player(userName, GetMD5Hash(userName));

            _game.AddPlayer(player);

            return player;
        }

        public Player GetPlayer(string userName)
        {
            return _game.Players.FirstOrDefault(player => player.Name == userName);
        }

        private string GetMD5Hash(string userName)
        {
            return string.Join(string.Empty, MD5.Create().ComputeHash(Encoding.Default.GetBytes(userName)).Select(item => item.ToString("x2")));
        }
    }
}