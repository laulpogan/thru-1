using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Thru
{
    public class StoreMenu
    {
        public SpriteFont font;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public Texture2D buttonImage;
        public ArrayList graph;
        public GraphicsDeviceManager Graphics;
        public Vector2 Coords;
        public Player Player;
        public Button MoneyToMoraleButton, MoneyToSnacksButton;
        public StoreMenu(IServiceProvider services, GraphicsDeviceManager graphics, Vector2 drawCoords, Player player)
        {
            if (player is not null)
                Player = player;
            Content = new ContentManager(services, "Content");
            buttonImage = Content.Load<Texture2D>("InterfaceTextures/short_button");
            font = Content.Load<SpriteFont>("Score");
            Graphics = graphics;
            Coords = drawCoords;
            buttonGroup = new ButtonGroup(new ArrayList(), Coords, ButtonArrangement.Horizontal);
            currentLocation = location;

            MoneyToMoraleButton = new Button(buttonImage, "Spend $100 to buy 10 Morale", font);
            MoneyToSnacksButton = new Button(buttonImage, "Spend $20 to buy 3 Snacks", font);

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
                                Buy(location, adjacentLocations[location]);
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

        public void Buy(Location location, float Value)
        {

            if (Player is not null)
            {
                if (Player.stats.Energy! > Value)
                {
                    Console.WriteLine($"Traveling to {location.Name} for {Value} Energy. {Player.Name} now has {Player.stats.Snacks} energy.");
                    Player.stats.Energy = Player.stats.Energy - (int)Value;
                    currentLocation = location;
                }
            }
        }
        public void buildMapButtons()
        {
            ArrayList buttons = new ArrayList();
            foreach (Location location in currentLocation.AdjacentLocations())
            {
                Button button = new Button(buttonImage, $"{location.ID}", font);
                buttons.Add(button);
            }

            // buttons.Add(menuButton);
            // buttons.Add(gameButton);
            buttonGroup = new ButtonGroup(buttons, Coords, ButtonArrangement.Horizontal);
        }


    }
}

