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
		public SpriteFont Font;


        public CharacterCreationView(IServiceProvider services, int width, int height, GraphicsDeviceManager graphics, GlobalState globalState)
		{
			Graphics = graphics;
			Content = new ContentManager(services, "Content");
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			hudBatch = new SpriteBatch(graphics.GraphicsDevice);
			Font = Content.Load<SpriteFont>("score");
			characterBuilder = new CharacterBuilder(services, graphics, new Point(600,400), Font);
			buttonImage = Content.Load<Texture2D>("longbutton");
			shuffleButton = new Button(globalState.MouseHandler,buttonImage, "Shuffle", Content.Load<SpriteFont>("Score"),null, null, Shuffle);
			menuButton = new Button(globalState.MouseHandler, buttonImage, "Main Menu", Content.Load<SpriteFont>("Score"));
			shuffleButton.stateMachineState = State.CharacterCreation;
			menuButton.stateMachineState = State.Menu;
			//menuButton.
			ArrayList buttons = new ArrayList();
			buttons.Add(shuffleButton);
			buttons.Add(menuButton);
			Coords = new Vector2(100, 100);
			buttonGroup = new ButtonGroup(buttons, Coords);
			characterModel = new CharacterModel(services, graphics, new Point(600, 400), globalState.Player, 1f,Font);
			Colors = ThruLib.allColors();
	}


		public State Update(GameTime gameTime)
		{
			buttonGroup.Update(gameTime);
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
		public void Draw(GraphicsDeviceManager graphics)
		{
			spriteBatch.Begin();
			buttonGroup.Draw(spriteBatch);
			characterModel.Draw(spriteBatch,1);
			spriteBatch.End();


		}

		public void Shuffle(object sender, EventArgs e)
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
