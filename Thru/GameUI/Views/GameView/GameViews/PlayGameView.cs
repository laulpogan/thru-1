using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;


namespace Thru
{
	public class PlayGameView : IGameView
	{
		public GraphicsDeviceManager Graphics;
		public SpriteBatch spriteBatch, hudBatch;
		public BackgroundModel background;
		public Encounter Encounter;
		public HUD hud;
		public DesignGrid grid;
		public Player player;
		public Location currentLocation;
		private ContentManager Content;
		private SpriteFont font;
		public MapMenu mapMenu;


		public PlayGameView(IServiceProvider services, GraphicsDeviceManager graphics, Location location)
{
			Graphics = graphics;
			currentLocation = location;
			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			font = Content.Load<SpriteFont>("Score");
			background = new BackgroundModel(services,graphics,0,0);
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			player = setupTestPlayer(services, graphics);
			Encounter = setupTestEncounter(services, graphics);
			hud = new HUD(services, graphics, player);
			mapMenu = new MapMenu(services, graphics, currentLocation, new Vector2(250, 850), player);
			grid = new DesignGrid(services, graphics);

		}

		public Player setupTestPlayer(IServiceProvider services, GraphicsDeviceManager graphics)
        {
			CharacterBuilder CharacterBuilder = new CharacterBuilder(services, graphics, currentLocation);
			return  CharacterBuilder.createCharacter();
		}
		public Encounter setupTestEncounter(IServiceProvider services, GraphicsDeviceManager graphics)
		{

			
			EncounterData data = new EncounterData();
			EncounterResolutionData success = new EncounterResolutionData("Morale", 1, null, null);
			EncounterResolutionData failure = new EncounterResolutionData("Morale", -1, null, null);

			EncounterOptionData option1 = new EncounterOptionData("option1", "Morale", 50, success, failure);
			EncounterOptionData option2 = new EncounterOptionData("option2", "Speed", 10, success, failure);
			EncounterOptionData option3 = new EncounterOptionData("option3", "Chillness", 15, success, failure);
			EncounterOptionData[] opts = new EncounterOptionData[] { option1, option2, option3 };
			data.text = "Sampletext";
			data.title = "SampleTitle";
			data.options = opts;
			data.dropRate = 4;
			data.encounterTags = new Tags[] { Tags.Desert };



			return new Encounter(player, data, null, services, graphics);

		}

		public GameState Update(GameTime gameTime)
		{
			Encounter.Update(gameTime);
			hud.Update(gameTime);
			background.Update(gameTime);
			currentLocation = mapMenu.Update(gameTime);
			if(currentLocation.Tags[0] == Tags.Town)
            {

            }
			GameState returnState = GameState.Play;
			if (hud.mainMenuButton.State == BState.JUST_RELEASED)
				returnState = GameState.Play;
			if (hud.mapButton.State == BState.JUST_RELEASED)
				returnState = GameState.Map;
			if (hud.snackButton.State == BState.JUST_RELEASED)
            {
				player.stats.Energy += 5;
				player.stats.Snacks = player.stats.Snacks - 1;
			}
			player.Update(gameTime);	
			return returnState;
		}
		public void Draw(GraphicsDeviceManager _graphics)
		{
			spriteBatch.Begin();
			background.Draw(_graphics);
			Encounter.Draw(spriteBatch);
			//grid.Draw(spriteBatch);
			spriteBatch.End();

			player.Draw(_graphics);


			hudBatch.Begin();
			hud.Draw(hudBatch);
			mapMenu.Draw(hudBatch);
			hudBatch.DrawString(font, $"Current Location: [{currentLocation.ID}] {currentLocation.Name}", new Vector2(400, 20), Color.Black);
			hudBatch.End();
		}

	}
}
