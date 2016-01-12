using System;
using JackLeitch.RateGate;

namespace LibraryThing.WebServices.Common
{
    public static class RateLimiters
    {
        public static readonly RateGate PerSecondRateGate = new RateGate(1, TimeSpan.FromSeconds(1));
        public static readonly RateGate PerDayRateGate = new RateGate(1000, TimeSpan.FromDays(1));
    }
}