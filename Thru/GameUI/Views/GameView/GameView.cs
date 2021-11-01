using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class GameView : IGameView
	{
		Button  mainMenuButton;


		private ButtonGroup buttonGroup;
		private ContentManager Content;
		public SpriteBatch spriteBatch;
		public AnimatedSprite background;
		public Encounter Encounter;
		public GameView(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
		{
			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			Texture2D buttonImage = Content.Load<Texture2D>("longButton");
			SpriteFont font = Content.Load<SpriteFont>("Score");
			ArrayList buttonList = new ArrayList();
			mainMenuButton = new Button(buttonImage, "Main Menu", font);
			buttonList.Add(mainMenuButton);
			buttonGroup = new ButtonGroup(buttonList, new Vector2(100, 100));

			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			background = new AnimatedSprite(Content.Load<Texture2D>("thru-switch-sheet"), 2, 2);
			Encounter = setupTestEncounter(services, graphics);


		}

        public Encounter setupTestEncounter(IServiceProvider services, GraphicsDeviceManager graphics)
        {

			CharacterBuilder CharacterBuilder = new CharacterBuilder(services, graphics);
			Player player = CharacterBuilder.createCharacter();
			EncounterData data = new EncounterData();

			EncounterOptionData option1 = new EncounterOptionData("option1", "Morale", 50, null);
			EncounterOptionData option2 = new EncounterOptionData("option2", "Speed", 10, null);
			EncounterOptionData option3 = new EncounterOptionData("option3", "Chillness", 15, null);
			EncounterOptionData[] opts = new EncounterOptionData[] { option1, option2, option3 };
			data.text = "Sampletext";
			data.title = "SampleTitle";
			data.options = opts;
			data.dropRate = 4;
			data.locationType = LocationType.Desert ;
			return new Encounter(player, data, services, graphics);

		}


		public State Update(GameTime gameTime)
		{

			buttonGroup.Update(gameTime);
			background.Update(gameTime);
			Encounter.Update(gameTime);
			if (mainMenuButton.State == BState.JUST_RELEASED)
			{
				return State.Menu;
			}
			

			return State.Game;
		}




		public void Draw(GraphicsDeviceManager _graphics)
		{
			spriteBatch.Begin();
			background.Draw(spriteBatch, new Vector2(650, 40), 1f);
			buttonGroup.Draw(spriteBatch);
			Encounter.Draw(spriteBatch);
			spriteBatch.End();

		}



	}

}