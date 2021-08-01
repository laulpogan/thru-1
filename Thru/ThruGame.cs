using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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

        public ThruGame()
        {
            menu = new Menu(Window.ClientBounds.Width, Window.ClientBounds.Height, Services);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }


        protected override void Initialize()
        {
            state = State.Menu;

            _graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1000;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {

            menu.LoadContent();

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
            
            _spriteBatch.Begin();
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
                    break;
                /*case State.Start:
					break;
				case State.Game:
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
