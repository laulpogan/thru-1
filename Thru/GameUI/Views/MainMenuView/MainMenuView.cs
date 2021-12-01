using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{ 
	public class MainMenuView : IView
	{
		Button newGameButton, mainSettingsButton, loadGameButton, characterCreationButton;
		

		private ButtonGroup buttonGroup;
		private ContentManager Content;
		public SpriteBatch spriteBatch;
		public AnimatedSprite background;
		public MainMenuView(IServiceProvider services, GraphicsDeviceManager graphics, GlobalState globalState)
		{

			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			Texture2D buttonImage = Content.Load<Texture2D>("InterfaceTextures/short_button");
			SpriteFont font = Content.Load<SpriteFont>("Score");
			ArrayList buttonList = new ArrayList();
			newGameButton = new Button(buttonImage, "New Game", font, null, globalState.Game);
			mainSettingsButton = new Button(buttonImage, "Main Settings", font,null,  globalState.MainSettings);
			loadGameButton = new Button(buttonImage, "Load Game", font, null, globalState.Game);
			characterCreationButton = new Button(buttonImage, "Character Creation", font, null, globalState.CharacterCreation);
			buttonList.Add(newGameButton);
			buttonList.Add(mainSettingsButton);
			buttonList.Add(loadGameButton);
			buttonList.Add(characterCreationButton);
			buttonGroup = new ButtonGroup(buttonList, new Vector2(100,100));

			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			background = new AnimatedSprite(Content.Load<Texture2D>("thru-switch-sheet"), 2, 2, Color.White);



		}
		public State Update(GameTime gameTime) {

			buttonGroup.Update(gameTime);
			background.Update(gameTime);
			if (mainSettingsButton.State == BState.JUST_RELEASED)
			{
				return State.MainSettings;
			} else if (loadGameButton.State == BState.JUST_RELEASED)
			{
				return State.Game;
			} else if (newGameButton.State == BState.JUST_RELEASED)
            {
				return State.Game;
            }
			else if (characterCreationButton.State == BState.JUST_RELEASED)
			{
				return State.CharacterCreation;
			}

			return State.Menu;
		}




		public void Draw(GraphicsDeviceManager _graphics) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, new Vector2(650, 40), 1f);
			buttonGroup.Draw(spriteBatch);
			spriteBatch.End();

		}



	}

}