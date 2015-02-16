using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Gib : GameObject
    {
        public Gib(Vector2 pos2, short frame2, short imy2, float spe, float ang)
        {
            Pos = pos2;
            SetSize(16);
            SetSpriteCoords(Frame(frame2, 16), imy2);
            Speed = spe;
            Angle = ang;
            Rotated = true;
        }

        public void Update()
        {
            AngleMath(false);
            Pos += Vel;
            Rotation += Speed;
        }
    }
}
