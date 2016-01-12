using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class WorkItem : CkItem
    {
        public decimal Rating { get; set; }
        public string Title { get; set; }
    }
}
