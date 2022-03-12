using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;

namespace Thru
{
    public class MapMenu 
    {
        public SpriteFontBase Font;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public Texture2D buttonImage;
        public Button menuButton;
        public Button gameButton;
        public ArrayList graph;
        public Location currentLocation;
        public GraphicsDeviceManager Graphics;
        public Vector2 Coords;
        public Character Player;
        public GlobalState GlobalState;
        public MapMenu(IServiceProvider services, GraphicsDeviceManager graphics, Location location, Vector2 drawCoords,GlobalState globalState, Character player = null)
        {
            GlobalState = globalState;
            if(player is not null)
                Player = player;
            Content = new ContentManager(services, "Content");
            buttonImage = Content.Load<Texture2D>("InterfaceTextures/short_button");
            Font = globalState.FontSystem.GetFont(12);
            Graphics = graphics;
            Coords = drawCoords;
            buttonGroup = new ButtonGroup(new ArrayList(), Coords, ButtonArrangement.Horizontal);
            currentLocation = location;

            menuButton = new Button(globalState.MouseHandler, buttonImage, "Menu", Font);
            gameButton = new Button(globalState.MouseHandler, buttonImage, "Game", Font);

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

            // todo - extend Button with a LocationButton that holds corresponding location information (ID)
            foreach (Button button in buttonGroup.ButtonList)
            {
                if (button.State == BState.JUST_RELEASED)
                {
                    Console.WriteLine("You just pressed " + button.Text);
                    Dictionary<Location, float> adjacentLocations = currentLocation.AdjacentLocationsWithWeight();
                    foreach (Location location in adjacentLocations.Keys)
                    {
                        Console.WriteLine("Adjacent Location: " + location.Name);
                        if (location.ID == button.Text)
                        {
                            if (Player is not null)
                            {
                                TravelTo(location, adjacentLocations[location]);
                            }
                            else
                            {
                                currentLocation = location;
                            }
                            buildMapButtons();
                        }
                    }
                }
            }
            return currentLocation;
        }

        public void TravelTo(Location location, float Value)
        {

            if (Player is not null)
            {
               if(Player.Stats.Energy!> Value)
                {
                    Console.WriteLine($"Traveling to {location.Name} for {Value} Energy. {Player.Name} now has {Player.Stats.Snacks} energy.");
                    Player.Stats.Energy = Player.Stats.Energy- (int)Value;
                    currentLocation = location;
                }
            }
        }
        public void buildMapButtons()
        {
            ArrayList buttons = new ArrayList();
            foreach (Location location in currentLocation.AdjacentLocations())
            {
                Button button = new Button(GlobalState.MouseHandler,buttonImage, $"Travel to {location.ID}", Font);
                buttons.Add(button);
            }

            buttons.Add(menuButton);
            buttons.Add(gameButton);
            buttonGroup = new ButtonGroup(buttons, Coords, ButtonArrangement.Horizontal);
        }
     

    }
}

