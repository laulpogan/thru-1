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
        private SpriteBatch _spriteBatch;
        private State state;
        private Menu menu;
        private MainSettings mainSettings;
        public MapView mapView;
        public Texture2D background;
        public DisplayWindow displayBox;
        public IOController IOController;
        GameStateController GameStateController;
        public MouseState mouseState;
        Camera2d cam;
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
                      state = State.Menu;
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            IOController = new IOController(Services, "TestPlaces4.json");

            //displayBox = new DisplayWindow(rect, Services);
            menu = new Menu(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            mainSettings = new MainSettings(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            background = Content.Load<Texture2D>("southern_terminus");
            //Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 200);
            displayBox = new DisplayWindow(rect, "", "", Services);
            GameStateController = new GameStateController(Services, displayBox);
            _graphics.PreferredBackBufferWidth = background.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = background.Height;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            cam = new Camera2d(_graphics);
            cam.Pos = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);

            mapView = new MapView( Services, Window.ClientBounds.Width, Window.ClientBounds.Height, _graphics);

            //setupLocations();

            

            Texture2D texture = Content.Load<Texture2D>("buttonsheet");
            animatedSprite = new AnimatedSprite(texture,2,2);
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


            GameStateController.Update(gameTime);
            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();


            //cam.UpdateCamera(_graphics.GraphicsDevice.Viewport);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _graphics.GraphicsDevice.Clear(Color.White);
            /*_spriteBatch.Begin(SpriteSortMode.FrontToBack, null,
                        null,
                        null,
                        null,
                        null,
                        cam.Transform);*/
            _spriteBatch.Begin();
          /*  _spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation());*/
            stateMachine(gameTime, StateMode.Draw);
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void stateMachine( GameTime gameTime, StateMode stateMode)
        {
            switch (state)
            {
                case State.Menu:
                    state = runState(menu, stateMode, gameTime) ?? state ;
                    _graphics.GraphicsDevice.Clear(Color.White);

                    break;
                case State.MainSettings:
                    _graphics.GraphicsDevice.Clear(Color.White);
                    

                    state = runState(mainSettings, stateMode, gameTime) ?? state;
					break;
				case State.Game:
                    state = runState(mapView, stateMode, gameTime) ?? state;

                    break;
                case State.Map:
                    _graphics.GraphicsDevice.Clear(Color.White);
                    state = runState(mapView, stateMode, gameTime) ?? state;

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
