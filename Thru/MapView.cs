using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Nez.UI;
using Nez;

namespace Thru
{
	public class MapView : IGameView
	{
		public Location currentLocation;
		public State State = State.Game;
		public Button menuButton, mapButton;
		public Texture2D buttonImage;
		public ContentManager Content;
		public MapMenu mapMenu;
		public ButtonGroup buttonGroup;
		MapDataHandler mapHandler;
		public MapView mapView;
		public Graph gameMap;
		public SpriteBatch spriteBatch, hudBatch;
		public bool ShowMap;
		public Camera2d cam;

		public MapView( IServiceProvider services, int width, int height, GraphicsDeviceManager graphics)
{
			cam = new Camera2d(graphics);
			cam.Pos = new Vector2(width / 2, height / 2);


			mapHandler = new MapDataHandler(width, height, services);
			gameMap = mapHandler.getGameMap();
			currentLocation = (Location)gameMap.Locations.ToArray()[1];
			Content = new ContentManager(services, "Content");
			mapMenu = new MapMenu(services, currentLocation, graphics);
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			ShowMap = true;

		}
		public  State Update(GameTime gameTime)
        {
			mapHandler.Update(gameTime);
			gameMap.Update(gameTime);

			State returnState = State.Map;
			if (ShowMap) {
				currentLocation = mapMenu.Update(gameTime);
				returnState = State.Map;
			}
			if (mapMenu.menuButton.State == BState.JUST_RELEASED)
			{
				Console.Write("Menu Button Press" );
				returnState = State.Menu;
			}
			if (mapMenu.mapButton.State == BState.JUST_RELEASED)
			{

				ShowMap = !ShowMap;
				Console.Write("Show Map: " + ShowMap);

			}
			cam.Pos = currentLocation.Coords;
			//Graphics.GraphicsDevice.Viewport = new Viewport(newX, newY , width , height );
			//Coords = newOrigin;

			mapMenu.Update(gameTime);

			return returnState;
        }
		public  void Draw( GraphicsDeviceManager _graphics)
        {

			spriteBatch.Begin(SpriteSortMode.BackToFront,null,
					null,
					null,
					null,
					null,
					cam.get_transformation());
			if (ShowMap)
			{
				mapHandler.Draw(spriteBatch);
				gameMap.Draw(spriteBatch);
			}
			spriteBatch.End();

			hudBatch.Begin();
			mapMenu.Draw(hudBatch);
			hudBatch.End();


		}

	}
}
