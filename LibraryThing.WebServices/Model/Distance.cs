using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Distance
    {
        public int? Measure { get; set; }
        public decimal? Value { get; set; }
    }
}