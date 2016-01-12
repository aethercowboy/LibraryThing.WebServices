using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public abstract class LocationItem : Item
    {
        public User User { get; set; }
        public string Description { get; set; }
    }
}