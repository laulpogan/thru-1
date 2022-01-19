using Newtonsoft.Json;
using System;

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
        [JsonProperty(PropertyName = "Energy")]
        public int Energy;
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
                case "Energy":
                    statValue = Energy;
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

        public int maxValue(string field)
        {
            int statValue = 0;
            switch (field)
            {
                case "Age":
                    statValue = 1000;
                    break;
                case "Charisma":
                    statValue = 100;
                    break;
                case "Chillness":
                    statValue = 100;
                    break;
                case "Cleverness":
                    statValue = 100;
                    break;
                case "Energy":
                    statValue = 1000;
                    break;
                case "Fitness":
                    statValue = 100;
                    break;
                case "Luck":
                    statValue = 100;
                    break;
                case "Miles":
                    statValue = 2650;
                    break;
                case "Money":
                    statValue = 999999999;
                    break;
                case "Morale":
                    statValue = 100;
                    break;
                case "Outdoorsyness":
                    statValue = 100;
                    break;
                case "Snacks":
                    statValue = 100;
                    break;
                case "Speed":
                    statValue = 100;
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
                case "Energy":
                    Energy = statValue;
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

        public Stats()
        {
            Random rand = new Random();
            Age = rand.Next(20) + 15;
            Charisma = rand.Next(20);
            Chillness = rand.Next(20);
            Cleverness = rand.Next(20);
            Energy = rand.Next(100);
            Fitness = rand.Next(20);
            Luck = rand.Next(20);
            Miles = rand.Next(20);
            Money = rand.Next(2000) + 1000;
            Morale = rand.Next(20);
            Outdoorsyness = rand.Next(20);
            Snacks = rand.Next(10) + 15;
            Speed = rand.Next(20);
        }

     public Stats baseStats()
        {
            return this;
        }

     public Stats withModifiers(Player player)
        {
            return this;
        }
    }


}