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
        public Button menuButton;
        public ArrayList graph;
        public Location Location;
	public Map( IServiceProvider services, Location location)
	{
            Button[] buttons = new Button[0];
        buttonGroup= new ButtonGroup(buttons,new Vector2(100, 100));
            Content = new ContentManager(services, "Content");
            Background = Content.Load<Texture2D>("westcoast");
            buttonImage = Content.Load<Texture2D>("longbutton");
            Location = location;
            ShowMap = false;
            buildMapButtons();
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (ShowMap)
            {
                spriteBatch.Draw(Background, new Vector2(0, 0), Color.White);

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
                Console.WriteLine(button.Text);
                button.Font = font;
                buttons[button.Text] = button;
            }
            foreach(Button button in buttonGroup.ButtonList)
            {
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

