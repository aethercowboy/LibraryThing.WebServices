using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class AuthorResponse : Response<AuthorLtml>
    {
        public override AuthorLtml Ltml { get; set; }
    }
}