using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using XColor = Microsoft.Xna.Framework.Color;
using CColor = System.Drawing.Color;
using KColor = System.Drawing.KnownColor;
using System.Text;

namespace Thru {
	public static class ThruLib
	{
		// wrapper for hit_image_alpha taking Rectangle and Texture
		public static Boolean hit_image_alpha(Rectangle rect, Texture2D tex, int x, int y)
		{
			return hit_image_alpha(0, 0, tex, tex.Width * (x - rect.X) /
				rect.Width, tex.Height * (y - rect.Y) / rect.Height);
		}

		// wraps hit_image then determines if hit a transparent part of image 
		public static  Boolean hit_image_alpha(float tx, float ty, Texture2D tex, int x, int y)
		{
			if (hit_image(tx, ty, tex, x, y))
			{
				uint[] data = new uint[tex.Width * tex.Height];
				tex.GetData<uint>(data);
				if ((x - (int)tx) + (y - (int)ty) *
					tex.Width < tex.Width * tex.Height)
				{
					return ((data[
						(x - (int)tx) + (y - (int)ty) * tex.Width
						] &
								0xFF000000) >> 24) > 20;
				}
			}
			return false;
		}

		// determine if x,y is within rectangle formed by texture located at tx,ty
		public static Boolean hit_image(float tx, float ty, Texture2D tex, int x, int y)
		{
			return (x >= tx &&
				x <= tx + tex.Width &&
				y >= ty &&
				y <= ty + tex.Height);
		}
		
		public static XColor colorFromString(string color)
        {
			CColor clrColor = CColor.FromName(color);
			XColor xColor = XNAColor(clrColor);
			return xColor;
		}

		public static XColor fromKColor(KColor color)
        {
			return colorFromString(color.ToString());
        }
		public static XColor[] allColors()
		{
			KColor[] colors = (KColor[])Enum.GetValues(typeof(KColor));
			XColor[] XNAcolors = new XColor[colors.Length];
			for (int i = 0; i < colors.Length; i++)
            {
				XNAcolors[i] = fromKColor(colors[i]);
            }

			return XNAcolors; 
		}

		public static XColor XNAColor(CColor color)
		{
			return new XColor(color.R, color.G, color.B, color.A);
		}

		//todo:fix this
		public static string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
		{
			string[] words = text.Split(' ');
			StringBuilder sb = new StringBuilder();
			float lineWidth = 0f;
			float spaceWidth = spriteFont.MeasureString(" ").X;

			foreach (string word in words)
			{
				Vector2 size = spriteFont.MeasureString(word);

				if (lineWidth + size.X < maxLineWidth)
				{
					sb.Append(word + " ");
					lineWidth += size.X + spaceWidth;
				}
				else
				{
					sb.Append("\n" + word + " ");
					lineWidth = size.X + spaceWidth;
				}
			}

			return sb.ToString();
		}


		public static Texture2D makeBlankRect(GraphicsDeviceManager graphics, int x, int y)
        {
			Texture2D rect = new Texture2D(graphics.GraphicsDevice, x, y);
			Color[] data = new Color[rect.Bounds.Width * rect.Bounds.Height];
			for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
			rect.SetData(data);
			return  rect;
		}
		public static BState getMouseState(bool mpressed, bool prev)
        {
			BState State = new BState();
			State = BState.UP;
			if (mpressed)
			{
				// mouse is currently down
				State = BState.DOWN;
			}
			else if (!mpressed && prev)
			{
				// mouse was just released
				if (State == BState.DOWN)
				{
					// button i was just down
					State = BState.JUST_RELEASED;
				}
			}
			else
			{
				State = BState.HOVER;
			}

			return State;
		}

}


	}




