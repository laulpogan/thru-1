using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Thru
{
    public class MapMenu : IGameView
    {
        public SpriteFont font;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public bool ShowMap;
        public Texture2D buttonImage;
        public Button mapButton;
        public Button menuButton;
        public ArrayList graph;
        public Location currentLocation;
        public MapMenu(IServiceProvider services, Location location)
        {
            Button[] buttons = new Button[0];
            buttonGroup = new ButtonGroup(buttons, new Vector2(100, 100));
            Content = new ContentManager(services, "Content");
            buttonImage = Content.Load<Texture2D>("longbutton");
            currentLocation = location;
            ShowMap = false;
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
                    var adjactLocations = currentLocation.AdjacentLocations();
                    foreach (Location location in adjactLocations)
                    {
                        if(location.Name == button.Text)
                        {
                            currentLocation = location;
                        } 
                    }
                    return currentLocation;
                }
            }
            return currentLocation;
        }
        public void buildMapButtons()
        {
            Dictionary<string, Button> buttons = new Dictionary<string, Button>();

            foreach (Location location in currentLocation.AdjacentLocations())
            {
                
                Button button = new Button(buttonImage);
                button.Text = location.Name;
                Console.WriteLine(button.Text);
                button.Font = font;
                buttons[button.Text] = button;
            }
            foreach (Button button in buttonGroup.ButtonList)
            {
                buttons[button.Text] = button;
            }

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
            return currentLocation;
        }

        public void setupCoords()
        {
           
        }

    }
}

