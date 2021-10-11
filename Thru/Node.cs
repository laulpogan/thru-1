using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Thru
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]

	public class Location
	{
		[JsonIgnore]
		public AnimatedSprite sprite;
		[JsonIgnore]
		public ArrayList Characters;
		[JsonProperty(PropertyName = "Trails")]
		public ArrayList Trails;
		[JsonIgnore]
		public bool isCurrent;
		[JsonProperty(PropertyName = "Coords")]
		public Vector2 Coords;
		[JsonProperty(PropertyName = "Name")]
		public string Name;
		[JsonIgnore]
		public Texture2D Background;
		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "Id")]
		public string ID;
		//public ArrayList Characters;
		public Location( ArrayList edges, String name,Texture2D texture, Vector2 coords)
		{
			Coords = coords;
			Trails = edges;
			isCurrent = false;
			Name = name;
			ID = name;
			//Characters = Characters;
			sprite = new AnimatedSprite(texture, 2, 2);
		}

		public void Update(GameTime gameTime)
		{
           /* if (!isCurrent)
            {
				sprite.Texture = LocationTexture;
            }*/
			sprite.Update(gameTime);
			
			

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			float scale = .01f;
			var widthAvg = (sprite.Texture.Width/sprite.Columns /2 )*scale;
			var heightAvg = (sprite.Texture.Height / sprite.Rows /2 )*scale;
			//Console.WriteLine("coordinates for location waypoint: " + Coords);
			sprite.Draw(spriteBatch, new Vector2(Coords.X - (int)widthAvg, Coords.Y - (int)heightAvg), scale);

		}
		public ArrayList AdjacentLocations()
        {
			ArrayList returnList = new ArrayList();
			foreach (Trail trail in Trails)
            {
				if (trail.Location1 is null || trail.Location2 is null)
					continue;
				else if (trail.Location1.ID == ID)
					returnList.Add(trail.Location2);
				else if (trail.Location2.ID == ID)
					returnList.Add(trail.Location1);
				else
					throw new Exception(Name+ " "+ID+ " This trail doesn't even go here");
            }
			return returnList;
        }
	}


}
