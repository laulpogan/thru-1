using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Thru
{
    public class DraggableGroup : IDraggable
    {

        public Point ScreenXY;
        public bool IsClicked;
        public List<ItemIconDraggable> Draggables;
        public Button Button;
        public Point ButtonHome, currentPoint;
        public MouseHandler MouseHandler;
        public bool isBeingDragged;
        public DraggableReceiver receiver, oldReceiver;
        public int gridMarginX, gridMarginY;
        float Bulk;
        Item Item;
        public int[,] itemShape;


        public DraggableGroup(MouseHandler mouseHandler, Texture2D icon, Point home, Item item, SpriteFont font = null)
        {
            MouseHandler = mouseHandler;
            Item = item;
            ButtonHome = home;
            currentPoint = home;
            gridMarginX = 18 + icon.Bounds.Width;
            Draggables = new List<ItemIconDraggable>();
            Bulk = item.Bulk;

            for(int i = 0; i < Bulk; i++)
            {
                Draggables.Add(new ItemIconDraggable(mouseHandler, icon, ButtonHome, item));
                ButtonHome.X += gridMarginX;

            }
           
            
        }

        public GameState Update(GameTime gameTime)
        {
           

            for (int i = 0; i < Bulk; i++)
            {
                
                if (Draggables[i].isBeingDragged)
                {
                    currentPoint = Draggables[i].Button.Bounds.Location;
                    currentPoint.X -= i * gridMarginX;
                    isBeingDragged = true;
                }
               


            }

            if (isBeingDragged)
                adjustGroupPosition(currentPoint);
            else
                currentPoint = ButtonHome;
            isBeingDragged = false;

           
            if (receiver is not null && receiver != oldReceiver)
            {
                ButtonHome = receiver.screenHome;
                receiver.item = Item;
                if (oldReceiver is not null)
                    oldReceiver.item = null;
                oldReceiver = receiver;
                receiver = null;
               // ButtonHome.X -= i * (grid_margin);
                for (int g = 0; g < Bulk; g++)
                {
                    Draggables[g].ButtonHome = ButtonHome;
                    Draggables[g].ButtonHome.X += g * gridMarginX;
                }
            }

            for (int i = 0; i < Bulk; i++)
                Draggables[i].Update(gameTime);
            return GameState.Inventory;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (ItemIconDraggable draggable in Draggables)
            {
                draggable.Draw(spriteBatch);
            }
        }

        public void adjustGroupPosition(Point point)
        {
            Draggables[0].Button.Bounds.Location = currentPoint;

            for(int i = 1; i < Bulk; i++)
            {
                Point tempPoint = Draggables[i - 1].Button.Bounds.Location;
                tempPoint.X += i * gridMarginX;
                Draggables[i].Button.Bounds.Location = tempPoint;
            }

        }

    }


}