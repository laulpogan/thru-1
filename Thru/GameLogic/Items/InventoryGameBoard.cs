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