using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON.Net.Feature;
using Newtonsoft.Json.Linq;
using System.IO.Pipelines;
using Newtonsoft.Json;

namespace Thru
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Trail
	{
		[JsonIgnore]
		public AnimatedSprite sprite;
		[JsonIgnore]
		public ArrayList Characters;
		[JsonProperty(PropertyName = "Locations")]
		public ArrayList Locations;
		[JsonIgnore]
		public bool isCurrent;
		[JsonProperty(PropertyName = "Name")]
		public String Name;
		[JsonProperty(PropertyName = "Location1")]
		public Location Location1;
		[JsonProperty(PropertyName = "Location2")]
		public Location Location2;
		[JsonProperty(PropertyName = "Description")]
		public string Description { get; set; }
		[JsonProperty(PropertyName = "Id")]
		public string ID;
		[JsonProperty(PropertyName = "Value")]
		public float Value;
		[JsonIgnore]
		public Texture2D Texture;

		public Trail(Location node1, Location node2, float value, String name, Texture2D texture)
		{
			isCurrent = false;
			Name = name;
			Texture = texture;
			//Characters = Characters;
			sprite = new AnimatedSprite(texture, 2, 2, Color.White);
			Location1 = node1;
			Location2 = node2;
			Value = value;
		}

        public void Update(GameTime gameTime)
		{
			/*if (!isCurrent)
			{
				sprite.Texture = LocationTexture;
			}*/
			sprite.Update(gameTime);



		}

		public void Draw(SpriteBatch spriteBatch)
		{
			DrawLine(spriteBatch, Location1.Coords, Location2.Coords, Color.Red);
		}

		public void DrawLine(SpriteBatch spriteBatch, Vector3 begin, Vector3 end, Color color, int width = 1)
		{
			Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
			Vector3 v = Vector3.Normalize(begin - end);
			float angle = (float)Math.Acos(Vector3.Dot(v, -Vector3.UnitX));
			if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
			spriteBatch.Draw(Texture, r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
		}

    }


}