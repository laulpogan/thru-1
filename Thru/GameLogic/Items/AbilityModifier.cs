using Newtonsoft.Json;
using System;

namespace Thru
{
    public class AbilityModifier : IModifier
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name;
        [JsonProperty(PropertyName = "ID")]
        public string ID;
        public string effectedStat;
        public int effect;



        public AbilityModifier()
        {

        }

    }


}