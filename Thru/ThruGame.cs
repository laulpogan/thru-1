using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Thru
{
	public class ThruGame : Game
	{
		Texture2D ballTexture;
		Vector2 ballPosition;
		float ballSpeed;
		private SpriteFont font;
		private Texture2D background;
		private Texture2D ball;
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private AnimatedSprite animatedSprite;
		private Button button1;
		private MouseState oldState;
		private Texture2D arrow;
		private float angle = 0;
		public ThruGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			
		}

		
		protected override void Initialize()
		{
			Texture2D buttonImage = new Texture2D(_graphics.GraphicsDevice, 100, 400);
			button1 = new Button(buttonImage, "");
	
			// TODO: Add your initialization logic here
			ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
_graphics.PreferredBackBufferHeight / 2);
			ballSpeed = 100f;
			_graphics.PreferredBackBufferWidth = 1000;  // set this value to the desired width of your window
			_graphics.PreferredBackBufferHeight = 1000;   // set this value to the desired height of your window
			_graphics.ApplyChanges();
			base.Initialize();
		}

		protected override void LoadContent()
		{
	
			// TODO: use this.Content to load your game content here
			ballTexture = Content.Load<Texture2D>("ball");
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			arrow = Content.Load<Texture2D>("arrow"); // use the name of your texture here, if you are using your own
			button1.Texture = Content.Load<Texture2D>("download");
			// This is the code we added earlier.
			background = Content.Load<Texture2D>("triangle"); // change these names to the names of your images
			ball = Content.Load<Texture2D>("ball");
			Texture2D texture = Content.Load<Texture2D>("SmileyWalk");
			animatedSprite = new AnimatedSprite(texture, 4, 4);
			font = Content.Load<SpriteFont>("Score"); // Use the name of your sprite font file here instead of 'Score'.

		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			MouseState newState = Mouse.GetState();
			int x = newState.X;
			int y = newState.Y;
            double ballangle = Math.Atan2(y - 240, x - 400);
			angle = (float)ballangle;
			angle += (float)1.571;
			if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
			{
				
				ballPosition.Y = y;
				ballPosition.X = x;
			}

			if (newState.LeftButton == ButtonState.Pressed)
			{
				ballPosition.Y = y;
				ballPosition.X = x;
			}
			oldState = newState; // this reassigns the old state so that it is ready for next time

			// TODO: Add your update logic here
			var kstate = Keyboard.GetState();

			if (kstate.IsKeyDown(Keys.Up))
				ballPosition.Y -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (kstate.IsKeyDown(Keys.Down))
				ballPosition.Y += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (kstate.IsKeyDown(Keys.Left))
				ballPosition.X -= ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (kstate.IsKeyDown(Keys.Right))
				ballPosition.X += ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			/*if (ballPosition.X > _graphics.PreferredBackBufferWidth - ballTexture.Width / 2)
				ballPosition.X = _graphics.PreferredBackBufferWidth - ballTexture.Width / 2;
			else if (ballPosition.X < ballTexture.Width / 2)
				ballPosition.X = ballTexture.Width / 2;

			if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height / 2)
				ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height / 2;
			else if (ballPosition.Y < ballTexture.Height / 2)
				ballPosition.Y = ballTexture.Height / 2; */
			animatedSprite.Update();
			button1.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
            if (button1.isPressed)
            {
				_graphics.GraphicsDevice.Clear(Color.PeachPuff) ;

			}
			else
            {
				_graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			}

			_spriteBatch.Begin();
			_spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
			button1.Draw(_spriteBatch, new Vector2(100, 200));

			Vector2 location = new Vector2(400, 240);
			Rectangle sourceRectangle = new Rectangle(0, 0, arrow.Width, arrow.Height);
			Vector2 origin = new Vector2(arrow.Width / 2, arrow.Height);
			_spriteBatch.Draw(arrow, location, sourceRectangle, Color.White, angle, origin, 1.0f, SpriteEffects.None, 1);
			_spriteBatch.Draw(ball, new Vector2(ballPosition.X, ballPosition.Y), Color.White);
			_spriteBatch.DrawString(font, "Score" + 0, new Vector2(100, 200), Color.Black);
			animatedSprite.Draw(_spriteBatch, new Vector2(400, 200));


			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
