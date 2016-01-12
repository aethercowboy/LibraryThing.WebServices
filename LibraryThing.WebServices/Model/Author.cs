using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Author
    {
        public int Id { get; set; }
        public string AuthorCode { get; set; }
        public string Value { get; set; }
    }
}