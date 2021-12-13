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

        public Point getBoardShapeOrigin()
        {

            Point point = BoardHome;
            Point point2 = MouseHandler.iconDragged.ShapeHome;


            return point - point2 ;
        }
        public GameState Update(GameTime gameTime)
        {
            if (ThruLib.hit_image_alpha(
                    Bounds, Icon, MouseHandler.mx, MouseHandler.my))
            {
                if(MouseHandler.dragged != null && Item == null)
                {
                   if(MouseHandler.State == BState.JUST_RELEASED)
                    {
                        if(GameBoard.isValidMove(MouseHandler.dragged.ItemShape, getBoardShapeOrigin()))
                        {
                           MouseHandler.dragged.Draggable.receiver = this;
                           IconDraggable = MouseHandler.iconDragged;
                           MouseHandler.iconDragged.receiver = this;
                           MouseHandler.iconDragged.BoardHome = BoardHome;
                           GameBoard.updateBoardAndReceivers(MouseHandler.dragged, getBoardShapeOrigin());

                           
                                for (int i = 0; i < MouseHandler.dragged.ItemShape.GetLength(0); i++)
                                    for (int j = 0; j < MouseHandler.dragged.ItemShape.GetLength(1); j++)
                                        if (MouseHandler.dragged.ItemShape[i, j] == 1)
                                        {
                                          MouseHandler.iconDragged.BoardHome += new Point(i, j);
                                        }

                        }
                            
                        
                        
                    }
                }
            }
                return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Icon,Bounds , Color.Black);
        }


        //todo:reconsolidate to game board class, this fucking thing is a mess
      
    }


}