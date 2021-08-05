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
			Button[] tempList = { mapButton, menuButton };
			buttonGroup = new ButtonGroup(tempList, new Vector2(100, 600));

		}
		public  State Update(GameTime gameTime)
        {

            if (Map.ShowMap) {
				Location = Map.Update(gameTime);
			}
			if (menuButton.State == BState.JUST_RELEASED)
			{
				Console.Write("Menu Button PRess" );

				return State.Menu;
			}
			if (mapButton.State == BState.JUST_RELEASED)
			{

				Map.ShowMap = !Map.ShowMap;
				Console.Write("Show Map: " + Map.ShowMap);

			}
			buttonGroup.Update(gameTime);

			return State;
        }
		public  void Draw(SpriteBatch _spriteBatch, GraphicsDeviceManager _graphics)
        {
			Location.Draw(_spriteBatch);
            
				Map.Draw(_spriteBatch);

            if (!Map.ShowMap)
            {
				buttonGroup.Draw(_spriteBatch);

			}


		}
	}
}
