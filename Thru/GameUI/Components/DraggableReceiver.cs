using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class DraggableReceiver  : IDraggableContainer
    {

        public Vector2 ScreenXY;
        public Rectangle Bounds;
        public Texture2D Icon;
        public MouseHandler MouseHandler;
        public Point ScreenHome; 
            public Point BoardHome { get; set; }
        public bool isOccupied
        {
            get
            {
                return Item is not null;
            }
        }
        public Item Item
        {
            get
            {
                return iconHeld is not null ? iconHeld.Item : null;
            }

        }
        public InventoryGameBoard GameBoard;
        public ItemIconDraggable iconHeld { get; set; }


        public DraggableReceiver(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point screenHome, Point boardHome, InventoryGameBoard gameBoard)
        {

            Icon = ThruLib.makeBlankRect(graphics, 32, 32);
            MouseHandler = mouseHandler;
            Bounds = Icon.Bounds;
            this.ScreenHome = screenHome;
            BoardHome = boardHome;
            Bounds.Location = this.ScreenHome;
            iconHeld = null;
            GameBoard = gameBoard;
        }

        public GameState Update(GameTime gameTime)
        {
                return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Icon,Bounds , Color.Black);
        }


      
    }


}