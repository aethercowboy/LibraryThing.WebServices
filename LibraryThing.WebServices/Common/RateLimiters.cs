using System;

namespace LibraryThing.WebServices.Common
{
    public static class RateLimiters
    {
        public static readonly SimpleRateGate PerSecondRateGate = new SimpleRateGate(1, TimeSpan.FromSeconds(1));
        public static readonly SimpleRateGate PerDayRateGate = new SimpleRateGate(1000, TimeSpan.FromDays(1));
    }
}
