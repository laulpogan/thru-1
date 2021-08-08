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

			Texture2D buttonImage = Content.Load<Texture2D>("longButton");
			SpriteFont font = Content.Load<SpriteFont>("Score");
			newGameButton = new Button(buttonImage, "New Game", font);
			mainSettingsButton = new Button(buttonImage, "Main Settings", font);
			loadGameButton = new Button(buttonImage, "Load Game", font);
			buttonGroup = new ButtonGroup(new Button[] { newGameButton, mainSettingsButton, loadGameButton }, new Vector2(100,100));


			
			
		
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
				return State.Game;
			}

			return State.Menu;
		}




		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) {
			_graphics.GraphicsDevice.Clear(background_color);
			buttonGroup.Draw(_spriteBatch);

		}



	}

}