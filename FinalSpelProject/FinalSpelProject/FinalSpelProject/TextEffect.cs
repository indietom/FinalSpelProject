using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class TextEffect:GameObject
    {
        float size;

        Color color;

        public TextEffect(Vector2 pos2, float size2, Color color2, float ang, float speed2)
        {
            Pos = pos2;
            color = color2;
            size = size2;
            Angle = ang;
            Speed = speed2;
        }
        public void Update()
        {
            AngleMath(false);
            Pos += Vel;
        }
    }
}
