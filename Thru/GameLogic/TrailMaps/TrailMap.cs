using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using GeoJSON.Net.Feature;

namespace Thru
{
	public class TrailMap
	{
		public ArrayList Locations;
		public ArrayList Characters;
		public ArrayList Trails;
		public String Name;
		public Vector2 Origin;
		public TrailMap(ArrayList locations, ArrayList trails, String name)
		{
			Locations = locations;
			Trails = trails;
			Name = name;
			Origin = new Vector2(0,0);
		}

		public void Update(GameTime gameTime)
		{
			foreach (Location loc in Locations)
			{
				loc.Update(gameTime);
			}
			foreach (Trail tr in Trails)
			{
				tr.Update(gameTime);
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			foreach (Location loc in Locations)
			{
				loc.Draw(spriteBatch);
			}
			foreach (Trail tr in Trails)
			{
				tr.Draw(spriteBatch);
			}
		}
	}


}
