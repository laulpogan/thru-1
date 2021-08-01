using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Thru
{ 	
	public class ButtonGroup
		{
			public ArrayList allMyButtons;
			public ButtonGroup(Button[] buttonList)
			{
				allMyButtons = new ArrayList();
				foreach ( Button i in buttonList) {
					allMyButtons.Add(i);
				}
			}

			public void Update()
			{
			}

			public void Draw(SpriteBatch spriteBatch, Vector2 location)
			{
				foreach (Button button in allMyButtons){
					button.Draw(spriteBatch, location) ;
				}

			}
	}
}
