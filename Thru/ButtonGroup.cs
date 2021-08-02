using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Thru
{ 	

	public class ButtonGroup
		{
			public ArrayList ButtonList;
		private int buttonHeight;
		private int buttonWidth;
		private int heightTracker;
			public ButtonGroup(Button[] buttonList, int clientWidth, int clientHeight)
			{
				ButtonList = new ArrayList();
				foreach ( Button i in buttonList) {
					buttonHeight = i.Bounds.Height;
					buttonWidth = i.Bounds.Width;
					int y = clientHeight / 2 -
					buttonList.Length / 2 * buttonHeight -
					(buttonList.Length % 2) * buttonHeight / 2;
					heightTracker += y + buttonHeight;
					i.Bounds.Y = heightTracker;
					i.Bounds.X = clientWidth / 2 - buttonWidth / 2;
					ButtonList.Add(i);
				Console.WriteLine(i.Bounds.X + " " + i.Bounds.Y);
			}

		}

		public void Update(GameTime gameTime)
			{
			foreach (Button button in ButtonList)
			{
				button.Update(gameTime);
			}

		}

		public void Draw(SpriteBatch spriteBatch)
			{
		
				foreach (Button button in ButtonList){
					button.Draw(spriteBatch);
				}

			}
	}
}
