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
        public Item  BearCan, ColdSoakJar, CookPot, IceAxe, Knife, MountainHouse, RawologyCorkball,
            SawyerBugRepellent, SawyerFilter, SleepingBag, Spoon, Spork, Stove, Tent, ToiletPaper, TrekkingPoles,
            WaterbottleClean, WaterbottleDirty;
        private ContentManager Content;
        public ButtonGroup buttonGroup;
        public Texture2D  BearCanImage, ColdSoakJarImage, CookPotImage, IceAxeImage, KnifeImage, MountainHouseImage,
            RawologyCorkballImage, SawyerBugRepellentImage, SawyerFilterImage, SleepingBagImage, SpoonImage, SporkImage, StoveImage,
            TentImage, ToiletPaperImage, TrekkingPolesImage, WaterbottleCleanImage, WaterbottleDirtyImage;
        public SpriteBatch spriteBatch;
        public Player Player;
        public Backpack Backpack;

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
            int[,] itemShape = new int[,]{
               { 0, 1, 0},
                { 1, 1, 0},
                { 0, 1, 0}
            };
            SpriteFont font = Content.Load<SpriteFont>("Score");
            Backpack = new Backpack(services, graphics, Player, globalState);
            BearCan = new Item(globalState.MouseHandler, BearCanImage, new Point(500, 250),false,4, 1.7f, 0, itemShape);
            ColdSoakJar = new Item(globalState.MouseHandler, ColdSoakJarImage, new Point(550, 250), false, 4, 1.7f, 0, itemShape);
            CookPot = new Item(globalState.MouseHandler, CookPotImage, new Point(600, 250), false, 4, 1.7f, 0, itemShape);
            IceAxe = new Item(globalState.MouseHandler, IceAxeImage, new Point(650, 250), false, 4, 1.7f, 0, itemShape);
            Knife = new Item(globalState.MouseHandler, KnifeImage, new Point(700, 250), false, 4, 1.7f, 0, itemShape);
            MountainHouse = new Item(globalState.MouseHandler, MountainHouseImage, new Point(500, 300), false, 4, 1.7f,0, itemShape);
            RawologyCorkball = new Item(globalState.MouseHandler, RawologyCorkballImage, new Point(500, 350), false,4, 1.7f, 0, itemShape);
            SawyerBugRepellent = new Item(globalState.MouseHandler, SawyerBugRepellentImage, new Point(500, 400), false, 4, 1.7f, 0, itemShape);
            SawyerFilter = new Item(globalState.MouseHandler, SawyerFilterImage, new Point(550, 300), false, 4, 1.7f, 0, itemShape);
            SleepingBag = new Item(globalState.MouseHandler, SleepingBagImage, new Point(600, 300), false, 4, 1.7f, 0, itemShape);
            Spoon = new Item(globalState.MouseHandler, SpoonImage, new Point(650, 300), false, 4, 1.7f, 0, itemShape);
            Spork = new Item(globalState.MouseHandler, SporkImage, new Point(700, 300), false, 4, 1.7f, 0, itemShape);
            Stove = new Item(globalState.MouseHandler, StoveImage, new Point(550, 350), false, 4, 1.7f, 0, itemShape);
            Tent = new Item(globalState.MouseHandler, TentImage, new Point(600, 350), false, 4, 1.7f, 0, itemShape);
            ToiletPaper = new Item(globalState.MouseHandler, ToiletPaperImage, new Point(650, 350), false, 4, 1.7f, 0, itemShape);
            TrekkingPoles = new Item(globalState.MouseHandler, TrekkingPolesImage, new Point(700, 350), false, 4, 1.7f, 0, itemShape);
            WaterbottleClean = new Item(globalState.MouseHandler, WaterbottleCleanImage, new Point(550, 400), false,4, 1.7f, 0, itemShape);
            WaterbottleDirty = new Item(globalState.MouseHandler, WaterbottleDirtyImage, new Point(600, 400), false, 4, 1.7f, 0, itemShape);
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
            Backpack.Update(gameTime);

            foreach (Item draggable in draggables)
            {
                draggable.Update(gameTime);
            }
            return GameState.Inventory;
        }

        public  void Draw(GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin();
            Backpack.Draw(spriteBatch);
          
            foreach (Item draggable in draggables)
            {
                draggable.Draw(spriteBatch);
            }
            Player.Draw(spriteBatch);
            spriteBatch.End();

        }
    }

     
}                      