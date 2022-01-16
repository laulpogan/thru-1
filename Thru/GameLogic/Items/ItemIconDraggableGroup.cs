using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Thru
{
    public class ItemIconDraggableGroup : IDraggable
    {
        public MouseHandler MouseHandler;
        public bool isBeingDragged
        {
            get
            {
                return MouseHandler.ItemDragged == Item;
            }
        }

        public enum GroupState
        {
            isOnBoard,
            isBeingDragged,
            isAtHome,
            isEquipped
        };
        public int gridMargin;
        public float Bulk;
        public Item Item;
        public ItemIconDraggable[,] Draggables;
        public Point ScreenHome;
        public Point BoardHome, BoardOrigin;
       
        public GroupState groupState
        {
            get
            {
                switch (Icon.IconState)
                {
                    case InventoryState.InventoryBoard:
                        return GroupState.isOnBoard;
                        break;
                    case InventoryState.Mouse:
                        return GroupState.isBeingDragged;
                        break;
                    case InventoryState.FreeSpace:
                        return GroupState.isAtHome;
                        break;
                    case InventoryState.Equipment:
                        return GroupState.isEquipped;
                        break;
                    default:
                        return GroupState.isAtHome;
                }
           
            }
        }

        public ItemSlot ItemSlot
        {
            get
            {
                return Item.ItemSlot;
            }
        }
        
        public ItemIconDraggable Icon
        {
            get
            {
                foreach (ItemIconDraggable draggable in Draggables)
                    if (draggable is not null)
                        return draggable;
                return null;
            }       
                
        }
        public Point CurrentPoint
        {
            get
            {
                switch (groupState)
                {
                    case GroupState.isOnBoard:
                        return InventoryGameBoard.getInventoryScreenXY(BoardHome.X, BoardHome.Y, BoardOrigin, gridMargin);
                        break;
                    case GroupState.isBeingDragged:
                        if (MouseHandler.iconHeld is not null)
                            return MouseHandler.mXY - new Point(MouseHandler.iconHeld.ShapeHome.X * gridMargin, MouseHandler.iconHeld.ShapeHome.Y * gridMargin) - new Point(gridMargin / 2, gridMargin / 2);
                        else
                            return ScreenHome;
                        break;
                    case GroupState.isEquipped:
                        return Icon.receiver.ScreenHome;
                            break;
                    case GroupState.isAtHome:
                        return ScreenHome;
                        break;
                    default:
                        return ScreenHome;
                            break;
                }

            }
            set { }
        }
        public int[,] trueShape;
        public int[,] ItemShape {
            get
            {
                for (int i = 0; i < Draggables.GetLength(0); i++)
                    for (int j = 0; j < Draggables.GetLength(1); j++)
                        if (Draggables[i, j] is not null)
                            trueShape[i, j] = 1;
                        else
                            trueShape[i, j] = 0;
                return trueShape;
            }
            set { trueShape = value; }
        
        }

        public ItemIconDraggable shapeOrigin;
      
      

        public ItemIconDraggableGroup(MouseHandler mouseHandler, Texture2D icon, int[,] itemShape, Point home,Point boardOrigin, Item item,SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            CurrentPoint = home;
            ScreenHome = home;
            BoardOrigin = boardOrigin;
            Item = item;
            ItemShape = itemShape;
            gridMargin = 50;
            Draggables = new ItemIconDraggable[itemShape.GetLength(0), itemShape.GetLength(1)];
            Bulk = itemShape.GetLength(0);
            ItemIconDraggable draggable;
            for (int i = 0; i < itemShape.GetLength(0); i++)
                for (int j = 0; j < itemShape.GetLength(1); j++)
                    if (itemShape[i, j] == 1)
                        Draggables[i, j] = new ItemIconDraggable(icon, new Point(i, j), item, this, font);

        }

        public GameState Update(GameTime gameTime)
        {
            foreach (ItemIconDraggable draggable in Draggables)
                if(draggable is not null)
                    draggable.Update(gameTime);
            return GameState.Inventory;
        }

        

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ItemIconDraggable draggable in Draggables)
                if (draggable is not null)
                    draggable.Draw(spriteBatch);
        }

        public void Rotate()
        {
            ThruLib.rotate90DegClockwise<ItemIconDraggable>(Draggables);
            ThruLib.rotate90DegClockwise<int>(ItemShape);

            for (int i = 0; i < ItemShape.GetLength(0); i++)
                for (int j = 0; j < ItemShape.GetLength(1); j++)
                    if (ItemShape[i, j] == 1)
                        if(Draggables[i,j] is not null)
                            Draggables[i, j].ShapeHome = new Point(i, j);
        }
        public void printShape()
        {

            Console.WriteLine("------------------");
            for (int i = 0; i < Draggables.GetLength(0); i++)
            {
                string duh = "";
                for (int j = 0; j < Draggables.GetLength(1); j++)
                    if (ItemShape[i, j] == 1)
                        duh += " " + new Point(i,j).ToString() + "O";
                    else if(Draggables[i,j] is not null)
                        duh += " " + Draggables[i, j].ShapeHome + "A";
                    else
                        duh += " " + new Point(i, j).ToString() + "Q";

                Console.WriteLine(duh);
            }

            Console.WriteLine("------------------");
        }

    }


}