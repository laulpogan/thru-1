using Newtonsoft.Json;

namespace Thru
{
    public enum Stats
    {
        //resources
        [JsonProperty(PropertyName = "Morale")]
        Morale,
        [JsonProperty(PropertyName = "Money")]
        Money,
        [JsonProperty(PropertyName = "Snacks")]
        Snacks,

        //vitals
        [JsonProperty(PropertyName = "Charisma")]
        Charisma,
        [JsonProperty(PropertyName = "Outdoorsyness")]
        Outdoorsyness,
        [JsonProperty(PropertyName = "Cleverness")]
        Cleverness,
        [JsonProperty(PropertyName = "Chillness")]
        Chillness,
        [JsonProperty(PropertyName = "Luck")]
        Luck,
        [JsonProperty(PropertyName = "Speed")]
        Speed,
        [JsonProperty(PropertyName = "Fitness")]
        Fitness,
        //gamestats
        [JsonProperty(PropertyName = "Miles")]
        Miles,
        [JsonProperty(PropertyName = "Age")]
        Age
}
}