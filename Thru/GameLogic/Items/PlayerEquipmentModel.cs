using Microsoft.AspNetCore.Routing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using FontStashSharp;

namespace Thru
{
    public class PlayerEquipmentModel
    {
        public List<EquipmentReceiver>Receivers;
        public Dictionary<ItemSlot, CharacterModelSprite> EquippedSprites;
        public Point ModelOrigin;
        Character Player;
        public int gridMargin;
        public MouseHandler MouseHandler;
        public FreeSpace FreeSpace;
        SpriteFontBase Font;
       

        public PlayerEquipmentModel(GraphicsDeviceManager graphics, MouseHandler mouseHandler,  Character player, int margin, int iconSize, Point modelOrigin, float scale = 1f,SpriteFontBase font = null)
        {
            Player = player;
            Receivers = new List<EquipmentReceiver>();
            EquippedSprites = Player.CharacterModel.EquippedSprites;
            ModelOrigin = modelOrigin;
            gridMargin = margin + iconSize;
            MouseHandler = mouseHandler;
            FreeSpace = new FreeSpace();
            Font = font;

            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics,  new Point(0,1), this, ItemSlot.Backpack, Font, "Bag"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics,  new Point(1, 0), this, ItemSlot.Hat, Font, "Hat"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(1, 1), this, ItemSlot.Shirt, Font, "Shirt"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(1, 2), this, ItemSlot.Pants, Font, "Pants"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics,  new Point(1, 3), this, ItemSlot.Shoes, Font, "Shoes"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(2,1), this, ItemSlot.Poles, Font, "Poles"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(2, 2), this, ItemSlot.Misc1, Font, "Misc"));
            Receivers.Add(new EquipmentReceiver(mouseHandler, graphics, new Point(0, 2), this, ItemSlot.Misc2, Font, "Misc"));
            Player.ScreenXY += new Point(ModelOrigin.X, ModelOrigin.Y);

        }



        public GameState Update(GameTime gameTime)
        {
           
            foreach (EquipmentReceiver receiver in Receivers)
            {

                if (receiver.isOccupied)
                {
                       if(receiver.Item.AnimatedSprite != EquippedSprites[receiver.itemSlot])
                    {
                        EquippedSprites[receiver.itemSlot] =  receiver.Item.AnimatedSprite;
                        if (receiver.Item.SecondarySprite != null)
                            receiver.Item.SecondarySprite.Model = Player.CharacterModel;
                        if (receiver.itemSlot == ItemSlot.Shirt)
                            EquippedSprites[ItemSlot.Sleeves] = receiver.Item.SecondarySprite;
                        else if (receiver.itemSlot == ItemSlot.Backpack)
                            EquippedSprites[ItemSlot.BackpackStraps] = receiver.Item.SecondarySprite;
                    }  
                } else
                {
                    Player.CharacterModel.EquippedSprites[receiver.itemSlot] = null;
                     if (receiver.itemSlot == ItemSlot.Shirt)
                            EquippedSprites[ItemSlot.Sleeves] = null;
                        else if (receiver.itemSlot == ItemSlot.Backpack)
                            EquippedSprites[ItemSlot.BackpackStraps] = null;
                }
                                 receiver.Update(gameTime);   
            }
            Player.Update(gameTime);
            return GameState.Inventory;
        }


     
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EquipmentReceiver receiver in Receivers)
                receiver.Draw(spriteBatch);
                        Player.Draw(spriteBatch);

        }
    }
}
