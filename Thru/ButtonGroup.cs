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
			public ButtonGroup(Button[] buttonList)
			{
				ButtonList = new ArrayList();
				foreach ( Button i in buttonList) {
					ButtonList.Add(i);
				}
			}

			public void Update()
			{
			foreach (Button button in ButtonList)
			{
				button.Update();
			}

		}

		public void Draw(SpriteBatch spriteBatch, Vector2 location)
			{
			Vector2 bound = new Vector2(0,0) ;
			Vector2 margin = new Vector2(50, 50);
				foreach (Button button in ButtonList){
					button.Draw(spriteBatch, location + bound + margin);
				bound += new Vector2(button.Bounds.Size.X, 0);
				}

			}
	}
}
