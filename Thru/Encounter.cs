using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection;
using System.Linq;

namespace Thru
{
	public class Encounter
	{
		public Dictionary<string, Player> Participants;
		public Location Location;
		public  string TestStat;
		public string successCondition, failureCondition;
		public string Title, Message;
		public int DC;
		public DisplayWindow DisplayWindow;
        public Dictionary<string, string> OptionsAndOutcomes;
		public ButtonGroup ButtonGroup;
        public Encounter(Dictionary<string, Player> participants, string testStat, int dc, string title, string message, DisplayWindow displayWindow, IServiceProvider services)
		{
			Participants = participants;
			TestStat = testStat;
			DC = dc;
			Title = title;
			Message = message;
			DisplayWindow = displayWindow;
			OptionsAndOutcomes = new Dictionary<string,string>(){
				{ "luck", "You chose A" },
				{ "strength", "You chose B" },
				{ "intelligence", "You chose C" },
				{ "charisma", "You chose D" }
            };
			Button[] buttonList = new Button[OptionsAndOutcomes.Keys.Count];
			Console.WriteLine("OptionsAndOutcomes.Keys.Count = " + OptionsAndOutcomes.Keys.Count);
			ContentManager Content = new ContentManager(services, "Content");

			Texture2D buttonImage = Content.Load<Texture2D>("longbutton");
			SpriteFont font = Content.Load<SpriteFont>("Score");
			for(int i = 0; i < OptionsAndOutcomes.Keys.Count; i++)
            {
				string key = OptionsAndOutcomes.Keys.ElementAt<string>(i);
				Button tempButton = new Button(buttonImage);
				tempButton.Text = key;
				tempButton.Font = font;
				Console.WriteLine(tempButton.Bounds.Height);
				buttonList[i]= tempButton;
			}
			foreach (Button button in buttonList)
			{
				Console.WriteLine(button.Text );

			}
			Console.WriteLine(buttonList.ToString());
			ButtonGroup = new ButtonGroup(buttonList, new Vector2(900, 200));
		}

		public State Update(GameTime gameTime)
		{
			foreach (Button button in ButtonGroup.ButtonList)
			{
				if (button.State == BState.JUST_RELEASED)
				{
					Message = OptionsAndOutcomes[button.Text];
					TestStat = button.Text;
					rollEncounter();

				}
			}
			DisplayWindow.Title = Title;
			DisplayWindow.Message = Message;
			DisplayWindow.Update();
			ButtonGroup.Update(gameTime);
			
			return State.Game;
		}

		public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			ButtonGroup.Draw(spriteBatch);
			DisplayWindow.Draw(spriteBatch,gameTime);
		}

		public void rollEncounter()
        {
			foreach(Player character in Participants.Values)
            {

				int y =character.stats[TestStat];
				if (y > DC)
                {
					Title = "Success!";
					Message= $"{character.Name} tenses and scrambles away, using their superior {TestStat} to their advantage. {character.Name} Succeeds!";
                } else
                {
					Title = "Failure!";
					Message = $"{character.Name} isn't quite {TestStat}-y enough to get the job done. {character.Name} Fails!";
				}
            }
        }
	}

}
