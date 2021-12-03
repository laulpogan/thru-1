using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace Thru
{
    public class HikerSocialRelationship
    {

       

        public class HikerDisposition
        {

            public ICharacter DispositionHolder, DispositionSubject;
            public int Affinity;
            public enum Relationship
            {
                Stranger,
                Friend,
                Tramily,
                Lover,
                Enemy,
                Rival,
                Confidante
            }
            public enum Stage
            {
                Rising,
                Falling,
                Neutral,
                Holding,
                Former,
                Over
            }
            public Relationship relationship, aspirationalRelationship;
            public HikerDisposition(ICharacter dispositionHolder, ICharacter dispositionSubject, int affinity)
            {
                DispositionHolder = dispositionHolder;
                DispositionSubject = dispositionSubject;
                Affinity = affinity;
            }


        }

        public ICharacter Char1, Char2;
        public HikerDisposition char1tochar2, char2tochar1;
        public ArrayList Memories;
        public bool haveMet; 
        public HikerSocialRelationship(ICharacter char1, ICharacter char2)
        {
            Char1 = char1;
            Char2 = char2;
            char1tochar2 = new HikerDisposition(Char1, Char2, 0);
            char2tochar1 = new HikerDisposition(Char2, Char1, 0);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}