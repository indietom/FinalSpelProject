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
        short worth;

        byte movmentType;
        byte type;

        public Loot(Vector2 pos2, byte type2, byte movmentType2)
        {
            Pos = pos2;
            type = type2;
            movmentType = movmentType2;
            SetSpriteCoords((short)(780 + FrameX(type)), 69);
            SetSize(16);
            worth = GetWorth();
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
