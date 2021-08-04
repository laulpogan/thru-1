using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Thru
{
	public class Map
    {
        public Texture2D Background;
        public SpriteFont font;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public bool ShowMap;
        public Texture2D buttonImage;
        public Button mapButton;
        public ArrayList graph;
        public Location Location;
	public Map( IServiceProvider services, Location location)
	{
            Content = new ContentManager(services, "Content");

            buttonImage = Content.Load<Texture2D>("longbutton");
            Location = location;
            ShowMap = true;
            buildMapButtons();
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ShowMap)
            {
                buttonGroup.Draw(spriteBatch);
            }
        }
        public Location Update(GameTime gameTime)
        {
            buttonGroup.Update(gameTime);
           

            foreach (Button button in buttonGroup.ButtonList)
            {
                if (button.State == BState.JUST_RELEASED)
                {
                    return findLocationByName(button.Text, Location.AdjacentLocations);
                }
            }
            return Location;
        }
        public void buildMapButtons()
        {
            Dictionary<string, Button> buttons = new Dictionary<string, Button>();

            foreach (Location location in Location.AdjacentLocations)
            {
                Button button = new Button(buttonImage);
                button.Text = location.Name;
                button.Font = font;
                button.Bounds.X = 150;
                button.Bounds.Y = 150;
                buttons[button.Text] = button;
            }
            List<Button> arr = new List<Button>(buttons.Values);
            Button[] buttonFinal = (new List<Button>(buttons.Values)).ToArray();
            buttonGroup = new ButtonGroup(buttonFinal, new Vector2(100, 100));
        }
        public Location findLocationByName(string name, ArrayList locations)
        {
            foreach (Location location in locations)
            {
                if (location.Name == name)
                {
                    return location;
                }
            }
            return Location;
        }

    }
}

