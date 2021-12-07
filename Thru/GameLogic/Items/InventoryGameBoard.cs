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
                    receivers[row,col] = new DraggableReceiver(mouseHandler, graphics, getScreenXY(row, col), new Point(row,col));
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

        public int[,]matrixMultiply(int[,] matrix1, int[,] matrix2)
        {
            int matrix1Rows = matrix1.GetLength(0);
            int matrix1Cols = matrix1.GetLength(1);
            int matrix2Rows = matrix2.GetLength(0);
            int matrix2Cols = matrix2.GetLength(1);
            int[,] product = new int[matrix1Rows, matrix2Cols];
            for(int i = 0; i<matrix1Rows; i++)
            {
                
                for(int x = 0; x < matrix2Cols; x++)
                {
                    int[] matrix1Elems = new int[matrix1Rows];
                    int[] matrix2Elems = new int[matrix2Cols];
                    for (int p = 0; p < matrix1Cols; p++)
                    {
                        matrix1Elems[p] = matrix1[p, i];  
                    }
                    for (int q = 0; q < matrix2Rows; q++)
                    {
                        matrix2Elems[q] = matrix2[q, x];
                    }
                    product[i, x] = dotProduct(matrix1Elems, matrix2Elems);
                }
            }

            
            return product;
        }

        public int dotProduct(int[]list1, int[] list2)
        {
            int size = list1.Length;
            int product = 0;
            for (int i = 0; i < size; i++)
            {
                product += list1[i] * list2[i];
            }
            return product;
        }

        public Point getScreenXY(int row, int col)
        {
            return new Point(Home.X + row * bloc, Home.Y + col * bloc);
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
    }


}