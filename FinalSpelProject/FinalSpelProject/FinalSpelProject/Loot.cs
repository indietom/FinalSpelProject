using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace FinalSpelProject
{
    class Loot : GameObject
    {
        byte movmentType;
        byte type;

        public Loot(Vector2 pos2, byte type2, byte movmentType2)
        {
            Pos = pos2;
            type = type2;
            movmentType = movmentType2;
            SetSpriteCoords((short)(780 + FrameX(type)), 69);
            SetSize(16);
        }
        
        public void Update(List<Player> players, List<TextEffect> textEffects)
        {
            switch(movmentType)
            {
                case 0:
                    Pos += new Vector2(0, Globals.worldSpeed);
                    break;
            }
            foreach(Player p in players)
            {
                if(p.FullHitBox.Intersects(FullHitBox))
                {
                    textEffects.Add(new TextEffect(Pos, GetWorth().ToString() + "+", 0.7f, Color.White, Pos - new Vector2(0, 64), 0.1f, 100, 3, 1));
                    p.RaiseScore(GetWorth());
                    SoundManager.PowerUp.Play();
                    Destroy = true;
                }
            }
        }

        public short GetWorth()
        {
            short[] worths = new short[6];
            
            worths[0] = 1000;
            worths[1] = 500;
            worths[2] = 1500;
            worths[3] = 1250;
            worths[4] = 900;
            worths[5] = 2000; 

            return worths[type];
        }
    }
}
