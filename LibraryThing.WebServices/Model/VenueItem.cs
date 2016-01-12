using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class VenueItem : LocationItem
    {
        public string Name { get; set; }
        public VenueType VenueType { get; set; }
        public string OfficialSite { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public Images Images { get; set; }
        public bool Wifi { get; set; }
        public bool Food { get; set; }
        public bool Indiebound { get; set; }
    }
}
