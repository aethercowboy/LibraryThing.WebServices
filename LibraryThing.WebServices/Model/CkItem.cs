using System;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public abstract class CkItem : Item
    {
        public Author Author { get; set; }
        public CommonKnowledge CommonKnowledge { get; set; }
    }
}