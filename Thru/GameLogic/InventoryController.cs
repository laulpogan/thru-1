using Newtonsoft.Json;
using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Thru
{
    public class InventoryController
    {

        ArrayList Draggables, Items, Receivers;
        public InventoryGameView InventoryGameView;
        public ButtonGroup ButtonGroup;
        public MouseState mouseState;
        bool mpressed, prev_mpressed = false;

        public InventoryController()
        {
            mpressed = mouseState.LeftButton == ButtonState.Pressed;
        }

        public void checkMenu()
        {
            mouseState = Mouse.GetState();
            prev_mpressed = mpressed;
            mpressed = mouseState.LeftButton == ButtonState.Pressed;
            if (mpressed)
            {
                DragAndDrop();
            }
           
        }
        public bool isOverReceiver(DraggableReceiver receiver, ItemIconDraggable draggable)
        {
            return receiver.Bounds.Contains(draggable.ScreenXY);
        }

        public void DragAndDrop()
        {
            foreach (DraggableReceiver receiver in Receivers)
            {
                foreach(ItemIconDraggable draggable in Draggables)
                {
                    if(isOverReceiver(receiver,draggable)){
                       draggable.ScreenHome = receiver.Bounds.Location;
                    }
                }
            }
        }
    }


}