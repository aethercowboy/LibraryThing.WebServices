using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}