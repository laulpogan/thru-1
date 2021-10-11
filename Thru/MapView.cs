using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class MapView : IGameView
	{
		public Location currentLocation;
		public State State = State.Game;
		public Button menuButton, mapButton;
		public Texture2D buttonImage;
		public ContentManager Content;
		public MapMenu MapMenu;
		public ButtonGroup buttonGroup;
		MapDataHandler mapHandler;
		public MapView mapView;
		public Graph gameMap;

		public MapView( IServiceProvider services, int width, int height, GraphicsDeviceManager graphics)
		{
			
			mapHandler = new MapDataHandler(width, height, services);
			gameMap = mapHandler.getGameMap();
			currentLocation = (Location)gameMap.Locations.ToArray()[1];
			Content = new ContentManager(services, "Content");
			MapMenu = new MapMenu(services, currentLocation, graphics);
			buttonImage = Content.Load<Texture2D>("longbutton");
			mapButton = new Button(buttonImage);
			mapButton.Text = "Map";
			mapButton.Font = Content.Load<SpriteFont>("Score");
			MapMenu.buttonGroup.ButtonList.Add(mapButton);
			menuButton = new Button(buttonImage);
			menuButton.Text = "Menu";
			menuButton.Font = Content.Load<SpriteFont>("Score");
			MapMenu.buttonGroup.ButtonList.Add(menuButton);
			ArrayList tempList = new ArrayList();
			tempList.Add(mapButton);
			tempList.Add(menuButton);
			buttonGroup = new ButtonGroup(tempList, new Vector2(100, 600));

		}
		public  State Update(GameTime gameTime)
        {
			mapHandler.Update(gameTime);
			gameMap.Update(gameTime);

			buttonGroup.Update(gameTime);
			if (MapMenu.ShowMap) {
				currentLocation = MapMenu.Update(gameTime);
				return State.Map;
			}
			if (menuButton.State == BState.JUST_RELEASED)
			{
				Console.Write("Menu Button Press" );
				State = State.Game;
				return State.Menu;
			}
			if (mapButton.State == BState.JUST_RELEASED)
			{

				MapMenu.ShowMap = !MapMenu.ShowMap;
				Console.Write("Show Map: " + MapMenu.ShowMap);

			}
			

			return State;
        }
		public  void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics)
        {
			//Location.Draw(_spriteBatch);
			mapHandler.Draw(_spriteBatch);
			gameMap.Draw(_spriteBatch);

			MapMenu.Draw(_spriteBatch);

            if (!MapMenu.ShowMap)
            {
				buttonGroup.Draw(_spriteBatch);

			}


		}

	}
}
