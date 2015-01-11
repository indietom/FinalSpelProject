using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalSpelProject
{
    class Chunk : GameObject
    {
        bool active;

        int[,] map;

        public Chunk(Vector2 pos2, string path)
        {
            Pos = pos2;

            FileManager fileManager = new FileManager();
            map = fileManager.LoadLevel(path);
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tilesheet)
        {
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    spriteBatch.Draw(tilesheet, new Vector2(x*16, y*16), new Rectangle(map[x, y]*16, 0, 16, 16), Color.White);
                }
            }
        }
    }
}
