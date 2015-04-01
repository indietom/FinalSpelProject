using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalSpelProject
{
    class TilesheetManager
    {
        public static Texture2D[] TileSheets;

        public static void Load(ContentManager contet)
        {
            TileSheets = new Texture2D[2];
            for(int i = 0; i < TileSheets.Count(); i++)
            {
                TileSheets[i] = contet.Load<Texture2D>("tilesheet" + i);
            }
        }
    }
}
