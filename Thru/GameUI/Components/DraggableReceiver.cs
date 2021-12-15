using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class DraggableReceiver
    {

        public Vector2 ScreenXY;
        public Rectangle Bounds;
        public Texture2D Icon;
        public MouseHandler MouseHandler;
        public Point ScreenHome, BoardHome;
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
                return IconDraggable is not null ? IconDraggable.Item : null;
            }

        }
        public InventoryGameBoard GameBoard;
        public ItemIconDraggable IconDraggable;


        public DraggableReceiver(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point screenHome, Point boardHome, InventoryGameBoard gameBoard)
        {

            Icon = ThruLib.makeBlankRect(graphics, 32, 32);
            MouseHandler = mouseHandler;
            Bounds = Icon.Bounds;
            this.ScreenHome = screenHome;
            BoardHome = boardHome;
            Bounds.Location = this.ScreenHome;
            IconDraggable = null;
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