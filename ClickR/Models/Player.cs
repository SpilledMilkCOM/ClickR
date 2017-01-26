using System;

namespace SignalRDemo.Models
{
    public class Player
    {
        public Player(string name, string hash)
        {
            Hash = hash;
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public int ClickCount { get; set; }

        public string ConnectionId { get; set; }

        public string Group { get; set; }

        public string Hash { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsReady { get; set; }
    }
}