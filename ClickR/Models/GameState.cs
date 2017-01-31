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
    public class GameState : IGameState
    {
        // Instance variables
        // Only support a single game for now.
        private readonly Game _game;

        public GameState()
        {
            _game = new Game();
        }

        public Game GameInfo => _game;

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