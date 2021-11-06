using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class GlobalState 
	{


		public SpriteBatch spriteBatch;
		public Location currentLocation;
		public MapGameView mapView;
		GameViewStateMachine stateMachine;
		public GameTime gameTime;
		public PlayGameView playView;
		public int Days;
		public ArrayList localWeather;
		public GlobalState(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
		{
			mapView = new MapGameView(services, clientWidth, clientHeight, graphics);
			currentLocation = mapView.currentLocation;
			playView = new PlayGameView(services, graphics, currentLocation);
			playView.player.location = currentLocation;
			stateMachine = new GameViewStateMachine(services, graphics, mapView, playView);
			stateMachine.currentState = GameState.Play;
		}



		public State Update(GameTime gameTime)
		{

			currentLocation = mapView.currentLocation;
			playView.currentLocation = currentLocation;
			playView.player.location = currentLocation;
			stateMachine.Update(gameTime);


			return State.Game;
		}




		



	}

}