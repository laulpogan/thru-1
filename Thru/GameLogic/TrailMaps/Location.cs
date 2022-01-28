using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Thru
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]

	public class Location
	{
		[JsonIgnore]
		public AnimatedSprite sprite;
		[JsonProperty(PropertyName = "Id")]
		public string ID;
		[JsonProperty(PropertyName = "Trails")]
		public ArrayList Trails;
		[JsonProperty(PropertyName = "Coords")]
		public Vector3 Coords;
		[JsonProperty(PropertyName = "CoordsXY")]
		public Vector2 CoordsXY;
		[JsonProperty(PropertyName = "Name")]
		public string Name;
		[JsonIgnore]
		public Texture2D Background;
		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }
		public Tags[] Tags;
		SpriteFont Font;
		public Location(String id, String name, ArrayList edges, Texture2D texture, Vector3 coords, SpriteFont font)
		{
			ID = id;
			Name = name;
			Font = font;
			Coords = coords;
			CoordsXY = new Vector2(coords.X, coords.Y);
			Trails = edges;
			sprite = new AnimatedSprite(texture, 2, 2, Color.White);
			Tags = new Tags[1];

		}

		public void Update(GameTime gameTime)
		{
			sprite.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			float scale = .01f;
			var widthAvg = (sprite.Texture.Width/sprite.Columns /2 )*scale;
			var heightAvg = (sprite.Texture.Height / sprite.Rows /2 )*scale;
			Vector2 pos = new Vector2(Coords.X - (int)widthAvg, Coords.Y - (int)heightAvg);
			sprite.Draw(spriteBatch,pos.ToPoint(), scale);
			pos.Y -= 4;
			pos.X -= 10;
			spriteBatch.DrawString(Font, Name, pos, Color.Black,0,Vector2.Zero, .2f, new SpriteEffects(), 0);

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

		public Dictionary<Location, float> AdjacentLocationsWithWeight()
		{
			Dictionary<Location, float> returnList = new Dictionary<Location, float>();
			foreach (Trail trail in Trails)
			{
				if (trail.Location1 is null || trail.Location2 is null)
					continue;
				else if (trail.Location1.ID == ID)
					returnList[trail.Location2] = trail.Value;
				else if (trail.Location2.ID == ID)
					returnList[trail.Location1] = trail.Value ;
				else
					throw new Exception(Name + " " + ID + " This trail doesn't even go here");
			}
			return returnList;
		}
	}
	
}

