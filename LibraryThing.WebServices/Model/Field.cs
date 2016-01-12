using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace LibraryThing.WebServices.Model
{
    [Serializable]
    public class Field
    {
        public int Type { get; set; }
        [DeserializeAs(Name="name", Attribute = true)]
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public List<Version> VersionList { get; set; } 
    }
}