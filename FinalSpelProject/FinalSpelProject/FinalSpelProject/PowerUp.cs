using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class PowerUp : GameObject
    {
        byte type;
        byte movmentPattern;

        bool special;

        float cosCount;

        public PowerUp(Vector2 pos2, byte type2, byte movmentPattern2, bool special2)
        {
            special = special2;
            movmentPattern = movmentPattern2;
            Pos = pos2;
            type = type2;
            SetSize(16);
            if(!special) SetSpriteCoords((short)(462+FrameX(type)), 1);
                 else SetSpriteCoords(FrameX(type), Frame(5));
            Speed = 4f;
        }
        public void Update(List<Player> players)
        {
            cosCount += 0.01f;
            switch (movmentPattern)
            {
                case 0:
                    Pos += new Vector2((float)Math.Cos((2 * (float)Math.PI * 1.2f) * cosCount), Speed);
                    break;
                case 1:
                    Pos += new Vector2(0, Game1.worldSpeed);
                    break;
            }
            HitBox = FullHitBox;
            foreach(Player p in players)
            {
                if(HitBox.Intersects(p.HitBox))
                {
                    p.SetGunType(type, false);
                    Destroy = true;
                }
            }
        }
    }
}
