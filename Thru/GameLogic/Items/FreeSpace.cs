using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static Thru.DraggableReceiver;

namespace Thru
{
    public class FreeSpace : IDraggableContainer
    {

        public MouseHandler MouseHandler;
        public Point ScreenHome { get; set; }
        public Point BoardHome { get; set; }
        public InventoryState receiverType { get; set; }
        public ItemSlot itemSlot { get; set; }

        public ItemIconDraggable iconHeld { get; set; }
        public List<ItemIconDraggable> iconsHeld { get; set; }


        public FreeSpace()
        {
            receiverType = InventoryState.FreeSpace;
            BoardHome = new Point (-1,-1);
            ScreenHome = new Point(0, 0);
            iconHeld = null;
            iconsHeld = new List<ItemIconDraggable>();
        }

        public GameState Update(GameTime gameTime)
        {
            return GameState.Inventory;
        }

   



    }


}