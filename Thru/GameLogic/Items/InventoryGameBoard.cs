﻿using Microsoft.Xna.Framework;
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
        public int[,] board;
        public int[,] currentBoard
        {
            get { return board; }
            set
            {
                if (board != value)
                {
                    ThruLib.printLn(board);
                }
                board =value;
            }
        }
        public int rows, columns, bloc;
        public Point BoardOrigin;
        public int gridMargin;
        public MouseHandler MouseHandler;
        public ItemIconDraggableGroup tempDraggable;
        public Item tempItem;
        public List<Item> draggables;
        public InventoryGameBoard(MouseHandler mouseHandler, GraphicsDeviceManager graphics, int rows, int columns, int margin, int iconSize, Point boardOrigin)
        {
            receivers = new DraggableReceiver[rows,columns];
            board = new int[rows, columns];
            bloc = margin + iconSize;
            BoardOrigin = boardOrigin;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            tempDraggable = null;
            for (int row = 0; row < rows; row ++)
                for (int col = 0; col < columns; col ++)
                {
                    receivers[row,col] = new DraggableReceiver(mouseHandler, graphics, getInventoryScreenXY(row, col, BoardOrigin, bloc), new Point(row,col), this);
                    board[row, col] = 0;
                }
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
                            tempDraggable.currentPoint.X -= i * gridMargin;
                            tempDraggable.currentPoint.Y -= j * gridMargin;
                            tempDraggable.isBeingDragged = true;
                        }
                tempDraggable.adjustGroupPosition(tempDraggable.currentPoint);
            }

            if (MouseHandler.RState == BState.JUST_RELEASED && MouseHandler.isDragging)
            {
                rotate90DegClockwise<int>(tempDraggable.Item.ItemShape);
                rotate90DegClockwise<ItemIconDraggable>(tempDraggable.Draggables);
                for (int i = 0; i < tempItem.ItemShape.GetLength(0); i++)
                    for (int j = 0; j < tempItem.ItemShape.GetLength(1); j++)
                        if (tempItem.ItemShape[i, j] == 1 && tempDraggable.Draggables[i, j] is not null)
                        {
                            tempDraggable.Draggables[i, j].ScreenHome = getInventoryScreenXY(i, j, tempDraggable.BoardHome, gridMargin);
                            Point newBoardPosition = new Point(tempDraggable.Draggables[i, j].ShapeHome.X, tempDraggable.Draggables[i, j].ShapeHome.Y);
                            tempDraggable.Draggables[i, j].BoardHome = newBoardPosition;
                        }
            }

            foreach (DraggableReceiver receiver in receivers)
            {
                if (ThruLib.hit_image_alpha(
                    receiver.Bounds, receiver.Icon, MouseHandler.mx, MouseHandler.my))
                {
                    if (MouseHandler.dragged != null && receiver.Item == null)
                    {
                        if (MouseHandler.State == BState.JUST_RELEASED)
                        {
                            if (isValidMove(MouseHandler.dragged.ItemShape, getBoardShapeOrigin(receiver)))
                            {
                                MouseHandler.dragged.Draggable.receiver = receiver;
                                MouseHandler.iconDragged.receiver = receiver;
                                updateBoardAndReceivers(MouseHandler.dragged, getBoardShapeOrigin(receiver));
                            }



                        }
                    }
                }
                if (receiver.Item is not null)
                    board[receiver.BoardHome.X, receiver.BoardHome.Y] = 1;
                else
                    board[receiver.BoardHome.X, receiver.BoardHome.Y] = 0;
                receiver.Update(gameTime);
            }
            foreach (Item draggable in draggables)
            {
                if (draggable.Draggable.receiver is not null)
                    board[draggable.Draggable.receiver.BoardHome.X, draggable.Draggable.receiver.BoardHome.Y] = 1;
                draggable.Update(gameTime);
            }
            return GameState.Inventory;
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
                Point tempPoint;

                if (tempDraggable.receiver is not null && tempDraggable.receiver != tempDraggable.oldReceiver)
                {
                    tempDraggable.BoardHome = tempDraggable.receiver.BoardHome - tempDraggable.receiver.IconDraggable.ShapeHome;
                    tempDraggable.ScreenHome = tempDraggable.receiver.ScreenHome - ThruLib.pointTransform(tempDraggable.receiver.IconDraggable.ShapeHome, gridMargin);
                    tempDraggable.receiver.Item = tempDraggable.Item;
                    if (tempDraggable.oldReceiver is not null)
                    {
                        tempDraggable.oldReceiver.Item = null;
                        tempDraggable.oldReceiver.isOccupied = false;
                        board[tempDraggable.oldReceiver.BoardHome.X, tempDraggable.oldReceiver.BoardHome.Y] = 0;
                    }
                    tempDraggable.oldReceiver = tempDraggable.receiver;
                    tempDraggable.receiver = null;
                    for (int i = 0; i < tempDraggable.Item.ItemShape.GetLength(0); i++)
                        for (int j = 0; j < tempDraggable.Item.ItemShape.GetLength(1); j++)
                            if (tempDraggable.Draggables[i, j] is not null)
                            {
                                tempPoint = new Point(tempDraggable.BoardHome.X + i, tempDraggable.BoardHome.Y + j);
                                tempDraggable.Draggables[i, j].receiver = receivers[tempDraggable.BoardHome.X + i, tempDraggable.BoardHome.Y + j];
                                if (tempDraggable.Draggables[i, j].receiver != tempDraggable.Draggables[i, j].oldReceiver)
                                {
                                    tempDraggable.Draggables[i, j].ScreenHome = tempDraggable.Draggables[i, j].receiver.ScreenHome;
                                    tempDraggable.Draggables[i, j].receiver.Item = tempDraggable.Item;
                                    if (tempDraggable.Draggables[i, j].oldReceiver is not null)
                                    {

                                        board[tempDraggable.Draggables[i, j].oldReceiver.BoardHome.X, tempDraggable.Draggables[i, j].oldReceiver.BoardHome.Y] = 0;
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
                receiver.Draw(spriteBatch);
            }
            foreach (Item draggable in draggables)
            {
                draggable.Draw(spriteBatch);
            }
        }

        //todo this is broken and combining board and screen coords ugh
        public static Point getInventoryScreenXY(int row, int col, Point BoardHome, int marginStep)
        {
            return new Point(BoardHome.X + (row * marginStep), BoardHome.Y + (col * marginStep));
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
            int newX = getTrueLength(iShape)[0] + point.X;
            int newY = getTrueLength(iShape)[1] + point.Y;
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

            for (int x = 0; x < getTrueLength(iShape)[0]; x++)
                for (int y = 0; y < getTrueLength(iShape)[1]; y++)
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
        public void updateBoardAndReceivers(Item Item, Point point)
        {
            DraggableReceiver tempReceiver;
            for (int x = 0; x < Item.ItemShape.GetLength(0); x++)
                for (int y = 0; y < Item.ItemShape.GetLength(1); y++)
                    if (Item.ItemShape[x, y] == 1 && Item.Draggable.Draggables is not null)
                    {
                        int newX = x + point.X;
                        int newY = y + point.Y;
                        board[newX, newY] = 1;
                        if(x! > Item.Draggable.Draggables.GetLength(0) && y! > Item.Draggable.Draggables.GetLength(1))
                            if(Item.Draggable.Draggables[x, y] is not null)
                            {
                                Item.Draggable.Draggables[x, y].BoardHome = new Point(newX, newY);
                                Item.Draggable.Draggables[x, y].ScreenHome = getInventoryScreenXY( x, y, point, gridMargin);
                            }
                            
                        tempReceiver = receivers[newX, newY];
                        tempReceiver.IconDraggable = Item.Draggable.Draggables[x, y];
                        tempReceiver.isOccupied = true;
                    }
            ThruLib.printLn(Item.ItemShape);
            ThruLib.printLn(board);
        }

        //formerly the receiver update method
        


           

            }
        }
       