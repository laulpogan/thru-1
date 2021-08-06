using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Text.Json;

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
        Location location1, location2, location3;
        ArrayList Locations;
        public ThruGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }
        public void setupLocations()
        {
            Locations = new ArrayList();
            location1 = new Location( Locations, background, "southern terminus");
            Locations.Add(location1);
            location2 = new Location( Locations,  Content.Load<Texture2D>("Moms_Diner"), "mom's diner");
            Locations.Add(location2);
            location1.AdjacentLocations.Add(location2);
            location3 = new Location(Locations, Content.Load<Texture2D>("triangle"), "7th dimension hyperroom");
            location2.AdjacentLocations.Add(location3);
            location1.AdjacentLocations.Add(location3);
            Locations.Add(location3);

            // var options = new JsonSerializerOptions { WriteIndented = true };
            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
                WriteIndented = true
            };
            string jsonString = JsonSerializer.Serialize(Locations, options);
            Console.WriteLine(jsonString);

        }

        protected override void Initialize()
        {
            state = State.Menu;
            //Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);

            //displayBox = new DisplayWindow(rect, Services);
            menu = new Menu(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
             mainSettings = new MainSettings(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            background = Content.Load<Texture2D>("southern_terminus");
            setupLocations();
            mainGameView = new MainGameView(location1, Services);


            _graphics.PreferredBackBufferWidth = background.Width;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = background.Height;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ///font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            stateMachine(gameTime, StateMode.Update);


            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteBatch.Begin();
            stateMachine(gameTime, StateMode.Draw);



            //displayBox.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private void stateMachine( GameTime gameTime, StateMode stateMode)
        {
            switch (state)
            {
                case State.Menu:
                    state = runState(menu, stateMode, gameTime) ?? state ;
                    _graphics.GraphicsDevice.Clear(Color.Green);

                    break;
                case State.MainSettings:
                    _graphics.GraphicsDevice.Clear(Color.Blue);
                    

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
