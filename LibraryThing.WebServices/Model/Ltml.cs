using System;
using RestSharp.Deserializers;

namespace LibraryThing.WebServices.Model
{
    public abstract class Ltml
    {
        [DeserializeAs(Name = "version", Attribute = true)]
        public decimal Version { get; set; }

        public string Legal { get; set; }
    }

    [Serializable]
    public abstract class Ltml<T> : Ltml
        where T : Item
    {
        public abstract T Item { get; set; }
    }
}