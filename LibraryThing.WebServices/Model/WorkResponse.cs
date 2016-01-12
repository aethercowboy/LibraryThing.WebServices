using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class WorkResponse : Response<WorkLtml>
    {
        public override WorkLtml Ltml { get; set; }
    }
}