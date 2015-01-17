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
        float cosCount;

        public PowerUp(Vector2 pos2, byte type2)
        {
            Pos = pos2;
            type = type2;
            SetSize(16);
            SetSpriteCoords(FrameX(type), 32 * 4 + 5);
            Speed = 4f;
        }
        public void Update(List<Player> players)
        {
            cosCount += 0.01f;
            Pos += new Vector2((float)Math.Cos((2*(float)Math.PI*1.2f)*cosCount), Speed);
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
