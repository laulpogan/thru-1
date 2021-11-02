using Newtonsoft.Json;

namespace Thru
{
    public class Stats
    {
        [JsonProperty(PropertyName = "Age")]
        public int Age;
        [JsonProperty(PropertyName = "Charisma")]
        public int Charisma;
        [JsonProperty(PropertyName = "Chillness")]
        public int Chillness;
        [JsonProperty(PropertyName = "Cleverness")]
        public int Cleverness;
        [JsonProperty(PropertyName = "Fitness")]
        public int Fitness;
        [JsonProperty(PropertyName = "Luck")]
        public int Luck;
        [JsonProperty(PropertyName = "Miles")]
        public int Miles;
        [JsonProperty(PropertyName = "Money")]
        public int Money;
        [JsonProperty(PropertyName = "Morale")]
        public int Morale;
        [JsonProperty(PropertyName = "Outdoorsyness")]
        public int Outdoorsyness;
        [JsonProperty(PropertyName = "Speed")]
        public int Speed;
        [JsonProperty(PropertyName = "Snacks")]
        public int Snacks;

        public int get(string field)
        {
            int statValue = 0;
            switch (field)
            {
                case "Age":
                    statValue = Age;
                    break;
                case "Charisma":
                    statValue = Charisma;
                    break;
                case "Chillness":
                    statValue = Chillness;
                    break;
                case "Cleverness":
                    statValue = Cleverness;
                    break;
                case "Fitness":
                    statValue = Fitness;
                    break;
                case "Luck":
                    statValue = Luck;
                    break;
                case "Miles":
                    statValue = Miles;
                    break;
                case "Money":
                    statValue = Money;
                    break;
                case "Morale":
                    statValue = Morale;
                    break;
                case "Outdoorsyness":
                    statValue = Outdoorsyness;
                    break;
                case "Snacks":
                    statValue = Snacks;
                    break;
                case "Speed":
                    statValue = Speed;
                    break;
                
            }
            return statValue;
        }
        public void set(string field, int statValue)
        {
            switch (field)
            {
                case "Age":
                    Age = statValue ;
                    break;
                case "Charisma":
                    Charisma = statValue;
                    break;
                case "Chillness":
                    Chillness = statValue;
                    break;
                case "Cleverness":
                    Cleverness = statValue;
                    break;
                case "Fitness":
                    Fitness = statValue;
                    break;
                case "Luck":
                    Luck = statValue;
                    break;
                case "Miles":
                    Miles = statValue;
                    break;
                case "Money":
                    Money = statValue;
                    break;
                case "Morale":
                    Morale = statValue;
                    break;
                case "Outdoorsyness":
                    Outdoorsyness = statValue;
                    break;
                case "Snacks":
                    Snacks = statValue;
                    break;
                case "Speed":
                    Speed = statValue;
                    break;

            }
        }

    }


}