using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

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
		public Character Player;
		public Location currentLocation, TrailLocation;
		private ContentManager Content;
		private SpriteFont font;
		public MapMenu mapMenu;
		public SoundEffect eatingSoundEffect;
		public GlobalState GlobalState;


		public PlayGameView(IServiceProvider services, GraphicsDeviceManager graphics, Location location, Location trailLocation, Character player, GlobalState globalState )
{
			GlobalState = globalState;
			Graphics = graphics;
			currentLocation = location;
			TrailLocation = trailLocation;
			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			font = Content.Load<SpriteFont>("Score");
			background = new BackgroundModel(services,graphics,0,0);
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			Player = player;
			Encounter = setupTestEncounter(services, graphics);
			hud = new HUD(services, graphics, Player, globalState);
			mapMenu = new MapMenu(services, graphics, currentLocation, new Vector2(250, 850),globalState, Player);
			grid = new DesignGrid(services, graphics);
			eatingSoundEffect = Content.Load<SoundEffect>("Audio/MunchMunch");
			hud.snackButton.onClick += playMunch;

		}


		public void playMunch(object sender, EventArgs e) 
        {
			eatingSoundEffect.Play();
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



			return new Encounter(Player, data, null, services, graphics, GlobalState) ;

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
			if (hud.inventoryButton.State == BState.JUST_RELEASED)
				returnState = GameState.Inventory;
			if (hud.snackButton.State == BState.JUST_RELEASED)
            {
			//	eatingSoundEffect.Play();
				Player.Stats.Energy += 5;
				Player.Stats.Snacks = Player.Stats.Snacks - 1;
			}
			
			Player.Update(gameTime);	
			return returnState;
		}
		public void Draw(GraphicsDeviceManager graphics)
		{
			spriteBatch.Begin();
			background.Draw(graphics);
			Encounter.Draw(spriteBatch);
			//grid.Draw(spriteBatch);
			Player.Draw(spriteBatch);
			spriteBatch.End();



			hudBatch.Begin();
			hud.Draw(hudBatch);
			mapMenu.Draw(hudBatch);
			hudBatch.DrawString(font, $"Current Location: [{currentLocation.ID}] {currentLocation.Name}", new Vector2(400, 20), Color.Black);
			hudBatch.End();
		}

	}
}
