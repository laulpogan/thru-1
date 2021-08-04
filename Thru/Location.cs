using System;
using System.Collections;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Thru
{
	public class Location
	{
		public ArrayList AdjacentLocations;
		public Texture2D background;
		public string name;
		public SpriteFont font;
		private ContentManager Content;
		int ClientWidth, ClientHeight;
		public ButtonGroup buttonGroup;
        public bool ShowMap;
        public Texture2D buttonImage;
		public Button mapButton;
        public Location(int clientWidth, int clientHeight, IServiceProvider services, ArrayList adjacentLocations)
        {
			AdjacentLocations = adjacentLocations;
			ShowMap = false;
			ClientWidth = clientWidth;
			ClientHeight = clientHeight;
			Content = new ContentManager(services, "Content");
			buttonImage = Content.Load<Texture2D>("roughbutton");
			font = Content.Load<SpriteFont>("Score");

			mapButton = new Button(buttonImage);
			mapButton.Text = "Map";
			mapButton.Font = font;
			showTravelMap();


		}
		public void Update(GameTime gameTime)
        {
			buttonGroup.Update(gameTime);
			mapButton.Update(gameTime);
			if (mapButton.State == BState.JUST_RELEASED)
            {
				ShowMap = !ShowMap;
            }

		}
		
		public void Draw(SpriteBatch spriteBatch)
        {
			spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
			mapButton.Draw(spriteBatch);
			if (ShowMap)
            {
				showTravelMap();
				buttonGroup.Draw(spriteBatch);
            }
        }

		public void showTravelMap()
        {
			Button[] buttons = new Button[AdjacentLocations.Count];
			int count = 0;
			foreach(Location location in AdjacentLocations)
            {
				//Texture2D rect = new Texture2D(_graphics.GraphicsDevice, 1000, 250);

				Button button = new Button(buttonImage);
				button.Text = location.name;
				button.Font = font;
				buttons[count] = button;
				count++;
            }
			 buttonGroup = new ButtonGroup(buttons,ClientWidth,ClientHeight);
        }
	}

}
