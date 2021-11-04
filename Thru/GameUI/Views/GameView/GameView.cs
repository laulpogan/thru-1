using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class GameView : IView
	{


		private ContentManager Content;
		public SpriteBatch spriteBatch;
		public Texture2D background;
		public Encounter Encounter;
		public HUD hud;
		public DesignGrid grid;
		public Player player;
		public Location currentLocation;
		public MapView mapView;
		GameViewStateMachine stateMachine;
		public GameTime gameTime;
		public GameView(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
		{
			Content = new ContentManager(services, "Content");
			Content.RootDirectory = "Content";
			background = Content.Load<Texture2D>("southern_terminus");
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			Encounter = setupTestEncounter(services, graphics);
			hud = new HUD(services, graphics, player);
			grid = new DesignGrid( services, graphics);	
			mapView = new MapView(services, clientWidth, clientHeight, graphics);
			stateMachine = new GameViewStateMachine(services, graphics, mapView);
		}

		public Encounter setupTestEncounter(IServiceProvider services, GraphicsDeviceManager graphics)
        {

			CharacterBuilder CharacterBuilder = new CharacterBuilder(services, graphics);
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
			data.locationType = LocationType.Desert ;


			
			return new Encounter(player, data, null, services, graphics);

		}


		public State Update(GameTime gameTime)
		{
			stateMachine.Update(gameTime);
			Encounter.Update(gameTime);
			hud.Update(gameTime);
			if (hud.mainMenuButton.State == BState.JUST_RELEASED)
				return State.Menu;
			if (hud.mapButton.State == BState.JUST_RELEASED)
            {
				if (stateMachine.currentState != GameState.Map)
					stateMachine.currentState = GameState.Map;
				else
					stateMachine.currentState = GameState.Play;
				return State.Game;
			}

				
			

			return State.Game;
		}




		public void Draw(GraphicsDeviceManager _graphics)
		{

			spriteBatch.Begin();
			spriteBatch.Draw(background, new Vector2(0,0), Color.White);
			Encounter.Draw(spriteBatch);
			hud.Draw(spriteBatch);
			grid.Draw(spriteBatch);
			stateMachine.Draw(gameTime);

			spriteBatch.End();

		}



	}

}