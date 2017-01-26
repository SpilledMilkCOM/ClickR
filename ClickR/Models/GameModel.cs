using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalRDemo.Models
{
    [Serializable]
    public class GameModel : IGame
    {
        public GameModel(Game game)
        {
            Name = "The Game";
            RankedPlayers = game.RankedPlayers.ToList();
        }

        public string Name { get; private set; }

        public List<Player> RankedPlayers { get; private set; }
    }
}