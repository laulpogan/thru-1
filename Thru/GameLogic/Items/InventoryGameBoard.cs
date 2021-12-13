using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;

namespace Thru
{
    public class InventoryGameBoard
    {

        public Rectangle Bounds;
        public DraggableReceiver[,] receivers;
        public int[,] board;
        public int rows, columns, bloc;
        public Point Home;
        public int[,] itemShape;
        public int gridMargin;
        public InventoryGameBoard(MouseHandler mouseHandler, GraphicsDeviceManager graphics, int rows, int columns, int margin, int iconSize, Point home)
        {
            receivers = new DraggableReceiver[rows,columns];
            board = new int[rows, columns];
            bloc = margin + iconSize;
            Home = home;
            gridMargin = margin + iconSize;
            for (int row = 0; row < rows; row ++)
                for (int col = 0; col < columns; col ++)
                {
                    receivers[row,col] = new DraggableReceiver(mouseHandler, graphics, ThruLib.getInventoryScreenXY(row, col, Home, bloc), new Point(row,col), this);
                    board[row, col] = 0;
                }


            
            
            itemShape = new int[,]
            {
                {1}
            };
            itemShape = new int[,]
            {
                {1,1}
            };
            itemShape = new int[,]
            {
                {0,1},
                {1,1}
            };
            itemShape = new int[,]{
                { 0, 0, 0},
                { 1, 1, 0},
                { 0, 1, 0}
            };
            itemShape = new int[,]{
                { 0, 0, 0, 0},
                { 1, 1, 0, 0},
                { 0, 1, 0, 0},
                { 0, 1, 0, 0},
            };
            itemShape = new int[,]{
                { 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0},
                { 0, 1, 0, 0, 0},
                { 0, 1, 0, 0, 0},
                { 0, 1, 0, 0, 0}
            };
            itemShape = new int[,]{
                { 0, 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0}
            };
            itemShape = new int[,]{
                { 0, 0, 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0}
            };
            itemShape = new int[,]{
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0}
            };

            itemShape = new int[,]{
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 1, 1, 0, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0, 0},
                { 0, 1, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0}
            };

        }
       
        public GameState Update(GameTime gameTime)
        {
            foreach (DraggableReceiver receiver in receivers)
            {
                receiver.Update(gameTime);
            }

            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DraggableReceiver receiver in receivers)
            {
                receiver.Draw(spriteBatch);
            }
        }

 

        public void checkCollision()
        {
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < columns; col++)
                    if (receivers[row, col] is not null)
                    {
                        if (receivers[row, col].isOccupied)
                        {

                        }
                    }
        }

   
        public bool isValidMove(int[,] iShape, Point point)
        {

            if (iShape.GetLength(0) + point.X > board.GetLength(0))
                return false;
            if (iShape.GetLength(1) + point.Y > board.GetLength(1))
                return false;

            for (int x = 0; x < iShape.GetLength(0); x++)
                for (int y = 0; y < iShape.GetLength(1); y++)
                    if (iShape[x, y] == 1)
                        if (board[x + point.X, y + point.Y] == 1)
                            return false;
                    
            return true;
        }
        public void updateBoardAndReceivers(Item Item, Point point)
        {
            DraggableReceiver tempReceiver;
            for (int x = 0; x < Item.ItemShape.GetLength(0); x++)
                for (int y = 0; y < Item.ItemShape.GetLength(1); y++)
                    if (Item.ItemShape[x, y] == 1)
                    {
                        int newX = x + point.X;
                        int newY = y + point.Y;
                        board[newX, newY] = 1;
                        Item.Draggable.Draggables[x, y].BoardHome = new Point(newX, newY);
                        Item.Draggable.Draggables[x, y].ScreenHome = getBoardXY(point, x, y);
                        tempReceiver = receivers[newX, newY];
                        tempReceiver.IconDraggable = Item.Draggable.Draggables[x, y];
                        tempReceiver.isOccupied = true;
                    }

        }

        public Point getBoardXY(Point point, int row, int col)
        {
            return new Point(Home.X + (row * gridMargin), Home.Y + (col * gridMargin));

        }

        public Point findCenter(int[,] iShape) { return Point.Zero; }
        public static Point getBoardShapeOrigin(Point BoardHome, Point ShapeHome)
        {
            return BoardHome - ShapeHome;
        }

    }

   


}