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


namespace Thru
{
    public class ThruGame : Game
    {
        enum StateMode
        {
            Update,
            Draw
        }
      
        private SpriteFont font;
        private GraphicsDeviceManager _graphics;
        public State currentState { 
            get { return state; } 
            set {
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
        public MapView mapView;
        public Texture2D background;
        public DisplayWindow displayBox;
        public IOController IOController;
        CharacterBuilder GameStateController;
        public MouseState mouseState;
        private AnimatedSprite animatedSprite;
        public ThruGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
       
        protected override void Initialize()
        {
            /* newRect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            Color[] data = new Color[1000 * 250];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            newRect.SetData(data);
*/
                      currentState = State.Menu;
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            IOController = new IOController(Services, "TestPlaces4.json");

            //displayBox = new DisplayWindow(rect, Services);
            menu = new MainMenuView(Window.ClientBounds.Width, Window.ClientBounds.Height, Services, _graphics);
            mainSettings = new MainSettingsView(Window.ClientBounds.Width, Window.ClientBounds.Height, Services, _graphics);
            background = Content.Load<Texture2D>("southern_terminus");
            //Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 200);
            displayBox = new DisplayWindow(rect, "", "", Services);
            GameStateController = new CharacterBuilder(Services, displayBox);
            _graphics.PreferredBackBufferWidth = background.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = background.Height;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

          
            mapView = new MapView( Services, Window.ClientBounds.Width, Window.ClientBounds.Height, _graphics);

            //setupLocations();

            

            Texture2D texture = Content.Load<Texture2D>("buttonsheet");
            animatedSprite = new AnimatedSprite(texture,2,2);
            base.Initialize();
        }

        protected override void LoadContent()
        {


            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            stateMachine(gameTime, StateMode.Update);


            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();


            //cam.UpdateCamera(_graphics.GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.White);
                      
            stateMachine(gameTime, StateMode.Draw);
            
            base.Draw(gameTime);
        }

        private void stateMachine( GameTime gameTime, StateMode stateMode)
        {
            _graphics.GraphicsDevice.Clear(Color.White);

            switch (currentState)
            {
                case State.Menu:
                    currentState = runState(menu, stateMode, gameTime) ?? currentState ;
                    break;
                case State.MainSettings:
                    currentState = runState(mainSettings, stateMode, gameTime) ?? currentState;
					break;
				case State.Game:
                    currentState = runState(mapView, stateMode, gameTime) ?? currentState;

                    break;
                case State.Map:
                    currentState = runState(mapView, stateMode, gameTime) ?? currentState;

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
                    Console.WriteLine("game is broken bucko: "+currentState);
                    Exit();
                    break;

            }
        }

        private State? runState(IGameView gameView, StateMode stateMode, GameTime gameTime) 
        {
           
            switch (stateMode)
            {
                case StateMode.Update:
                    return gameView.Update(gameTime);
                     
                case StateMode.Draw:
                    gameView.Draw(_graphics);
                    return null;
                default:
                    Exit();
                    break;
            }
            return null;
        }  



    }
}
