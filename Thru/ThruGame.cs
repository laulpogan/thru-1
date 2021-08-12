using System;
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
        private SpriteBatch _spriteBatch;
        private State state;
        private Menu menu;
        private MainSettings mainSettings;
        public MainGameView mainGameView;
        public Texture2D background;
        public DisplayWindow displayBox;
        public IOController IOController;
        Location location1, location2, location3;
        Dictionary<string,Location> Locations;
        GameStateController GameStateController;
        MapDataHandler mapHandler;
        public MouseState mouseState;
        Camera cam;
        public ThruGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void setupLocations()
        {
            Locations = new Dictionary<string, Location>();

            location1 = new Location(Locations, background, "southern terminus");
            Locations["southern terminus"] = location1;

            location2 = new Location(Locations, Content.Load<Texture2D>("Moms_Diner"), "mom's diner");
            Locations["mom's diner"] = location2;
            location1.AdjacentLocations["mom's diner"] = location2;

            location3 = new Location(Locations, Content.Load<Texture2D>("triangle"), "7th dimension hyperroom");
            Locations["7th dimension hyperroom"] = location3;
            location2.AdjacentLocations["7th dimension hyperroom"] = location3;
            location1.AdjacentLocations["7th dimension hyperroom"] = location3;

           
            IOController.serializeToFile<Location>(Locations);
            var jsonString = IOController.deserializeFromFile<Location>();
            foreach (string key in jsonString.Keys)
            {
                Console.WriteLine("LOADED: " + key + ": " + jsonString[key]);
            }
        }
       
        protected override void Initialize()
        {
            /* newRect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            Color[] data = new Color[1000 * 250];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            newRect.SetData(data);
*/
            state = State.Menu;
            //Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            IOController = new IOController(Services, "TestPlaces3.json");
            //displayBox = new DisplayWindow(rect, Services);
            menu = new Menu(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            mainSettings = new MainSettings(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            background = Content.Load<Texture2D>("southern_terminus");
            setupLocations();
            mainGameView = new MainGameView(location1, Services);
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 200);
            displayBox = new DisplayWindow(rect, "", "", Services);
            GameStateController = new GameStateController(Services, displayBox);
            _graphics.PreferredBackBufferWidth = background.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = background.Height;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            mapHandler = new MapDataHandler(Window.ClientBounds.Width, Window.ClientBounds.Height);
            cam = new Camera(_graphics.GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            stateMachine(gameTime, StateMode.Update);
          


            mapHandler.Update(gameTime);
           // GameStateController.Update(gameTime);
            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();



            cam.UpdateCamera(_graphics.GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack,null,
                        null,
                        null,
                        null,
                        null,
                        cam.Transform);
            stateMachine(gameTime, StateMode.Draw);
            mapHandler.Draw(_spriteBatch, gameTime);
           
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void stateMachine( GameTime gameTime, StateMode stateMode)
        {
            switch (state)
            {
                case State.Menu:
                    state = runState(menu, stateMode, gameTime) ?? state ;
                   // _graphics.GraphicsDevice.Clear(Color.Green);

                    break;
                case State.MainSettings:
                    //_graphics.GraphicsDevice.Clear(Color.Blue);
                    

                    state = runState(mainSettings, stateMode, gameTime) ?? state;
					break;
				case State.Game:
                    state = runState(mainGameView, stateMode, gameTime) ?? state;

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
                    Console.WriteLine("game is broken bucko: "+state);
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
                    gameView.Draw(_spriteBatch, _graphics);
                    return null;
                default:
                    Exit();
                    break;
            }
            return null;
        }

    }
}
