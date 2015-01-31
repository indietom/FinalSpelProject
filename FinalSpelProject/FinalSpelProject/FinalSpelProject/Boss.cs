using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Boss
    {

    }
    struct BossPart
    {
        Vector2 Pos { get; set; }

        byte type;
        byte hp;
        byte width;
        byte height;
        
        public byte GetHp() { return hp; }
        public byte GetType() { return type; }

        public short imx;
        public short imy;

        public Rectangle FullHitBox { get { return new Rectangle((int)Pos.X, (int)Pos.Y, width, height); } }

        public void Update()
        {

        }

        public void Draw()
        {

        }
    }
}
