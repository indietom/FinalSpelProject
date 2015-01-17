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

        float cosCount;

        public PowerUp(Vector2 pos2, byte type2, byte movmentPattern2)
        {
            movmentPattern = movmentPattern2;
            Pos = pos2;
            type = type2;
            SetSize(16);
            SetSpriteCoords(FrameX(type), 32 * 4 + 5);
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
                    p.SetGunType(type);
                    Destroy = true;
                }
            }
        }
    }
}
