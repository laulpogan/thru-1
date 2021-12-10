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
        public Texture2D icon;
        public MouseHandler MouseHandler;
        public Point screenHome, boardHome, boardOrigin;
        public bool isOccupied;
        public Item item;
        public InventoryGameBoard GameBoard;

        public DraggableReceiver(MouseHandler mouseHandler, GraphicsDeviceManager graphics, Point home, Point BoardHome, InventoryGameBoard gameBoard)
        {

            icon = ThruLib.makeBlankRect(graphics, 32, 32);
            MouseHandler = mouseHandler;
            Bounds = icon.Bounds;
            screenHome = home;
            Bounds.Location = screenHome;
            item = null;
            GameBoard = gameBoard;
        }

        public GameState Update(GameTime gameTime)
        {
            if (ThruLib.hit_image_alpha(
                    Bounds, icon, MouseHandler.mx, MouseHandler.my))
            {
                if(MouseHandler.dragged != null && item == null)
                {
                   if(MouseHandler.State == BState.JUST_RELEASED)
                    {
                        if(GameBoard.isValidMove(MouseHandler.dragged.ItemShape, boardHome))
                        {
                            MouseHandler.dragged.Draggable.receiver = this;
                            GameBoard.board[boardHome.X, boardHome.Y] = 1;
                            for (int f = 0; f < GameBoard.board.GetLength(0); f++)
                                for (int l = 0; l < item.Bulk; l++)
                                 //todo: loop over board?? I need to match the
                            for (int i = 0; i < item.Bulk; i++)
                                for (int j = 0; j < item.Bulk; j++)
                                    if (item.ItemShape[i, j] == 1)
                                    {
                                        //todo: change the board state of the spots where the other icons were

                                    }

                        }
                            
                        
                        
                    }
                }
            }
                return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon,Bounds , Color.Black);
        }
    }


}