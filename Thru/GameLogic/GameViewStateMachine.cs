using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using Newtonsoft.Json;
using System.Reflection.Emit;

namespace Thru
{


    public class GameViewStateMachine
    {
        private GraphicsDeviceManager Graphics;
        public IGameView currentView;
        public MapGameView MapView;
        public PlayGameView PlayView;
        public InventoryGameView InventoryView;


        enum StateMode
        {
            Update,
            Draw
        }
        public GameState currentState
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    Console.WriteLine("Changed GameState from " + state + " to " + value);
                }
                state = value;
            }
        }
        private GameState state;
        
        public GameViewStateMachine(IServiceProvider services, GraphicsDeviceManager graphics, MapGameView mapView, PlayGameView playView, InventoryGameView inventoryView)
        {
            Graphics = graphics;
            currentState = GameState.Play;
            MapView = mapView;
            PlayView = playView;
            InventoryView = inventoryView;
            
        }

        public void Update(GameTime gameTime)
        {
            stateMachine(gameTime, StateMode.Update);
        }

        public void Draw(GameTime gameTime)
        {
            stateMachine(gameTime, StateMode.Draw);
        }

        private void stateMachine(GameTime gameTime, StateMode stateMode)
        {
            switch (currentState)
            {
                case GameState.Pause:
                  //  currentState = runState(menu, stateMode, gameTime) ?? currentState;
                    break;
                case GameState.Settings:
                  //  currentState = runState(mainSettings, stateMode, gameTime) ?? currentState;
                    break;
                case GameState.Play:
                    currentState = runState(PlayView, stateMode, gameTime) ?? currentState;
                    break;
                case GameState.Map:
                    currentState = runState(MapView, stateMode, gameTime) ?? currentState;
                    break;
                case GameState.Encounter:
                 //   currentState = runState(characterCreationView, stateMode, gameTime) ?? currentState;
                    break;
                case GameState.Inventory:
                     currentState = runState(InventoryView, stateMode, gameTime) ?? currentState;
                    break;
                /*case State.Final:
					break;
				case State.Road:
					break;
				case State.Town:
					break;
				case State.Trailhead:
					break;
				case State.Start:
					break; */
                default:
                    Console.WriteLine("game is broken bucko: " + currentState);
                    break;
            }
            foreach (var i in Enum.GetValues(typeof(GameState)))
            {
                string name = Enum.GetName(typeof(State), i);
                if (currentState.ToString() == name)
                {
                    State thing = new State();
                };
            }


        }

        private GameState? runState(IGameView gameView, StateMode stateMode, GameTime gameTime)
        {
            switch (stateMode)
            {
                case StateMode.Update:
                    return gameView.Update(gameTime);
                case StateMode.Draw:
                    gameView.Draw(Graphics);
                    return null;
                default:
                    break;
            }
            return null;
        }
    }
}
