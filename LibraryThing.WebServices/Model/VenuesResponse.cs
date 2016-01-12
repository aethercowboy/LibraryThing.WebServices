using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class VenuesResponse : Response<VenuesLtml>
    {
        public override VenuesLtml Ltml { get; set; }
    }
}