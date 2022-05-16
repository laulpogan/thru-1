using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using FontStashSharp;

namespace Thru
{
    public class Item
    {

        public Texture2D Icon;
        public Point Home;
        public float Bulk, Weight, renderScale;
        public bool isFlexible;
        public ItemIconDraggableGroup DraggableGroup;
        public ItemSlot ItemSlot;
        public int[,] ItemShape;
        public string Name, Description;
        public CharacterModelSprite AnimatedSprite;
        public CharacterModelSprite SecondarySprite;
        public string iconPath, spritePath, secondarySpritePath;
        public Point ScreenXY
        {
            get
            {
                return DraggableGroup.CurrentPoint;
            }
        }
        public enum ItemState
        {
            Equipped,
            Inventory
        }

        public Character Owner;
        public ItemState State;
        public Item(MouseHandler mouseHandler, string name, Texture2D icon, Point point, Point boardOrigin, bool isflexible, float bulk, float weight, float scale, int[,] itemShape, ItemSlot itemSlot, Texture2D animatedSprite = null, Texture2D secondarySprite = null, SpriteFontBase font = null)
        {
            Name = name;
            Icon = icon;
            Home = point;
            ItemShape = itemShape;
            renderScale = scale;
            Bulk = bulk;
            Weight = weight;
            isFlexible = isflexible;
            ItemSlot = itemSlot;
            if (animatedSprite is not null)
                AnimatedSprite = new CharacterModelSprite(animatedSprite, null, Color.White);
            else
                AnimatedSprite = null;
            if (secondarySprite is not null)
                SecondarySprite = new CharacterModelSprite(secondarySprite, null, Color.White);
            DraggableGroup = new ItemIconDraggableGroup(mouseHandler, Icon, itemShape, Home, boardOrigin, this, font);

        }

        public void Update(GameTime gameTime)
        {
            DraggableGroup.Update(gameTime);

            if (State == ItemState.Equipped && AnimatedSprite is not null)
            {
                AnimatedSprite.Update(gameTime);
                if (SecondarySprite is not null)
                    SecondarySprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DraggableGroup.Draw(spriteBatch);
        }



        public ItemData convertToRecord()
        {


            Console.WriteLine(ItemShape);
            Console.WriteLine(Home);
            ItemData itemData = new ItemData();
            itemData.name = Name;
            itemData.itemShape = DraggableGroup.ItemShape;
            itemData.isFlexible = isFlexible;
            itemData.bulk = Bulk;
            itemData.weight = Weight;
            itemData.itemSlot = ItemSlot;
            itemData.boardHome = DraggableGroup.BoardHome;
            itemData.screenXY = DraggableGroup.ScreenHome;
            itemData.iconPath = iconPath;
            itemData.spriteSheetPath = spritePath;
            itemData.secondarySpriteSheetPath = secondarySpritePath;
            return itemData;

        }
    }

}
