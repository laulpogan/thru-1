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
		[JsonProperty(PropertyName = "rewardTrailName")]
		public TrailNameData rewardTrailName;
	}

	public record EncounterRewardData
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