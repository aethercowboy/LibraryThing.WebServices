using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Location
    {
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public Address Address { get; set; }

        public Distance Distance { get; set; }
    }
}