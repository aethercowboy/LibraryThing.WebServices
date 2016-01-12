using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class VenueLtml : Ltml<VenueItem>
    {
        public override VenueItem Item { get; set; }
    }
}