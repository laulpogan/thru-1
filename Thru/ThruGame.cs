using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

namespace Thru
{
    public class ThruGame : Game
    {
        enum StateMode
        {
            Update,
            Draw
        }
        const int
            EASY_BUTTON_INDEX = 0,
            MEDIUM_BUTTON_INDEX = 1,
            HARD_BUTTON_INDEX = 2;
        private SpriteFont font;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State state;
        private Menu menu;
        private MainSettings mainSettings;
        public Texture2D background;
        public DisplayWindow displayBox;
        Location location1, location2, location3;
        Location currentLocal;
        ArrayList Locations;
        public ThruGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }


        protected override void Initialize()
        {
            state = State.Menu;
            Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);
            Locations = new ArrayList();
            location1 = new Location(Window.ClientBounds.Width, Window.ClientBounds.Height, Services, Locations);
            Locations.Add(location1);
            location2 = new Location(Window.ClientBounds.Width, Window.ClientBounds.Height, Services, Locations);
            Locations.Add(location2);
            location1.AdjacentLocations.Add(location2);
            location3 = new Location(Window.ClientBounds.Width, Window.ClientBounds.Height, Services, Locations);
            location2.AdjacentLocations.Add(location3);
            location1.AdjacentLocations.Add(location3);
            Locations.Add(location3);
            currentLocal = location1;
            displayBox = new DisplayWindow(rect, Services);
            menu = new Menu(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            mainSettings = new MainSettings(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            background = Content.Load<Texture2D>("southern_terminus");

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


            foreach(Location location in Locations)
            {
                location.Update(gameTime);
            }
            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(0,0), Color.White);
            currentLocal.Draw(_spriteBatch);
            stateMachine(gameTime, StateMode.Draw);



            displayBox.Draw(_spriteBatch, gameTime);

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
				/*case State.Game:
					break;
				case State.Final:
					break;
				case State.Road:
					break;
				case State.Town:
					break;
				case State.Trailhead:
					break;
				case State.Start:
					break;
				case State.Start:
					break;*/
                default:
                    Console.WriteLine("game is broken bucko");
                    Exit();
                    break;

            }
        }

        private State? runState(IGameView gameView, StateMode stateMode, GameTime gameTime) 
        {
            switch(stateMode)
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
