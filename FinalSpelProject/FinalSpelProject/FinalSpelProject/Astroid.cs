using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    // This could be a part of the projectile class but I think it will look cleaner this way 
    class Astroid : GameObject
    {
        byte size;
        byte type;

        float acceleration;
        float sway;

        public Astroid(Vector2 pos2, byte size2, byte type2, float speed2, float acceleration2, float sway)
        {
            Speed = speed2;
            acceleration = acceleration2;
            Pos = pos2;
            size = size2;
            size = ((byte)size == 0) ? (byte)64 : (byte)128;
            type = type2;
            SetSize(size);
            SetSpriteCoords((short)GetSprite().X, (short)GetSprite().Y);
        }

        public void Update(List<Player> players)
        {
            Speed += acceleration;
            Pos += new Vector2(sway, Speed);
            Rotation += Speed;
            foreach(Player p in players)
            {
                if(p.FullHitBox.Intersects(FullHitBoxMiddle))
                {
                    p.Dead = false;
                }
            }
            Destroy = (Pos.Y >= Globals.screenH + Height) ? true : Destroy;
        }

        public Point GetSprite()
        {
            return new Point(FrameX(type), (1170-64)+size/2+1);
        }
    }
}
