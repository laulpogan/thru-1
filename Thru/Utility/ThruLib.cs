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




}


	}



}

