using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Thru
{
	public class MainGameView : IGameView
	{
		public Location Location;
		public State State = State.Game;
		public Button menuButton, mapButton;
		public Texture2D buttonImage;
		public ContentManager Content;
		public Map Map;
		public ButtonGroup buttonGroup;
		public MainGameView(Location location, IServiceProvider services)
		{
			Location = location;
			Content = new ContentManager(services, "Content");
			Map = new Map(services, Location) ;
			buttonImage = Content.Load<Texture2D>("longbutton");
			mapButton = new Button(buttonImage);
			mapButton.Text = "Map";
			mapButton.Font = Content.Load<SpriteFont>("Score");
			Map.buttonGroup.ButtonList.Add(mapButton);
			menuButton = new Button(buttonImage);
			menuButton.Text = "Menu";
			menuButton.Font = Content.Load<SpriteFont>("Score");
			Map.buttonGroup.ButtonList.Add(menuButton);
			buttonGroup = new ButtonGroup((Button[])Map.buttonGroup.ButtonList.ToArray(typeof(Button)), new Vector2(100, 100));
		}
		public  State Update(GameTime gameTime)
        {
            Location = Map.Update(gameTime);
			buttonGroup.Update(gameTime);
            if (menuButton.State == BState.JUST_RELEASED)
			{
				return State.Menu;
			}
			if (mapButton.State == BState.JUST_RELEASED)
			{
				Map.ShowMap = !Map.ShowMap;
			}
			return State;
        }
		public  void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics)
        {
			Location.Draw(_spriteBatch);
            if (Map.ShowMap)
            {
				Map.Draw(_spriteBatch);
            }
            else
            {
				buttonGroup.Draw(_spriteBatch);
			}

		}
	}
}
