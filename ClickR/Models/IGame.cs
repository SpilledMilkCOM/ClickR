using System.Collections.Generic;

namespace SignalRDemo.Models
{
    public interface IGame
    {
        List<Player> RankedPlayers { get; }
    }
}