using System.Collections.Generic;

namespace LibraryThing.WebServices.Model
{
    public class EventsLtml : ListLtml<EventItem>
    {
        public override List<EventItem> ItemList { get; set; }
    }
}