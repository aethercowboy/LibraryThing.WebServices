using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class WorkLtml : Ltml<WorkItem>
    {
        public override WorkItem Item { get; set; }
    }
}