using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class AuthorLtml : Ltml<AuthorItem>
    {
        public override AuthorItem Item { get; set; }
    }
}