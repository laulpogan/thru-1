using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;

namespace Thru
{
    public class GlobalState
    {
        enum StateMode
        {
            Update,
            Draw
        }

        public SpriteBatch spriteBatch;
        public Location currentLocation;
        public GameTime gameTime;
        public int Days;
        public ArrayList localWeather;
        private GraphicsDeviceManager Graphics;

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
        private MainMenuView menu;
        private MainSettingsView mainSettings;
        private ContentManager Content;

        public CharacterCreationView characterCreationView;
        public GameView gameView;
        public Texture2D background;
        public IOController IOController;
        public MouseState mouseState;
        public int windowWidth, windowHeight;
        public GlobalState(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics)
        {
            Content = new ContentManager(services, "Content");
            Content.RootDirectory = "Content"; currentState = State.Menu;
            windowWidth = clientWidth;
            windowHeight = clientHeight;
            Graphics = graphics;
            Texture2D rect = new Texture2D(graphics.GraphicsDevice, 1000, 250);
            IOController = new IOController(services, "TestPlaces4.json");
            menu = new MainMenuView(services, graphics, this);
            mainSettings = new MainSettingsView(windowWidth, windowHeight, services, graphics);
            gameView = new GameView(windowWidth, windowHeight, services, graphics);
            background = Content.Load<Texture2D>("southern_terminus");
            graphics.PreferredBackBufferWidth = background.Width;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = background.Height;   // set this value to the desired height of your window
            graphics.ApplyChanges();
            
        }





        private void stateMachine(GameTime gameTime, StateMode stateMode)
        {
            switch (currentState)
            {
                case State.Menu:
                    currentState = runState(menu, stateMode, gameTime) ?? currentState;
                    break;
                case State.MainSettings:
                    currentState = runState(mainSettings, stateMode, gameTime) ?? currentState;
                    break;
                case State.Game:
                    currentState = runState(gameView, stateMode, gameTime) ?? currentState;
                    break;
                case State.CharacterCreation:
                    currentState = runState(characterCreationView, stateMode, gameTime) ?? currentState;
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
        }

        private State? runState(IView gameView, StateMode stateMode, GameTime gameTime)
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

        //this shit doesn't work right now but it isn't the end of the world- right now the only way to end up in a state is if the machine returned it
        // which is pretty great in terms of knowing how you get errors, as opposed to any button being able to shunt your state machine anywhere.
        protected void setState(State newState, object sender, EventArgs e)
        {
            currentState = newState;
        }

        public void Menu(object sender, EventArgs e)
        {
            currentState = State.Menu;
        }
        public void Game(object sender, EventArgs e)
        {
            currentState = State.Game;
        }
        public void CharacterCreation(object sender, EventArgs e)
        {
            currentState = State.CharacterCreation;
        }
        public void MainSettings(object sender, EventArgs e)
        {
            currentState = State.MainSettings;
        }


        public State Update(GameTime gameTime)
        {

            stateMachine(gameTime, StateMode.Update);



            return State.Game;
        }


        public void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.White);
            stateMachine(gameTime, StateMode.Draw);
        }




    }

}