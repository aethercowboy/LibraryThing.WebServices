using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class VenueResponse : Response<VenueLtml>
    {
        public override VenueLtml Ltml { get; set; }
    }
}