using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Thru
{
	public class MapGameView : IGameView
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
		public MapGameView mapView;
		public TrailMap gameMap;
		public SpriteBatch spriteBatch, hudBatch;
		public Camera cam;
		public GraphicsDeviceManager Graphics;
		private SpriteFont font;
		public MapGameView( IServiceProvider services, int width, int height, GraphicsDeviceManager graphics)
{
			Graphics = graphics;
			cam = new Camera(graphics.GraphicsDevice.Viewport);

			mapHandler = new MapDataHandler(width, height, services);
			gameMap = mapHandler.getGameMap();
			
			currentLocation = (Location)gameMap.Locations[0];
			cam.Pos = currentLocation.Coords;

			Content = new ContentManager(services, "Content");
			font = Content.Load<SpriteFont>("Score");
			mapMenu = new MapMenu(services, graphics, currentLocation, new Vector2(200,850));
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);

		}
		public  GameState Update(GameTime gameTime)
        {
			mapHandler.Update(gameTime);
			gameMap.Update(gameTime);
			lastLocation = currentLocation;
			currentLocation = mapMenu.Update(gameTime);
			if( lastLocation != currentLocation)
            {
				cam.ResetZoom();
				cam.Pos = currentLocation.Coords;
			}
			cam.UpdateCamera(Graphics.GraphicsDevice.Viewport);
			GameState returnState = GameState.Map;
			if (mapMenu.menuButton.State == BState.JUST_RELEASED)
			{
				Console.Write("Menu Button Press" );
				returnState = GameState.Play;
			}
			
			if(mapMenu.gameButton.State == BState.JUST_RELEASED)
            {
				returnState = GameState.Play;
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
			mapHandler.Draw(spriteBatch);
			gameMap.Draw(spriteBatch);
			spriteBatch.End();

			hudBatch.Begin();
			hudBatch.DrawString(font, $"Current Location: [{currentLocation.ID}] {currentLocation.Name}", new Vector2(400, 20), Color.Black);
			mapMenu.Draw(hudBatch);
			hudBatch.End();
		}

	}
}
