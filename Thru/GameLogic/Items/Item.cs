using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class Item
	{

		Texture2D Image;
		Rectangle Rect;
		public Item(Texture2D image, Point point)
		{
			Image = image;
			Rect = Image.Bounds;
			Rect.Location = point;
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Image, new Vector2(Rect.Location.X, Rect.Location.Y),Rect, Color.Black, 0f, Vector2.Zero, 2f , SpriteEffects.None, 0f);
		}
	}

}
