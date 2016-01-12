using System.Collections.Generic;

namespace LibraryThing.WebServices.Model
{
    public class VenuesLtml : ListLtml<VenueItem>
    {
        public override List<VenueItem> ItemList { get; set; }
    }
}