using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Reflection;
using System.Linq;
using ADOX;
using CDO;

namespace Thru
{
	public class Encounter
	{

		public Location Location;
		public string Title, Message;
		public DisplayWindow DisplayWindow;
        public Dictionary<string, EncounterOptionData> Options;
		public ButtonGroup ButtonGroup;
		public bool selectionMade;
		public Player character;
		public Encounter(Player player, EncounterData data, Location location, IServiceProvider services, GraphicsDeviceManager graphics)
		{
			character = player;
			selectionMade = false;
			Options = new Dictionary<string,EncounterOptionData>();
			ArrayList buttonList = new ArrayList();
			ContentManager Content = new ContentManager(services, "Content");
			Texture2D buttonImage = Content.Load<Texture2D>("longbutton");
			SpriteFont font = Content.Load<SpriteFont>("Score");
			Title = data.title;
			Message = data.text;
			DisplayWindow = new DisplayWindow(Message,Title,services,graphics);
			foreach (EncounterOptionData option in data.options)
			{
				Button tempButton = new Button(buttonImage);
				tempButton.Text = option.text;
				tempButton.Font = font;
				buttonList.Add(tempButton);
				Options.Add(option.text, option);
			}
			ButtonGroup = new ButtonGroup(buttonList, new Vector2(250, 250));


            switch (data.resolutionType)
            {
				case ResolutionType.Cutscene:
					break;
				case ResolutionType.Duo:
					break;
				case ResolutionType.Leader:
					break;
				case ResolutionType.PVP:
					break;
				case ResolutionType.PVE:
					break;
				case ResolutionType.Quadruple:
					break;
				case ResolutionType.Random:
					break;
				case ResolutionType.SimpleMajority:
					foreach(ICharacter character in location.Characters)
                    {

                    }
					break;
				case ResolutionType.Tramily:
					break;
				case ResolutionType.Triple:
					break;



			}
		}


		/*public Encounter buildSampleEncounter()
		{
			Encounter encounter = new Encounter();
			return encounter;
		}*/
		public State Update(GameTime gameTime)
		{
			foreach (Button button in ButtonGroup.ButtonList)
			{
				if (button.State == BState.JUST_RELEASED && !selectionMade)
				{
					Message = button.Text;
					rollEncounter(Options[button.Text]);

				}
			}
			DisplayWindow.Title = Title;
			DisplayWindow.Message = Message;
			DisplayWindow.Update();
			ButtonGroup.Update(gameTime);
			
			return State.Game;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
            if (!selectionMade)
			{
				ButtonGroup.Draw(spriteBatch);
			}
			DisplayWindow.Draw(spriteBatch);
		}

		public bool rollEncounter(EncounterOptionData option)
        {
			selectionMade = true;
			bool success = false;
			Message = option.text;
			Random rand = new Random();
			int stat =character.stats.get(option.checkStat);
				if (stat + rand.Next(20) >= option.diceCheck)
                {
					Title = "Success!";
					Message= $"{character.Name} uses their superior {option.checkStat} to their advantage. {character.Name} Succeeds!";
					success = true;
			} else
                {
					Title = "Failure!";
					Message = $"{character.Name} isn't quite {option.checkStat}-y enough to get the job done. {character.Name} Fails!";
				}


            if (success)
            {
				resolveEncounter(option.consequence.success);
            } else
            {
				resolveEncounter(option.consequence.failure);

			}
			return success;
        }

		public void resolveEncounter(EncounterResolutionData resolution)
        {
            int stat = character.stats.get(resolution.effectedStat);
			character.stats.set(resolution.effectedStat, stat + resolution.effect);

        }

    }

}
