using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class EventsResponse : Response<EventsLtml>
    {
        public override EventsLtml Ltml { get; set; }
    }
}