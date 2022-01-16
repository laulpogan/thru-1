using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Thru
{
    public class PlayerEquipmentModel
    {
        public List<EquipmentReceiver>Receivers;
        public Point ModelOrigin;
        Player Player;
        public int gridMargin;
        public MouseHandler MouseHandler;
        public FreeSpace FreeSpace;
        SpriteFont Font;
       

        public PlayerEquipmentModel(GraphicsDeviceManager graphics, MouseHandler mouseHandler,  Player player, int margin, int iconSize, Point modelOrigin, SpriteFont font = null)
        {
            Player = player;
           
            Receivers = new List<EquipmentReceiver>();
           
            ModelOrigin = modelOrigin;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            FreeSpace = new FreeSpace();
            Font = font;

            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics,  new Point(0,1), this, ItemSlot.Bag, Font, "Bag"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics,  new Point(1, 0), this, ItemSlot.Hat, Font, "Hat"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(1, 1), this, ItemSlot.Shirt, Font, "Shirt"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(1, 2), this, ItemSlot.Pants, Font, "Pants"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics,  new Point(1, 3), this, ItemSlot.Shoes, Font, "Shoes"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(2,1), this, ItemSlot.Poles, Font, "Poles"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(2, 2), this, ItemSlot.Misc, Font, "Misc"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(0, 2), this, ItemSlot.Misc, Font, "Misc"));
            Player.ScreenXY += new Vector2(3 * gridMargin, 0);

        }

        public GameState Update(GameTime gameTime)
        {
           
            Player.Update(gameTime);
            foreach (EquipmentReceiver receiver in Receivers)
                receiver.Update(gameTime);
            return GameState.Inventory;
        }


     
        public void Draw(SpriteBatch spriteBatch)
        {
            Player.Draw(spriteBatch);
            foreach (EquipmentReceiver receiver in Receivers)
                receiver.Draw(spriteBatch);
        }
    }
}
