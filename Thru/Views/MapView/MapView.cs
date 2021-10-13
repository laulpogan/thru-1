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
		public Location lastLocation;
		public State State = State.Game;
		public Button menuButton, mapButton;
		public Texture2D buttonImage;
		public ContentManager Content;
		public MapMenu mapMenu;
		public ButtonGroup buttonGroup;
		MapDataHandler mapHandler;
		public MapView mapView;
		public TrailMap gameMap;
		public SpriteBatch spriteBatch, hudBatch;
		public bool ShowMap;
		public Camera cam;
		public GraphicsDeviceManager Graphics;
		public MapView( IServiceProvider services, int width, int height, GraphicsDeviceManager graphics)
{
			Graphics = graphics;
			cam = new Camera(graphics.GraphicsDevice.Viewport);
			cam.Pos = new Vector2(width / 2, height / 2);


			mapHandler = new MapDataHandler(width, height, services);
			gameMap = mapHandler.getGameMap();
			
				currentLocation = (Location)gameMap.Locations.ToArray()[0];

			
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
			lastLocation = currentLocation;
			currentLocation = mapMenu.Update(gameTime);
			if( lastLocation != currentLocation)
            {
				cam.Pos = currentLocation.Coords;
			}
			cam.UpdateCamera(Graphics.GraphicsDevice.Viewport);
			State returnState = State.Map;
			if (ShowMap) {
				
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
			
		
			

			return returnState;
        }
		public  void Draw( GraphicsDeviceManager _graphics)
        {

			spriteBatch.Begin(SpriteSortMode.BackToFront,null,
					null,
					null,
					null,
					null,
					cam.Transform);
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
