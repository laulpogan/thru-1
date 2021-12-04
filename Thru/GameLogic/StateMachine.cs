/*using System;
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


    public class StateMachine
    {
        private GraphicsDeviceManager graphics;
    
        enum StateMode
        {
            Update,
            Draw
        }
        public State currentState
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    Console.WriteLine("Changed state from " + state + " to " + value);
                }
                state = value;
            }
        }
        private State state;
        public enum States { };
        public StateMachine(Enum states, GraphicsDeviceManager graphics)
        {
            graphics = graphics;
            States = states;
            foreach (var i in Enum.GetValues(typeof(States)))
            {
                string name = Enum.GetName(typeof(State), i);
                if (currentState.ToString() == name)
                {
                    State thing = new State();
                };
            }
            currentState = State.Menu;

        }

        protected void Update(GameTime gameTime)
        {
            stateMachine(gameTime, StateMode.Update);
        }

        protected void Draw(GameTime gameTime)
        {
            stateMachine(gameTime, StateMode.Draw);
        }

        private void stateMachine(GameTime gameTime, StateMode stateMode)
        {

            foreach (var i in Enum.GetValues(typeof(States)))
            {
                string name = Enum.GetName(typeof(State), i);
                if (currentState.ToString() == name)
                {
                    State thing = new State();
                };
            }


        }

        private State? runState(IGameView gameView, StateMode stateMode, GameTime gameTime)
        {
            switch (stateMode)
            {
                case StateMode.Update:
                    return gameView.Update(gameTime);
                case StateMode.Draw:
                    gameView.Draw(graphics);
                    return null;
                default:
                    break;
            }
            return null;
        }
    }
}
*/