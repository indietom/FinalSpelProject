using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Tile : GameObject
    {
        short type;
        byte currentLevel;

        public short GetType() { return type; }

        bool active;

        public bool GetActive() { return active; }

        public Tile(Vector2 pos2, short type2)
        {
            Pos = pos2;
            type = type2;
            AssignSprite();
        }

        public void Update()
        {
            Pos += new Vector2(0, Globals.worldSpeed);
            active = (Pos.Y >= -Height) ? true : false;
            Destroy = (Pos.Y >= Globals.screenH) ? true : Destroy;
        }
        
        public void AssignSprite()
        {
            Imx = (short)(16 * type);
            SetSize(16);
        }
    }
}
