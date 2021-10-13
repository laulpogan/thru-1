using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Thru
{
    public class MapMenu 
    {
        public SpriteFont font;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public Texture2D buttonImage;
        public Button mapButton;
        public Button menuButton;
        public ArrayList graph;
        public Location currentLocation;
        public GraphicsDeviceManager Graphics;
        public Vector2 Coords;
        public MapMenu(IServiceProvider services, Location location, GraphicsDeviceManager graphics)
        {
           
            Content = new ContentManager(services, "Content");
            buttonImage = Content.Load<Texture2D>("longbutton");
            font = Content.Load<SpriteFont>("Score");
            Graphics = graphics;
            Coords = new Vector2(100, 100);
            buttonGroup = new ButtonGroup(new ArrayList(), Coords);
            currentLocation = location;
            buttonImage = Content.Load<Texture2D>("longbutton");

            mapButton = new Button(buttonImage, "Map", Content.Load<SpriteFont>("Score"));

            menuButton = new Button(buttonImage, "Menu", Content.Load<SpriteFont>("Score"));


            buildMapButtons();
            foreach (Location location1 in currentLocation.AdjacentLocations())
                Console.WriteLine("Adjacent Locations: " + location1.Name);

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            
                buttonGroup.Draw(spriteBatch);
            

        }
        public Location Update(GameTime gameTime)
        {


            buttonGroup.Update(gameTime);

            foreach (Button button in buttonGroup.ButtonList)
            {
                if (button.State == BState.JUST_RELEASED)
                {
                    Console.WriteLine("You just pressed "+button.Text);
                    var adjacentLocations = currentLocation.AdjacentLocations();
                    foreach (Location location in adjacentLocations)
                    {
                            Console.WriteLine("Adjacent Location: " + location.Name);

                            if (location.Name == button.Text)
                        {
                            currentLocation = location;
                            buildMapButtons();
             
                        }
                    }
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
                button.Font = font;
                buttons[button.Text] = button;
            }
      /*      foreach (Button button in buttonGroup.ButtonList)
            {
                buttons[button.Text] = button;
            }*/

            ArrayList buttonFinal = new ArrayList(buttons.Values);
            buttonFinal.Add(mapButton);
            buttonFinal.Add(menuButton);
            buttonGroup = new ButtonGroup(buttonFinal, Coords);
        }
     

    }
}

