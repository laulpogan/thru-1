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
        public InventoryGameBoard(MouseHandler mouseHandler, GraphicsDeviceManager graphics, int rows, int columns, int margin, int iconSize, Point home)
        {
            receivers = new DraggableReceiver[rows,columns];
            board = new int[rows, columns];
            bloc = margin + iconSize;
            Home = home;
            for (int row = 0; row < rows; row ++)
            {
                for (int col = 0; col < columns; col ++)
                {
                    receivers[row,col] = new DraggableReceiver(mouseHandler, graphics, ThruLib.getInventoryScreenXY(row, col, Home, bloc), new Point(row,col), this);
                    board[row, col] = 0;
                }
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
            bool isValid = true;
            for (int row = point.X; row < rows; row++)
                for (int col = point.Y; col < columns; col++)
                    for (int x = 0; x < iShape.GetLength(0); x++)
                        for (int y = 0; y < iShape.GetLength(1); y++)
                            if (board[row, col] == 1 && iShape[x,y] == 1)
                            {
                                isValid = false;
                            }
                    return isValid;
        }

    }


}