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

	public record EncounterData
    {
		[JsonProperty(PropertyName = "options")]
		public EncounterOptionData[] options;
		[JsonProperty(PropertyName = "dropRate")]
		public float dropRate;
		[JsonProperty(PropertyName = "locationType")]
		public LocationType locationType;
    }

	public record EncounterOptionData
    {
		[JsonProperty(PropertyName = "text")]
		public string text;
		[JsonProperty(PropertyName = "checkStat")]
		public Stats checkStat;
		[JsonProperty(PropertyName = "consequence")]
		public EncounterConsequenceData consequence;
	}
	
	public record EncounterConsequenceData
    {
		[JsonProperty(PropertyName = "success")]
		public EncounterConsequenceData[] success;
		[JsonProperty(PropertyName = "failure")]
		public EncounterResolutionData[] failure;


	}

	public record EncounterResolutionData
	{
		[JsonProperty(PropertyName = "effectedStat")]
		public Stats effectedStat;
		[JsonProperty(PropertyName = "effect")]
		public int effect;
		[JsonProperty(PropertyName = "rewardItem")]
		public ItemData rewardItem;
		public TrailNameData rewardTrailName;


	}


	public record ItemData
    {
		[JsonProperty(PropertyName = "ID")]
		public string ID;
		[JsonProperty(PropertyName = "name")]
		public string name;
		[JsonProperty(PropertyName = "buff")]
		public buffData buff;
    }

	public record CharacterData
    {
		[JsonProperty(PropertyName = "characterStats")]
		public CharacterStatsData characterStats;
		[JsonProperty(PropertyName = "trailName")]
		public TrailNameData trailName;
    }

	public record CharacterStatsData
    {
		[JsonProperty(PropertyName = "Morale")]
		public int Morale;
		[JsonProperty(PropertyName = "Money")]
		public int Money;
		[JsonProperty(PropertyName = "Charisma")]
		public int Charisma;
		[JsonProperty(PropertyName = "Hardiness")]
		public int Hardiness;
		[JsonProperty(PropertyName = "Cleverness")]
		public int Cleverness;
		[JsonProperty(PropertyName = "Chillness")]
		public int Chillness;

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

}