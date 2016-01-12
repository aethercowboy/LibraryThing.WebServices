using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Venue
    {
        public int Id { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Value { get; set; }
    }
}