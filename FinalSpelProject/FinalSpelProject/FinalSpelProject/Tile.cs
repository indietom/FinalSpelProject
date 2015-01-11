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

        bool solid;

        public Tile(Vector2 pos2, byte type2, bool solid2)
        {
            AssignSprite();
        }
        public void Update()
        {

        }
        
        public void AssignSprite()
        {
            Imx = (byte)(16 * type);
        }
    }
}
