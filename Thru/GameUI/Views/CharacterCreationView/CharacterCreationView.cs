using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace Thru
{
	public class CharacterCreationView : IView
	{
		public State State = State.CharacterCreation;
		public Button menuButton, shuffleButton;
		public Texture2D buttonImage;
		public ContentManager Content;
		public MapMenu mapMenu;
		public ButtonGroup buttonGroup;
		public SpriteBatch spriteBatch, hudBatch;
		public Camera cam;
		public GraphicsDeviceManager Graphics;
		public CharacterBuilder characterBuilder;
		public Vector2 Coords;

		public CharacterCreationView(IServiceProvider services, int width, int height, GraphicsDeviceManager graphics)
		{
			Graphics = graphics;
			Content = new ContentManager(services, "Content");
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			characterBuilder = new CharacterBuilder(services, graphics, null);
			buttonImage = Content.Load<Texture2D>("longbutton");
			shuffleButton = new Button(buttonImage, "Shuffle", Content.Load<SpriteFont>("Score"));
			menuButton = new Button(buttonImage, "Menu", Content.Load<SpriteFont>("Score"));
			shuffleButton.stateMachineState = State.CharacterCreation;
			menuButton.stateMachineState = State.Menu;
			ArrayList buttons = new ArrayList();
			buttons.Add(shuffleButton);
			buttons.Add(menuButton);
			Coords = new Vector2(100, 100);
			buttonGroup = new ButtonGroup(buttons, Coords);

		}


		public State Update(GameTime gameTime)
		{
			buttonGroup.Update(gameTime);
			foreach (Button button in buttonGroup.ButtonList)
			{
				if (button.State == BState.JUST_RELEASED)
				{
					Console.WriteLine("You just clicked the" + button.Text + " button.");
					return button.stateMachineState;
				
				}
			}
			return State.CharacterCreation;
		}
		public void Draw(GraphicsDeviceManager _graphics)
		{
			spriteBatch.Begin();
			buttonGroup.Draw(spriteBatch);
			spriteBatch.End();
			/*hudBatch.Begin();
			hudBatch.End();*/


		}

	}
}
