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
               return MouseHandler.dragged is not null ? MouseHandler.dragged.DraggableGroup : null;
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
                            if (isValidMove(MouseHandler.dragged.DraggableGroup, getBoardShapeOrigin(receiver)))
                                handOffIconGroup(draggedIconGroup, receiver.BoardHome);
                            MouseHandler.iconHeld = null;
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
                draggable.oldReceiver.iconHeld = null;
            Console.WriteLine("Adding Shape Coordinates " + draggable.ShapeHome + "and Board coordinates" + draggable.Group.BoardHome);
            Point newPoint = draggable.ShapeHome + draggable.Group.BoardHome;
            Console.WriteLine("New Point at " + newPoint);
            draggable.receiver = receivers[newPoint.X, newPoint.Y];
            draggable.receiver.iconHeld = draggable;
        }

        public void parseIconGroup(ItemIconDraggableGroup draggableGroup, DraggableReceiver receiver)
        {
            draggableGroup.oldReceiver = draggableGroup.receiver;
            draggableGroup.receiver = receiver;
            MouseHandler.iconHeld.receiver = receiver;
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

        public void handOffIcon(ItemIconDraggable draggable, IDraggableContainer container)
        {
            draggable.oldReceiver = draggable.receiver;
            if(draggable.receiver is not null)
                draggable.receiver.iconHeld = null;
            container.iconHeld = draggable;
            draggable.receiver = container;
        }
        
        public void handOffIconGroup(ItemIconDraggableGroup group, Point point)
        {
            foreach (ItemIconDraggable draggable in group.Draggables)
                if (draggable is not null)
                    handOffIcon(draggable, receivers[point.X + draggable.ShapeHome.X, point.Y + draggable.ShapeHome.Y]);
        }


    public Point getBoardShapeOrigin(DraggableReceiver receiver)
        {

            Point point = receiver.BoardHome;
            Point point2 = MouseHandler.iconHeld.ShapeHome;


            return point - point2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (DraggableReceiver receiver in receivers)
            {
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

        public bool isValidMove(ItemIconDraggableGroup group, Point point)
        {
            ThruLib.printLn(board);

            if (!ThruLib.isInBounds(group.Item.ItemShape, point, rows, columns))
                return false;
            foreach (ItemIconDraggable draggable in group.Draggables)
            {
                try
                {
                    if(receivers[point.X + draggable.ShapeHome.X, point.Y + draggable.ShapeHome.Y].isOccupied)
                    {
                        return false;
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                } 
            }
            return true;
        }


       
       /* public bool isValidMove(int[,] iShape, Point point)
        {
           


            

            for (int x = 0; x < shapeWidth; x++)
                for (int y = 0; y < shapeHeight; y++)
                    if (iShape[x, y] == 1)
                        if (board[x + point.X, y + point.Y] == 1)
                        {
                            Console.WriteLine("Y + point.y = "+ y +point.Y);
                            Console.WriteLine("X + point.x = " + x + point.X);
                            Console.WriteLine("Failed to place piece at (" + point + "): collision at (" + x + point.X + "," + y + point.Y +")");
                            ThruLib.printLn(iShape);
                            ThruLib.printLn(board); 
      
        }
                     
                    

                        
            return true;
        }*/

        //getting the length of the shape arrays to where they have blocks, not their whole.
        
     

        


           

            }
        }
       