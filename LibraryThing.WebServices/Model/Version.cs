using System;
using System.Collections.Generic;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Version
    {
        public int Id { get; set; }
        public bool Archived { get; set; }
        public string Lang { get; set; }
        public Date Date { get; set; }
        public Person Person { get; set; }
        public List<string> FactList { get; set; } 
    }
}