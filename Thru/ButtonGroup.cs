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
		public Vector2 Origin;
			public ButtonGroup(Button[] buttonList, Vector2 origin)
			{
			Origin = origin;
				ButtonList = new ArrayList();
			heightTracker = (int)origin.Y;
			
				foreach ( Button i in buttonList) {
					
					buttonHeight = i.Bounds.Height;
					buttonWidth = i.Bounds.Width;
				Console.WriteLine("button Width: " + buttonWidth + " button Height: " + buttonHeight);

				i.Bounds.Y = heightTracker;
				heightTracker += buttonHeight + 10;
				i.Bounds.X = (int)origin.X;
				Console.WriteLine("Name: "+ i.Text+" BoundsX: " + i.Bounds.X + " BoundsY: "+ i.Bounds.Y  );
					ButtonList.Add(i);
			}

		}
		public void updatePositions(GameTime gameTime)
        {
			heightTracker = (int)Origin.Y;

			foreach (Button button in ButtonList)
			{

				buttonHeight = button.Bounds.Height;
				buttonWidth = button.Bounds.Width;
				button.Bounds.Y = heightTracker;
				heightTracker += buttonHeight + 10;
				button.Bounds.X = (int)Origin.X;
				button.Update(gameTime);

			}
		
		}
		public void Update(GameTime gameTime)
			{
			updatePositions(gameTime);

		}

		public void Draw(SpriteBatch spriteBatch)
			{
		
				foreach (Button button in ButtonList){
					button.Draw(spriteBatch);
				}

			}
	}
}
