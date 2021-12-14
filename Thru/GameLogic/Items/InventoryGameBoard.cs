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
        public int[,] currentBoard
        {
            get { return board; }
            set
            {
                if (board != value)
                {
                    Console.WriteLine("Changed Board from " + board + " to " + value);
                }
                board =value;
            }
        }
        public int rows, columns, bloc;
        public Point Home;
        public int[,] itemShape;
        public int gridMargin;
        public MouseHandler MouseHandler;
        ItemIconDraggableGroup tempDraggable;
        Item tempItem;
        public InventoryGameBoard(MouseHandler mouseHandler, GraphicsDeviceManager graphics, int rows, int columns, int margin, int iconSize, Point home)
        {
            receivers = new DraggableReceiver[rows,columns];
            board = new int[rows, columns];
            bloc = margin + iconSize;
            Home = home;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            tempDraggable = null;
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

            if (MouseHandler.isDragging)
            {
                receiverShuffle();
                for (int i = 0; i < MouseHandler.dragged.ItemShape.GetLength(0); i++)
                    for (int j = 0; j < MouseHandler.dragged.ItemShape.GetLength(1); j++)
                        if (MouseHandler.dragged.Draggable.Draggables[i, j] is not null && MouseHandler.dragged.Draggable.Draggables[i, j].isBeingDragged)
                        {
                            tempDraggable.currentPoint = tempDraggable.Draggables[i, j].Button.Bounds.Location;
                            tempDraggable.currentPoint.X -= i * tempDraggable.gridMarginX;
                            tempDraggable.currentPoint.Y -= j * tempDraggable.gridMarginY;
                            tempDraggable.isBeingDragged = true;
                        }
                tempDraggable.adjustGroupPosition(tempDraggable.currentPoint, tempDraggable.currentBoardPoint);
            }

            if (MouseHandler.RState == BState.JUST_RELEASED && MouseHandler.dragged.Draggable == tempDraggable)
            {
                Console.WriteLine("Before");
                printLn(tempDraggable.Item.ItemShape);
                ThruLib.rotate90DegClockwise<int>(tempDraggable.Item.ItemShape);
                ThruLib.rotate90DegClockwise<ItemIconDraggable>(tempDraggable.Draggables);
                Console.WriteLine("After");
                printLn(tempDraggable.Item.ItemShape);
                for (int i = 0; i < tempItem.ItemShape.GetLength(0); i++)
                    for (int j = 0; j < tempItem.ItemShape.GetLength(1); j++)
                        if (tempItem.ItemShape[i, j] == 1 && tempDraggable.Draggables[i, j] is not null)
                        {
                            tempDraggable.Draggables[i, j].ScreenHome = ThruLib.getInventoryScreenXY(i, j, tempDraggable.BoardHome, gridMargin);
                            Point newBoardPosition = new Point(tempDraggable.Draggables[i, j].ShapeHome.X, tempDraggable.Draggables[i, j].ShapeHome.Y);
                            tempDraggable.Draggables[i, j].BoardHome = newBoardPosition;
                        }
            }

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
            int newX = iShape.GetLength(0) + point.X;
            int newY = iShape.GetLength(1) + point.Y;
            if (newX > board.GetLength(0)-1 || newX <0)
                return false;
            if (newY> board.GetLength(1) -1|| newY< 0)
                return false;

            for (int x = 0; x < newX; x++)
                for (int y = 0; y < newY; y++)
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

        public void receiverShuffle()
        {
            tempItem = MouseHandler.dragged == null ? null : MouseHandler.dragged;
            if (tempItem is not null)
                tempDraggable = tempItem.Draggable;


            else
            {
                tempDraggable.currentPoint = tempDraggable.ScreenHome;
                tempDraggable.currentBoardPoint = tempDraggable.BoardHome;

                tempDraggable.isBeingDragged = false;


                if (tempDraggable.receiver is not null && tempDraggable.receiver != tempDraggable.oldReceiver)
                {
                    tempDraggable.BoardHome = tempDraggable.receiver.BoardHome - tempDraggable.receiver.IconDraggable.ShapeHome;
                    tempDraggable.ScreenHome = tempDraggable.receiver.ScreenHome - ThruLib.pointTransform(tempDraggable.receiver.IconDraggable.ShapeHome, tempDraggable.gridMarginX);
                    tempDraggable.receiver.Item = tempDraggable.Item;
                    if (tempDraggable.oldReceiver is not null)
                        tempDraggable.oldReceiver.Item = null;
                    tempDraggable.oldReceiver = tempDraggable.receiver;
                    tempDraggable.receiver = null;
                    for (int i = 0; i < tempDraggable.Item.ItemShape.GetLength(0); i++)
                        for (int j = 0; j < tempDraggable.Item.ItemShape.GetLength(1); j++)
                            if (tempDraggable.Draggables[i, j] is not null)
                            {
                                tempDraggable.Draggables[i, j].receiver = receivers[tempDraggable.BoardHome.X + i, tempDraggable.BoardHome.Y + j];
                                if (tempDraggable.Draggables[i, j].receiver != tempDraggable.Draggables[i, j].oldReceiver)
                                {
                                    tempDraggable.Draggables[i, j].ScreenHome = tempDraggable.Draggables[i, j].receiver.ScreenHome;
                                    tempDraggable.Draggables[i, j].receiver.Item = tempDraggable.Item;
                                    if (tempDraggable.Draggables[i, j].oldReceiver is not null)
                                    {
                                        tempDraggable.Draggables[i, j].oldReceiver.Item = null;
                                        tempDraggable.Draggables[i, j].oldReceiver.isOccupied = false;
                                    }


                                    tempDraggable.Draggables[i, j].oldReceiver = tempDraggable.Draggables[i, j].receiver;
                                    tempDraggable.Draggables[i, j].receiver = null;
                                }

                                tempDraggable.Draggables[i, j].receiver.isOccupied = false;
                                tempDraggable.Draggables[i, j].receiver.isOccupied = false;
                                tempDraggable.Draggables[i, j].BoardHome = tempDraggable.BoardHome + new Point(i, j);
                            }
                }




           

            }
        }
        public void printLn(int[,] input)
        {

            Console.WriteLine("------------------");
            for (int i = 0; i < input.GetLength(0); i++)
            {
                string duh = "";
                for (int j = 0; j < input.GetLength(1); j++)
                    duh += " " + input[i, j].ToString();
                Console.WriteLine(duh);
            }

            Console.WriteLine("------------------");

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