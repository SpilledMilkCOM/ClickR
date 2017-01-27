using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SignalRDemo.Models
{
    public class Game
    {
        // Needs to be thread safe since the Game is inside of a Singleton.
        private readonly ConcurrentDictionary<string, Player> _players;

        public Game()
        {
            Name = "The Game";

            _players = new ConcurrentDictionary<string, Player>(StringComparer.OrdinalIgnoreCase);

            Started = DateTime.UtcNow;
        }

        public bool IsGameOn { get; set; }

        public string Name { get; set; }

        public IEnumerable<Player> Players => _players.Values;

        public IEnumerable<Player> RankedPlayers => _players.Values.OrderByDescending(player => player.ClickCount);

        public DateTime Started { get; private set; }

        public void AddPlayer(Player player)
        {
            _players.TryAdd(player.Id, player);
        }

        public void RemovePlayer(Player player)
        {
            Player removedPlayer;

            _players.TryRemove(player.Id, out removedPlayer);
        }
    }
}