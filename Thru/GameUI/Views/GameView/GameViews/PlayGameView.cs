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
		public Texture2D background;
		public Encounter Encounter;
		public HUD hud;
		public DesignGrid grid;
		public Player player;
		public Location currentLocation;
		private ContentManager Content;
		private SpriteFont font;

		public PlayGameView(IServiceProvider services, GraphicsDeviceManager graphics)
{
			Graphics = graphics;
			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			font = Content.Load<SpriteFont>("Score");
			background = Content.Load<Texture2D>("southern_terminus");
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			Encounter = setupTestEncounter(services, graphics);
			hud = new HUD(services, graphics, player);

			grid = new DesignGrid(services, graphics);

		}
		public Encounter setupTestEncounter(IServiceProvider services, GraphicsDeviceManager graphics)
		{

			CharacterBuilder CharacterBuilder = new CharacterBuilder(services, graphics, currentLocation);
			player = CharacterBuilder.createCharacter();
			EncounterData data = new EncounterData();
			EncounterResolutionData resolutionPos = new EncounterResolutionData("Morale", 1, null, null);
			EncounterResolutionData resolutionNeg = new EncounterResolutionData("Morale", -1, null, null);

			EncounterConsequenceData consequence = new EncounterConsequenceData(resolutionPos, resolutionNeg);
			EncounterOptionData option1 = new EncounterOptionData("option1", "Morale", 50, consequence);
			EncounterOptionData option2 = new EncounterOptionData("option2", "Speed", 10, consequence);
			EncounterOptionData option3 = new EncounterOptionData("option3", "Chillness", 15, consequence);
			EncounterOptionData[] opts = new EncounterOptionData[] { option1, option2, option3 };
			data.text = "Sampletext";
			data.title = "SampleTitle";
			data.options = opts;
			data.dropRate = 4;
			data.locationType = LocationType.Desert;



			return new Encounter(player, data, null, services, graphics);

		}

		public GameState Update(GameTime gameTime)
		{
			Encounter.Update(gameTime);
			hud.Update(gameTime);
			GameState returnState = GameState.Play;
			if (hud.mainMenuButton.State == BState.JUST_RELEASED)
				returnState = GameState.Play;
			if (hud.mapButton.State == BState.JUST_RELEASED)
				returnState = GameState.Map;
			
			return returnState;
		}
		public void Draw(GraphicsDeviceManager _graphics)
		{
			spriteBatch.Begin();
			spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
			Encounter.Draw(spriteBatch);
			grid.Draw(spriteBatch);
			spriteBatch.End();
			

			hudBatch.Begin();
			hud.Draw(hudBatch);
			hudBatch.DrawString(font, $"Current Location: [{currentLocation.ID}] {currentLocation.Name}", new Vector2(400, 20), Color.Black);
			hudBatch.End();
		}

	}
}
