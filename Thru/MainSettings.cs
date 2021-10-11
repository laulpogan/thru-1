using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;

namespace Thru
{

	public class MainSettings : IGameView
	{
		Button returnButton;
		private ContentManager Content;
		public ButtonGroup buttonGroup;
		public MainSettings(int clientWidth, int clientHeight, IServiceProvider services)
		{
			Content = new ContentManager(services, "Content");

			Texture2D buttonImage = Content.Load<Texture2D>("longbutton");
            returnButton = new Button(buttonImage);
			ArrayList returnList = new ArrayList();
			returnList.Add(returnButton);
			buttonGroup = new ButtonGroup(returnList, new Vector2(100, 100));
		}

		public void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics) {
			buttonGroup.Draw(_spriteBatch);

		}
		public State Update(GameTime gameTime) {
			buttonGroup.Update(gameTime);
            return returnButton.State == BState.JUST_RELEASED ? State.Menu : State.MainSettings;
        }
	}
}

