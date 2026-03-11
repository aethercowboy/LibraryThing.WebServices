using System;
using System.Collections.Generic;
using System.Threading;

namespace LibraryThing.WebServices.Common
{
    public sealed class SimpleRateGate
    {
        private readonly int _occurrences;
        private readonly TimeSpan _timeUnit;
        private readonly Queue<DateTimeOffset> _queue = new Queue<DateTimeOffset>();
        private readonly object _sync = new object();

        public SimpleRateGate(int occurrences, TimeSpan timeUnit)
        {
            if (occurrences <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(occurrences));
            }

            if (timeUnit <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(timeUnit));
            }

            _occurrences = occurrences;
            _timeUnit = timeUnit;
        }

        public void WaitToProceed()
        {
            while (true)
            {
                TimeSpan delay;

                lock (_sync)
                {
                    var now = DateTimeOffset.UtcNow;
                    while (_queue.Count > 0 && now - _queue.Peek() >= _timeUnit)
                    {
                        _queue.Dequeue();
                    }

                    if (_queue.Count < _occurrences)
                    {
                        _queue.Enqueue(now);
                        return;
                    }

                    delay = _timeUnit - (now - _queue.Peek());
                    if (delay < TimeSpan.Zero)
                    {
                        delay = TimeSpan.Zero;
                    }
                }

                Thread.Sleep(delay);
            }
        }
    }
}
