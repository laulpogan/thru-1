using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections;


namespace Thru
{

	public class MainSettingsView : IView
	{
		Button returnButton;
		private ContentManager Content;
		public ButtonGroup buttonGroup;
		public SpriteBatch spriteBatch;
		public MainSettingsView(int clientWidth, int clientHeight, IServiceProvider services, GraphicsDeviceManager graphics, GlobalState globalState)
		{
			Content = new ContentManager(services, "Content");
			spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
			Texture2D buttonImage = Content.Load<Texture2D>("InterfaceTextures/longbutton");
            returnButton = new Button(globalState.MouseHandler, buttonImage);
			ArrayList returnList = new ArrayList();
			returnList.Add(returnButton);
			buttonGroup = new ButtonGroup(returnList, new Vector2(100, 100));
		}

		public void Draw( GraphicsDeviceManager _graphics) {
			spriteBatch.Begin();
			buttonGroup.Draw(spriteBatch);
			spriteBatch.End();

		}
		public State Update(GameTime gameTime) {
			buttonGroup.Update(gameTime);
            return returnButton.State == BState.JUST_RELEASED ? State.Menu : State.MainSettings;
        }
	}
}

