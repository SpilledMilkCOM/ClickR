using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SignalRDemo.Models
{
    public class Player
    {
        private volatile int _clickCount;
        // Keep this thread safe because two people can logon as the same name.  "Power Team"
        private readonly ConcurrentQueue<DateTime> _clickTimes;
        private double? _spanMax;

        public Player(string name, string hash)
        {
            _clickCount = 0;
            _clickTimes = new ConcurrentQueue<DateTime>();
            Hash = hash;
            Id = Guid.NewGuid().ToString();
            MaxClicks = 50;
            Name = name;
            Joined = DateTime.UtcNow;
        }

        public int ClickCount => _clickCount;

        public double ClicksPerSecond
        {
            get
            {
                double result = 0;

                var difference = QueueSpan();

                if (difference.TotalSeconds > 0)
                {
                    result = _clickTimes.Count / difference.TotalSeconds;
                }

                return result;
            }
        }

        public string ConnectionId { get; set; }

        public string Group { get; set; }

        public string Hash { get; set; }

        public string Id { get; set; }

        public bool IsReady { get; set; }

        public DateTime Joined { get; set; }

        public DateTime? LastClick { get; private set; }

        /// <summary>
        /// The length of the queue for statistics
        /// </summary>
        public int MaxClicks { get; private set; }

        public string Name { get; set; }

        public double? SpanMax
        {
            get
            {
                double? spanSeconds = SpanSeconds;

                if (spanSeconds.HasValue && (_spanMax > spanSeconds || !_spanMax.HasValue))
                {
                    _spanMax = spanSeconds;
                }

                return _spanMax;
            }
        }

        public double? SpanSeconds
        {
            get
            {
                double? result = null;

                if (_clickTimes.Count == MaxClicks)
                {
                    result = QueueSpan().TotalMilliseconds / 1000;
                }

                return result;
            }
        }

        public void Clicked()
        {
            _clickCount++;
            LastClick = DateTime.UtcNow;

            _clickTimes.Enqueue(LastClick.Value);

            // Don't let the list become too big.
            // Might need some locking in here.

            while (_clickTimes.Count > MaxClicks)
            {
                DateTime removed;

                _clickTimes.TryDequeue(out removed);
            }
        }

        /// <summary>
        /// Returns the amount of time spanning from the first click (in the queue) to the last.
        /// </summary>
        /// <returns></returns>
        private TimeSpan QueueSpan()
        {
            DateTime started = _clickTimes.FirstOrDefault();
            DateTime ended = _clickTimes.LastOrDefault();
            return ended.Subtract(started);
        }
    }
}