using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON;
using Newtonsoft.Json;
using GeoJSON.Net.Geometry;
using GeoJSON.Net.Feature;

namespace Thru
{
    public record FirstName
    {
        [JsonProperty(PropertyName = "female")]
        public int female;
        [JsonProperty(PropertyName = "male")]
        public int male;
        [JsonProperty(PropertyName = "most_likely")]
        public string most_likely;
    }

    public record LocationData
    {
        [JsonProperty(PropertyName = "text")]
        public string text;
        [JsonProperty(PropertyName = "title")]
        public string title;
        [JsonProperty(PropertyName = "options")]
        public EncounterOptionData[] options;
        [JsonProperty(PropertyName = "dropRate")]
        public float dropRate;
        [JsonProperty(PropertyName = "locationType")]
        public Tags[] encounterTags;
        [JsonProperty(PropertyName = "resolutionType")]
        public ResolutionType resolutionType;

    }

    public record EncounterData
    {
        [JsonProperty(PropertyName = "text")]
        public string text;
        [JsonProperty(PropertyName = "title")]
        public string title;
        [JsonProperty(PropertyName = "options")]
        public EncounterOptionData[] options;
        [JsonProperty(PropertyName = "dropRate")]
        public float dropRate;
        [JsonProperty(PropertyName = "locationType")]
        public Tags[] encounterTags;
        [JsonProperty(PropertyName = "resolutionType")]
        public ResolutionType resolutionType;
    }

    public enum ResolutionType
    {
        Cutscene,
        Duo,
        Leader,
        PVP,
        PVE,
        Quadruple,
        Random,
        SimpleMajority,
        Tramily,
        Triple
    }

    public record EncounterOptionData
    {
        [JsonProperty(PropertyName = "text")]
        public string text;
        [JsonProperty(PropertyName = "checkStat")]
        public string checkStat;
        [JsonProperty(PropertyName = "diceCheck")]
        public int diceCheck;
        [JsonProperty(PropertyName = "success")]
        public EncounterResolutionData success;
        [JsonProperty(PropertyName = "failure")]
        public EncounterResolutionData failure;
        public EncounterOptionData(string Text, string CheckStat, int DC, EncounterResolutionData Success, EncounterResolutionData Failure)

        {
            text = Text;
            checkStat = CheckStat;
            success = Success;
            failure = Failure;
            diceCheck = DC;
        }
    }


    public record EncounterResolutionData
    {
        [JsonProperty(PropertyName = "effectedStat")]
        public string effectedStat;
        [JsonProperty(PropertyName = "text")]
        public string text;
        [JsonProperty(PropertyName = "effect")]
        public int effect;
        [JsonProperty(PropertyName = "rewardItem")]
        public ItemData rewardItem;
        [JsonProperty(PropertyName = "rewardTrailName")]
        public TrailNameData rewardTrailName;

        public EncounterResolutionData(string EffectedStat, int Effect, ItemData item, TrailNameData trailName)
        {
            effectedStat = EffectedStat;
            effect = Effect;
            rewardItem = item;
            rewardTrailName = trailName;
        }
    }


    public record RecordManifestData
    {
        [JsonProperty(PropertyName = "type")]
        public string type;
        [JsonProperty(PropertyName = "path")]
        public string path;
    }

    public record GlobalStateData
    {

    }

    public record ItemData
    {
        [JsonProperty(PropertyName = "ID")]
        public string ID;
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "buff")]
        public buffData buff;
        [JsonProperty(PropertyName = "itemShape")]
        public int[,] itemShape;
        [JsonProperty(PropertyName = "itemSlot")]
        public ItemSlot itemSlot;
        [JsonProperty(PropertyName = "isFlexible")]
        public bool isFlexible;
        [JsonProperty(PropertyName = "iconPath")]
        public string iconPath;
        [JsonProperty(PropertyName = "spriteSheetPath")]
        public string spriteSheetPath;
        [JsonProperty(PropertyName = "bulk")]
        public float bulk;
        [JsonProperty(PropertyName = "weight")]
        public float weight;
        [JsonProperty(PropertyName = "boardHome")]
        public Microsoft.Xna.Framework.Point boardHome;
        [JsonProperty(PropertyName = "screenXY")]
        public Microsoft.Xna.Framework.Point screenXY;

        [JsonProperty(PropertyName = "secondarySpriteSheetPath")]
        public string secondarySpriteSheetPath;

    }

    public record CharacterData
    {
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "characterStats")]
        public Stats characterStats;
        [JsonProperty(PropertyName = "trailName")]
        public TrailNameData trailName;
        [JsonProperty(PropertyName = "tramily")]
        public CharacterData[] tramily;
    }




    public record TrailNameData
    {
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "dropRate")]
        public float dropRate;
        [JsonProperty(PropertyName = "buff")]
        public buffData buff;
    }

    public record buffData
    {
        [JsonProperty(PropertyName = "isDeBuff")]
        public bool isDeBuff = false;
        [JsonProperty(PropertyName = "effectedStat")]
        public Stats effectedStat;
        [JsonProperty(PropertyName = "statModifier")]
        public int statModifier;
    }



    public record perkData
    {
        [JsonProperty(PropertyName = "name")]
        public string name;
        [JsonProperty(PropertyName = "buff")]
        public buffData buff;
    }

}