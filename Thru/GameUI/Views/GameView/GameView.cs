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


		public Location currentLocation, trailLocation;
		public MapGameView mapView;
		GameViewStateMachine stateMachine;
		public GameTime gameTime;
		public PlayGameView playView;
		public GameView(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
		{
			mapView = new MapGameView(services, clientWidth, clientHeight, graphics);
			currentLocation = mapView.currentLocation;
			trailLocation = mapView.currentTrailLocation;
			playView = new PlayGameView(services, graphics, currentLocation, trailLocation);
			mapView.Player = playView.player;
			playView.player.location = currentLocation;
			playView.player.trailLocation = trailLocation;
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




		public void Draw(GraphicsDeviceManager _graphics)
		{

			
			stateMachine.Draw(gameTime);


		}



	}

}