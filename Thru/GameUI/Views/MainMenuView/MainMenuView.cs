using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{ 
	public class MainMenuView : IGameView
	{
		Button newGameButton, mainSettingsButton, loadGameButton;
		

		private ButtonGroup buttonGroup;
		private ContentManager Content;
		public SpriteBatch spriteBatch;
		public MainMenuView (int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
		{

			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			Texture2D buttonImage = Content.Load<Texture2D>("longButton");
			SpriteFont font = Content.Load<SpriteFont>("Score");
			ArrayList buttonList = new ArrayList();
			newGameButton = new Button(buttonImage, "New Game", font);
			mainSettingsButton = new Button(buttonImage, "Main Settings", font);
			loadGameButton = new Button(buttonImage, "Load Game", font);
			buttonList.Add(newGameButton);
			buttonList.Add(mainSettingsButton);
			buttonList.Add(loadGameButton);
			buttonGroup = new ButtonGroup(buttonList, new Vector2(100,100));

			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);




		}
		public State Update(GameTime gameTime) {

			buttonGroup.Update(gameTime);
			
			if (mainSettingsButton.State == BState.JUST_RELEASED)
			{
				return State.MainSettings;
			} else if (loadGameButton.State == BState.JUST_RELEASED)
			{
				return State.Map;
			} else if (newGameButton.State == BState.JUST_RELEASED)
            {
				return State.CharacterCreation;
            }

			return State.Menu;
		}




		public void Draw(GraphicsDeviceManager _graphics) {
			spriteBatch.Begin();
			buttonGroup.Draw(spriteBatch);
			spriteBatch.End();

		}



	}

}