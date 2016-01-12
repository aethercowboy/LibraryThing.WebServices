using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public abstract class Response<T>
    {
        public string Stat { get; set; }
        public abstract T Ltml { get; set; }
    }
}
