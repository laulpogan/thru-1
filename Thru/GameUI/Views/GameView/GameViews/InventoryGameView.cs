using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Reflection.Metadata;

namespace Thru
{
    public class InventoryGameView : IGameView
    {

        public ArrayList receivers, draggables;
        public ItemIconDraggable  BearCan, ColdSoakJar, CookPot, IceAxe, Knife, MountainHouse, RawologyCorkball,
            SawyerBugRepellent, SawyerFilter, SleepingBag, Spoon, Spork, Stove, Tent, ToiletPaper, TrekkingPoles,
            WaterbottleClean, WaterbottleDirty;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public Texture2D  BearCanImage, ColdSoakJarImage, CookPotImage, IceAxeImage, KnifeImage, MountainHouseImage,
            RawologyCorkballImage, SawyerBugRepellentImage, SawyerFilterImage, SleepingBagImage, SpoonImage, SporkImage, StoveImage,
            TentImage, ToiletPaperImage, TrekkingPolesImage, WaterbottleCleanImage, WaterbottleDirtyImage;
        public SpriteBatch spriteBatch;
        public Player Player;
        public Item Backpack;

        public InventoryGameView(IServiceProvider services, GraphicsDeviceManager graphics, Player player, GlobalState globalState)
{
            Player = player;
            Content = new ContentManager(services, "Content");
            BearCanImage = Content.Load<Texture2D>("ItemIcons/Bearcan32x32");
            ColdSoakJarImage = Content.Load<Texture2D>("ItemIcons/ColdSoakJar32x32");
            CookPotImage = Content.Load<Texture2D>("ItemIcons/CookPot32x32");
            IceAxeImage = Content.Load<Texture2D>("ItemIcons/IceAx32x32");
            KnifeImage = Content.Load<Texture2D>("ItemIcons/Knife32x32");
            MountainHouseImage = Content.Load<Texture2D>("ItemIcons/MountainHouse32x32");
            RawologyCorkballImage = Content.Load<Texture2D>("ItemIcons/RawologyCorkballLarge32x32");
            SawyerBugRepellentImage = Content.Load<Texture2D>("ItemIcons/Sawyer-BugRepellent32x32");
            SawyerFilterImage = Content.Load<Texture2D>("ItemIcons/Sawyer-Filter32x32");
            SleepingBagImage = Content.Load<Texture2D>("ItemIcons/SleepingBag132x32");
            SpoonImage = Content.Load<Texture2D>("ItemIcons/Spoon32x32");
            SporkImage = Content.Load<Texture2D>("ItemIcons/Spork32x32");
            StoveImage = Content.Load<Texture2D>("ItemIcons/Stove32x32");
            TentImage = Content.Load<Texture2D>("ItemIcons/Tent132x32");
            ToiletPaperImage = Content.Load<Texture2D>("ItemIcons/ToiletPaper32x32");
            TrekkingPolesImage = Content.Load<Texture2D>("ItemIcons/TrekkingPoles32x32");
            WaterbottleCleanImage = Content.Load<Texture2D>("ItemIcons/Waterbottle-CLEAN32x32");
            WaterbottleDirtyImage = Content.Load<Texture2D>("ItemIcons/Waterbottle-DIRTY32x32");


            Backpack = new Item(Content.Load<Texture2D>("ItemIcons/Backpack-Raptor1-32x32"), new Point(250,250));
            SpriteFont font = Content.Load<SpriteFont>("Score");
            receivers = new ArrayList();
            for(int x = 1050; x<= 1800; x += 50)
            {
                for (int y = 250; y<= 800; y += 50)
                {
                    receivers.Add(new DraggableReceiver(globalState.MouseHandler, graphics, new Point(x, y)));
                }
            }
            
            BearCan = new ItemIconDraggable(globalState.MouseHandler, BearCanImage, new Point(500, 250), font);
            ColdSoakJar = new ItemIconDraggable(globalState.MouseHandler, ColdSoakJarImage, new Point(550, 250), font);
            CookPot = new ItemIconDraggable(globalState.MouseHandler, CookPotImage, new Point(600, 250), font);
            IceAxe = new ItemIconDraggable(globalState.MouseHandler, IceAxeImage, new Point(650, 250), font);
            Knife = new ItemIconDraggable(globalState.MouseHandler, KnifeImage, new Point(700, 250), font);
            MountainHouse = new ItemIconDraggable(globalState.MouseHandler, MountainHouseImage, new Point(500, 300), font);
            RawologyCorkball = new ItemIconDraggable(globalState.MouseHandler, RawologyCorkballImage, new Point(500, 350), font);
            SawyerBugRepellent = new ItemIconDraggable(globalState.MouseHandler, SawyerBugRepellentImage, new Point(500, 400), font);
            SawyerFilter = new ItemIconDraggable(globalState.MouseHandler, SawyerFilterImage, new Point(550, 300), font);
            SleepingBag = new ItemIconDraggable(globalState.MouseHandler, SleepingBagImage, new Point(600, 300), font);
            Spoon = new ItemIconDraggable(globalState.MouseHandler, SpoonImage, new Point(650, 300), font);
            Spork = new ItemIconDraggable(globalState.MouseHandler, SporkImage, new Point(700, 300), font);
            Stove = new ItemIconDraggable(globalState.MouseHandler, StoveImage, new Point(550, 350), font);
            Tent = new ItemIconDraggable(globalState.MouseHandler, TentImage, new Point(600, 350), font);
            ToiletPaper = new ItemIconDraggable(globalState.MouseHandler, ToiletPaperImage, new Point(650, 350), font);
            TrekkingPoles = new ItemIconDraggable(globalState.MouseHandler, TrekkingPolesImage, new Point(700, 350), font);
            WaterbottleClean = new ItemIconDraggable(globalState.MouseHandler, WaterbottleCleanImage, new Point(550, 400), font);
            WaterbottleDirty = new ItemIconDraggable(globalState.MouseHandler, WaterbottleDirtyImage, new Point(600, 400), font);
            spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            draggables = new ArrayList(){
                BearCan, ColdSoakJar, CookPot, IceAxe, Knife, MountainHouse, RawologyCorkball,
                SawyerBugRepellent, SawyerFilter, SleepingBag, Spoon, Spork, Stove, Tent, ToiletPaper, TrekkingPoles,
                WaterbottleClean, WaterbottleDirty
        };
        }

        public  GameState Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            foreach(DraggableReceiver receiver in receivers)
            {
                receiver.Update(gameTime);
            }
             foreach(ItemIconDraggable draggable in draggables)
            {
                draggable.Update(gameTime);
            }
            Backpack.Update(gameTime);
            return GameState.Inventory;
        }

        public  void Draw(GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            Backpack.Draw(spriteBatch);
            foreach (DraggableReceiver receiver in receivers)
            {
                receiver.Draw(spriteBatch);
            }
            foreach (ItemIconDraggable draggable in draggables)
            {
                draggable.Draw(spriteBatch);
            }
            Player.Draw(spriteBatch);
            spriteBatch.End();

        }
    }

     
}                      