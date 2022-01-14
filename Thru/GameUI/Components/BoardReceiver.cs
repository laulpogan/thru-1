using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace Thru
{
    public class BoardReceiver  : IDraggableContainer
    {

        public Vector2 ScreenXY;
        public Rectangle Bounds;
        public Texture2D Icon;
        public MouseHandler MouseHandler;
        public Point ScreenHome {
            get
            {
                return ThruLib.getInventoryScreenXY(BoardHome.X, BoardHome.Y, GameBoard.BoardOrigin, GameBoard.gridMargin);
            }
            set { } } 
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
                return iconHeld is not null ? iconHeld.Group.Item : null;
            }
            set { }

        }
        public InventoryGameBoard GameBoard;
        public ItemIconDraggable iconHeld { get; set; }


        public BoardReceiver(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point boardHome, InventoryGameBoard gameBoard)
        {

            Icon = ThruLib.makeBlankRect(graphics, 32, 32);
            MouseHandler = mouseHandler;
            Bounds = Icon.Bounds;
            BoardHome = boardHome;
            iconHeld = null;
            GameBoard = gameBoard;
            Bounds.Location = this.ScreenHome;

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