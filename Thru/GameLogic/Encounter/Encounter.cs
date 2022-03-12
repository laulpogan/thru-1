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
		public InteractionMessageBox DisplayWindow;
        public Dictionary<string, EncounterOptionData> Options;
		public ButtonGroup ButtonGroup;
		public bool selectionMade;
		public Character character;
		public bool isResolved;
		Button okButton;
		ButtonGroup okButtonGroup;
		public Encounter(Character player, EncounterData data, Location location, IServiceProvider services, GraphicsDeviceManager graphics, GlobalState globalState)
		{
			character = player;
			selectionMade = false;
			Options = new Dictionary<string,EncounterOptionData>();
			ArrayList buttonList = new ArrayList();
			ContentManager Content = new ContentManager(services, "Content");
			Texture2D buttonImage = Content.Load<Texture2D>("InterfaceTextures/short_button");
			Title = data.title;
			Message = data.text;
			DisplayWindow = new InteractionMessageBox(Message,Title,services,graphics, 1000, 150, globalState.Font);
			foreach (EncounterOptionData option in data.options)
			{
				Button tempButton = new Button(globalState.MouseHandler, buttonImage);
				tempButton.Text = option.text;
				tempButton.Font = globalState.Font;
				buttonList.Add(tempButton);
				Options.Add(option.text, option);
			}
			ButtonGroup = new ButtonGroup(buttonList, new Vector2(800, 750), ButtonArrangement.Horizontal);
			okButton = new Button(globalState.MouseHandler, buttonImage, "OK",  globalState.Font);
			buttonList.Clear();
			buttonList.Add(okButton);
			okButtonGroup = new ButtonGroup(buttonList, new Vector2(800, 850), ButtonArrangement.Horizontal);

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
					break;
				case ResolutionType.Tramily:
					break;
				case ResolutionType.Triple:
					break;



			}
		}


		public State Update(GameTime gameTime)
		{
			foreach (Button button in ButtonGroup.ButtonList)
			{
				if (button.State == BState.JUST_RELEASED && !selectionMade)
				{
					Message = button.Text;
					rollEncounter(Options[button.Text]);
					ButtonGroup = okButtonGroup;
				}
			}
			DisplayWindow.Title = Title;
			DisplayWindow.Message = Message;
			DisplayWindow.Update();
			ButtonGroup.Update(gameTime);
			if (okButton.State == BState.JUST_RELEASED && selectionMade)
            {
				isResolved = true;
            }
			return State.Game;
		}

		public void Draw(SpriteBatch spriteBatch)
		{  
			if (!isResolved)
            {
				
				ButtonGroup.Draw(spriteBatch);
				DisplayWindow.Draw(spriteBatch);
			}
            
		}

		public bool rollEncounter(EncounterOptionData option)
        {
			selectionMade = true;
			bool success = false;
			Message = option.text;
			Random rand = new Random();
			int stat =character.Stats.get(option.checkStat);
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
				resolveEncounter(option.success);
            } else
            {
				resolveEncounter(option.failure);

			}
			return success;
        }

		public void resolveEncounter(EncounterResolutionData resolution)
        {
            int stat = character.Stats.get(resolution.effectedStat);
			character.Stats.set(resolution.effectedStat, stat + resolution.effect);
         }

    }

}
