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
        public bool isOccupied;
        public Item Item;
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
            Item = null;
            GameBoard = gameBoard;
        }

        public GameState Update(GameTime gameTime)
        {
            if (Item is not null)
                isOccupied = true;
            else
                isOccupied = false;
                return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Icon,Bounds , Color.Black);
        }


      
    }


}