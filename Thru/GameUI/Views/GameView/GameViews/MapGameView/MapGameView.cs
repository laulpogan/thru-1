using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Thru
{
	public class MapGameView : IGameView
	{
		public Location currentLocation, currentTrailLocation;
		public Location lastLocation, lastTrailLocation;
		public Location destinationTrailLocation;
		public State State = State.Game;
		public Button menuButton, mapButton;
		public Texture2D buttonImage;
		public ContentManager Content;
		public MapMenu mapMenu;
		public ButtonGroup buttonGroup;
		MapDataHandler mapHandler;
		public MapGameView mapView;
		public TrailMap gameMap, TrailMap;
		public SpriteBatch spriteBatch, hudBatch;
		public Camera cam;
		public GraphicsDeviceManager Graphics;
		private SpriteFont font;
		public Player Player;
		int mileCounter;
		int Value;
		public ArrayList Visited;
		Queue<Vector3> vertList;
		
		public MapGameView( IServiceProvider services, int width, int height, GraphicsDeviceManager graphics, Player player)
{
			Graphics = graphics;
			cam = new Camera(graphics.GraphicsDevice.Viewport);
			Player = player;
			Visited = new ArrayList();
			mapHandler = new MapDataHandler(width, height, services);
			gameMap = mapHandler.getGameMap();
			//todo: getTrailMap is FUBAR right now. Need to fix so our boi has a path
			TrailMap = mapHandler.getGameMap();
			vertList = mapHandler.getTrailPoints();
			currentLocation = (Location)gameMap.Locations[0];
            currentTrailLocation = (Location)TrailMap.Locations[0];
			
			Player.Location = currentLocation;
			Player.TrailLocation = currentTrailLocation;
			
			destinationTrailLocation = chooseRoute();
			Visited.Add(currentTrailLocation.ID);
			cam.Pos = currentLocation.CoordsXY;

			Content = new ContentManager(services, "Content");
			font = Content.Load<SpriteFont>("Score");
			mapMenu = new MapMenu(services, graphics, currentLocation, new Vector2(200,850));
			Queue<Vector3> trailCoords = mapHandler.getTrailPoints();
			//Player.MapCoords = trailCoords[0];
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			mileCounter = 0;
			Value =3;

		}



		public Location chooseRoute()
		{

			
			//Player.ScreenXY = ;
			var locs = Player.TrailLocation.AdjacentLocationsWithWeight();
			//choose where to go
			Console.WriteLine($"Number of Adjacent Locations: {locs.Count}");
			foreach (Location loc in locs.Keys)
			{
				if (lastTrailLocation is null || (loc.ID != lastTrailLocation.ID && !Visited.Contains(loc.ID)))
				{
					Console.WriteLine($"Current Location: {currentTrailLocation.Coords}\nDestination:{loc.Coords}");
					Value = (int)locs[loc];
					//loc.Coords = vertList.Dequeue();
					return loc;
				}
			}
			return null;
		}
		public void step()
        {

			var tempVec = vertList.Dequeue();
			Player.ScreenXY = new Vector2(tempVec.X, tempVec.Y);
			/*if (destinationTrailLocation.Coords.X > currentTrailLocation.Coords.X)
				Player.ScreenXY.X += 1;
			else if (destinationTrailLocation.Coords.X < currentTrailLocation.Coords.X)
				Player.ScreenXY.X -= 1;
			if (destinationTrailLocation.Coords.Y > currentTrailLocation.Coords.Y)
				Player.ScreenXY.Y += 1;
			else if (destinationTrailLocation.Coords.Y < currentTrailLocation.Coords.Y)
				Player.ScreenXY.Y -= 1;
		if (destinationTrailLocation.Coords == currentTrailLocation.Coords)
		{
			Console.WriteLine("Huzzah, inflection point at " + currentTrailLocation.Coords);
			lastTrailLocation = currentTrailLocation;
			currentTrailLocation = destinationTrailLocation;
			destinationTrailLocation = chooseRoute();
		}*/

		}


		public  GameState Update(GameTime gameTime)
        {
			
		   if(destinationTrailLocation is not null)
            {
				/*if (mileCounter > Value)
				{
					lastLocation = currentLocation;
					lastTrailLocation = currentTrailLocation;
					currentTrailLocation = destinationTrailLocation;
					Visited.Add(currentTrailLocation.ID);
					destinationTrailLocation = chooseRoute();
					mileCounter = 0;
				}*/
				step();

				mileCounter++;
			}
           
			currentLocation = mapMenu.Update(gameTime);

			mapHandler.Update(gameTime);
			gameMap.Update(gameTime);
			Player.Update(gameTime);
			destinationTrailLocation = mapMenu.Update(gameTime);
			if ( lastLocation != currentLocation)
            {
				cam.ResetZoom();
				cam.Pos = currentLocation.CoordsXY;
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
			Player.Draw(spriteBatch);
			spriteBatch.End();

			hudBatch.Begin();
			hudBatch.DrawString(font, $"Current Location: [{currentLocation.ID}] {currentLocation.Name}", new Vector2(400, 20), Color.Black);
			mapMenu.Draw(hudBatch);
			hudBatch.End();
		}

	}
}
