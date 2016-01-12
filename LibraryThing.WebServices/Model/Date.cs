using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Date
    {
        public long TimeStamp { get; set; }
        public string Value { get; set; }
    }
}