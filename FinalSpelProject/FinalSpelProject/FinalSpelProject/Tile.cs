using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Tile : GameObject
    {
        public byte Tag { get; set; }
        byte type;
        byte currentLevel;

        bool active;

        public bool GetActive() { return active; }

        public Tile(Vector2 pos2, byte type2)
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
            Imx = (byte)(16 * type);
            SetSize(16);
        }
    }
}
