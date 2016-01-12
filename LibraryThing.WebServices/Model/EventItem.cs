using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class EventItem : LocationItem
    {
        public string Title { get; set; }
        public Venue Venue { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }
}