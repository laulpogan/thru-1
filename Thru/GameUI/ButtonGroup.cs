using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Thru
{
	public enum ButtonArrangement
	{
		Horizontal,
		Vertical
	}
	public class ButtonGroup
	{

		public ArrayList ButtonList;
		private ButtonArrangement _arrangement;
		private Vector2 _origin;
		private int _spacing;

		public ButtonGroup(ArrayList buttonList, Vector2 origin, ButtonArrangement arrangement = ButtonArrangement.Vertical, int spacing_px = 10)
		{
			ButtonList = new ArrayList();
			_arrangement = arrangement;
			_origin = origin;
			_spacing = spacing_px;

			foreach (Button button in buttonList)
			{
				ButtonList.Add(button);
			}
			updatePositions();
		}

		public void updatePositions()
		{
			if (_arrangement == ButtonArrangement.Vertical)
			{
				int heightTracker = (int)_origin.Y;
				foreach (Button button in ButtonList)
				{

					var buttonHeight = button.Bounds.Height;
					button.Bounds.Y = heightTracker;
					heightTracker += buttonHeight + _spacing;
					button.Bounds.X = (int)_origin.X;

				}
			}
			else if (_arrangement == ButtonArrangement.Horizontal)
			{
				int widthTracker = (int)_origin.X;
				foreach (Button button in ButtonList)
				{
					var buttonWidth = button.Bounds.Width;
					button.Bounds.Y = (int)_origin.Y;
					widthTracker += buttonWidth + _spacing;
					button.Bounds.X = widthTracker;
				}
			}
			else
			{
				// big error
			}
		}

		public void Update(GameTime gameTime)
		{
			updatePositions();
			foreach (Button button in ButtonList)
			{
				button.Update(gameTime);
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{

			foreach (Button button in ButtonList)
			{
				button.Draw(spriteBatch);
			}

		}
	}
}
