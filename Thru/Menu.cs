using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{ 
	public class Menu : IGameView
	{
		Button newGameButton, mainSettingsButton, loadGameButton;
		

		private Color background_color = Color.Red;
		private ButtonGroup buttonGroup;
		private ContentManager Content;
		public Menu (int clientWidth, int clientHeight, IServiceProvider services )
		{

			Content = new ContentManager(services, "Content");

			Texture2D easy =
					   Content.Load<Texture2D>(@"images/easy");
			Texture2D medium =
				Content.Load<Texture2D>(@"images/medium");
			Texture2D hard =
				Content.Load<Texture2D>(@"images/hard");
			newGameButton = new Button(easy);
			mainSettingsButton = new Button(medium);
			loadGameButton = new Button(hard);
			buttonGroup = new ButtonGroup(new Button[] { newGameButton, mainSettingsButton, loadGameButton }, clientWidth, clientHeight);


			
			
		
		}
		public State Update(GameTime gameTime) {

			buttonGroup.Update(gameTime);
			if(newGameButton.State == BState.JUST_RELEASED)
            {
				//todo: add stuff like return State.NewGame
				return State.Menu;
            } else if (mainSettingsButton.State == BState.JUST_RELEASED)
			{
				return State.MainSettings;
			} else if (loadGameButton.State == BState.JUST_RELEASED)
			{
				return State.Menu;
			}

			return State.Menu;
		}




		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) {
			_graphics.GraphicsDevice.Clear(background_color);
			buttonGroup.Draw(_spriteBatch);

		}



	}

}