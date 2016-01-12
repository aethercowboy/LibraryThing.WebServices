using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public abstract class Item
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public string Url { get; set; }
    }
}