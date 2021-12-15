using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Thru
{
    public class InventoryGameBoard
    {

        public Rectangle Bounds;
        public DraggableReceiver[,] receivers;
        public int[,] trueBoard;
        public int[,] board
        {
            get {
                foreach (DraggableReceiver receiver in receivers)
                    if (receiver is not null && receiver.isOccupied)
                        trueBoard[receiver.BoardHome.X, receiver.BoardHome.Y] = 1;
                
                return trueBoard;}
            set {}
        }
        public int[,] EmptyBoard;
        public int rows, columns, bloc;
        public Point BoardOrigin;
        public int gridMargin;
        public MouseHandler MouseHandler;
        public ItemIconDraggableGroup draggedIconGroup {
            get
            {
               return MouseHandler.dragged is not null ? MouseHandler.dragged.Draggable : null;
            }
        }
        
        public List<Item> draggables;
        public InventoryGameBoard(MouseHandler mouseHandler, GraphicsDeviceManager graphics, int rows, int columns, int margin, int iconSize, Point boardOrigin)
        {
            trueBoard = ThruLib.emptyBoard(rows, columns);
            receivers = new DraggableReceiver[rows,columns];
            bloc = margin + iconSize;
            BoardOrigin = boardOrigin;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            for (int row = 0; row < rows; row ++)
                for (int col = 0; col < columns; col ++)
                {
                    receivers[row,col] = new DraggableReceiver(mouseHandler, graphics, getInventoryScreenXY(new Point(row, col)), new Point(row,col), this);
                }
        }
       
       
        public GameState Update(GameTime gameTime)
        {
            
            if (MouseHandler.isDragging)
                draggedIconGroup.adjustGroupPosition(draggedIconGroup.CurrentPoint);


            if (MouseHandler.RState == BState.JUST_RELEASED && MouseHandler.isDragging)
                rotate90DegClockwise<ItemIconDraggable>(draggedIconGroup.Draggables);

            foreach (DraggableReceiver receiver in receivers) {
                if (ThruLib.hit_image_alpha(receiver.Bounds, receiver.Icon, MouseHandler.mx, MouseHandler.my))
                    if (MouseHandler.dragged != null && receiver.Item == null)
                        if (MouseHandler.State == BState.JUST_RELEASED)
                        {
                            if (isValidMove(MouseHandler.dragged.ItemShape, getBoardShapeOrigin(receiver)))
                                parseIconGroup(draggedIconGroup, receiver);
                            MouseHandler.iconDragged = null;
                        }
                receiver.Update(gameTime);
             }
     
            foreach (Item draggable in draggables)
                draggable.Update(gameTime);
            return GameState.Inventory;
        }


        public void parseSingleIcon(ItemIconDraggable draggable)
        {
            draggable.oldReceiver = draggable.receiver;
            draggable.receiver = null;
            if (draggable.oldReceiver is not null)
                draggable.oldReceiver.IconDraggable = null;
            Console.WriteLine("Adding Shape Coordinates " + draggable.ShapeHome + "and Board coordinates" + draggable.Group.BoardHome);
            Point newPoint = draggable.ShapeHome + draggable.Group.BoardHome;
            Console.WriteLine("New Point at " + newPoint);
            draggable.receiver = receivers[newPoint.X, newPoint.Y];
            draggable.receiver.IconDraggable = draggable;
        }

        public void parseIconGroup(ItemIconDraggableGroup draggableGroup, DraggableReceiver receiver)
        {
            draggableGroup.oldReceiver = draggableGroup.receiver;
            draggableGroup.receiver = receiver;
            MouseHandler.iconDragged.receiver = receiver;
            foreach (ItemIconDraggable draggable in draggableGroup.Draggables)
                if (draggable is not null)
                    parseSingleIcon(draggable);
        }
        public void matchIconsToReceivers(ItemIconDraggableGroup group, Point point)
        {
            foreach(ItemIconDraggable draggable in group.Draggables)
                if(draggable is not null)
                {

                }
        }

                    


    public Point getBoardShapeOrigin(DraggableReceiver receiver)
        {

            Point point = receiver.BoardHome;
            Point point2 = MouseHandler.iconDragged.ShapeHome;


            return point - point2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DraggableReceiver receiver in receivers)
            {
                if (receiver.isOccupied)
                    receiver.Item.Draw(spriteBatch);
                receiver.Draw(spriteBatch);
            }
            foreach (Item draggable in draggables)
            {
                draggable.Draw(spriteBatch);
            }
        }

        public  Point getInventoryScreenXY(Point BoardHome)
        {
            return new Point(BoardOrigin.X + (BoardHome.X * gridMargin), BoardOrigin.Y + (BoardHome.Y * gridMargin));
        }

        public static T[,] rotate90DegClockwise<T>(T[,] a)
        {
            int N = a.GetLength(0);
            // It will traverse the each cycle
            for (int i = 0; i < N / 2; i++)
            {
                for (int j = i; j < N - i - 1; j++)
                {

                    // It will swap elements of each cycle in clock-wise direction
                    T temp = a[i, j];
                    a[i, j] = a[N - 1 - j, i];
                    a[N - 1 - j, i] = a[N - 1 - i, N - 1 - j];
                    a[N - 1 - i, N - 1 - j] = a[j, N - 1 - i];
                    a[j, N - 1 - i] = temp;
                }
            }
            return a;
        }

        public bool isValidMove(int[,] iShape, Point point)
        {
            ThruLib.printLn(board);
            int newX = iShape.GetLength(0) + point.X;
            int newY = iShape.GetLength(1) + point.Y;
            if (newX > board.GetLength(0) || point.X < 0)
            {
                Console.WriteLine("Failed to place piece at (" + point + "): going off top or bottom edge");
                return false;
            }
            if (newY > board.GetLength(1) || point.Y < 0)
            {
                Console.WriteLine("Failed to place piece at ("+ point +"): going off left or right edge");
                return false;
            }

            for (int x = 0; x < iShape.GetLength(0); x++)
                for (int y = 0; y < iShape.GetLength(1); y++)
                    if (iShape[x, y] == 1)
                        if (board[x + point.X, y + point.Y] == 1)
                        {
                            Console.WriteLine("Y + point.y = "+ y +point.Y);
                            Console.WriteLine("X + point.x = " + x + point.X);
                            Console.WriteLine("Failed to place piece at (" + point + "): collision at (" + x + point.X + "," + y + point.Y +")");
                            ThruLib.printLn(iShape);
                            ThruLib.printLn(board); 
                            return false;
                        }
                     
                    

                        
            return true;
        }

        //getting the length of the shape arrays to where they have blocks, not their whole.
        public int[] getTrueLength(int[,] iShape)
        {
            int isRowsEmpty, isColsEmpty;
            int rowsISawNumbers = iShape.GetLength(0);
            int colsISawNumbers = iShape.GetLength(1);
            for (int x = 0; x < iShape.GetLength(0); x++)
            {
                isRowsEmpty = 0;
                isColsEmpty = 0;
                for (int y = 0; y < iShape.GetLength(1); y++)
                {
                    isRowsEmpty += iShape[x, y];
                    isColsEmpty += iShape[y, x];
                }
                if (isRowsEmpty > 0)
                    rowsISawNumbers = x;
                if (isColsEmpty > 0)
                    colsISawNumbers = x;
            }
            int[] trueLength = new int[2];
            trueLength[0] = rowsISawNumbers;
            trueLength[1] = colsISawNumbers;
            return trueLength;
                
        }
     

        


           

            }
        }
       