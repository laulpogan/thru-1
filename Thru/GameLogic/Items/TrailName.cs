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
        public IModifier Modifier;




        public TrailName(string name, IModifier modifier)
        {
            Name = name;
            Modifier = modifier;
        }

    }


}