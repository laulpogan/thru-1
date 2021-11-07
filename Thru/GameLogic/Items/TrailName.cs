using Newtonsoft.Json;
using System;

namespace Thru
{
    public class TrailName
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name;
        [JsonProperty(PropertyName = "ID")]
        public string ID;
        public StatModifier StatModifier;




        public TrailName(string name, StatModifier statModifier)
        {
            Name = name;
            StatModifier = statModifier;
        }

    }


}