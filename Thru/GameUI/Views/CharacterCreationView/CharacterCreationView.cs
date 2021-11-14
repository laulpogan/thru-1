using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Linq;
using System.Reflection;

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
		public GraphicsDeviceManager Graphics;
		public CharacterBuilder characterBuilder;
		public CharacterModel characterModel;
		public Vector2 Coords;
		public Color[] Colors;
		public int x;


        public CharacterCreationView(IServiceProvider services, int width, int height, GraphicsDeviceManager graphics)
		{
			Graphics = graphics;
			Content = new ContentManager(services, "Content");
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			characterBuilder = new CharacterBuilder(services, graphics);
			buttonImage = Content.Load<Texture2D>("longbutton");
			shuffleButton = new Button(buttonImage, "Shuffle", Content.Load<SpriteFont>("Score"));
			menuButton = new Button(buttonImage, "Main Menu", Content.Load<SpriteFont>("Score"));
			shuffleButton.stateMachineState = State.CharacterCreation;
			menuButton.stateMachineState = State.Menu;
			ArrayList buttons = new ArrayList();
			buttons.Add(shuffleButton);
			buttons.Add(menuButton);
			Coords = new Vector2(100, 100);
			buttonGroup = new ButtonGroup(buttons, Coords);
			characterModel  = new CharacterModel(services, graphics, new Vector2(600, 400));
			Colors = ThruLib.allColors();
			x =0 ;
	}


		public State Update(GameTime gameTime)
		{
			buttonGroup.Update(gameTime);
			if( x% 2 == 0)
				Shuffle();

			x++;
			if (shuffleButton.State == BState.JUST_RELEASED)
            {
				Shuffle();
            }
			characterModel.Update(gameTime);

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
			characterModel.Draw(spriteBatch,1);
			spriteBatch.End();
			/*hudBatch.Begin();
			hudBatch.End();*/


		}

		public void Shuffle()
		{
			Random rand = new Random();
/*characterModel.bodyColor = Colors[rand.Next(0, Colors.Length)];
characterModel.hairColor = Colors[rand.Next(0, Colors.Length)];
characterModel.eyeColor = Colors[rand.Next(0, Colors.Length)];
characterModel.shirtColor = Colors[rand.Next(0, Colors.Length)];
characterModel.pantsColor = Colors[rand.Next(0, Colors.Length)];
characterModel.shoeColor = Colors[rand.Next(0, Colors.Length)];*/
            characterModel.bodyColor = new Color(rand.Next(0,255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
			characterModel.hairColor = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
			characterModel.eyeColor = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
			characterModel.shirtColor = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
			characterModel.pantsColor = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
			characterModel.shoeColor = new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
		}
	}
}
