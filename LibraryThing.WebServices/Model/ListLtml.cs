using System.Collections.Generic;

namespace LibraryThing.WebServices.Model
{
    public abstract class ListLtml<T> : Ltml
        where T : Item
    {
        public abstract List<T> ItemList { get; set; }  
    }
}