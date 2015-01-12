using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

// Chunk size should be 20*60 tiles

namespace FinalSpelProject
{
    class Chunk : GameObject
    {
        bool active;

        int[,] map;

        public Chunk(Vector2 pos2, string path)
        {
            Pos = pos2;

            active = true;

            FileManager fileManager = new FileManager();
            map = fileManager.LoadLevel(path);
        }

        public void Update()
        {
            Pos += new Vector2(0, Game1.worldSpeed);
            //Pos += new Vector2(0, 1);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tilesheet)
        {
            if (active)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    for (int y = 0; y < map.GetLength(0); y++)
                    {
                        spriteBatch.Draw(tilesheet, new Vector2((x * 16)+Pos.X, (y * 16)+Pos.Y), new Rectangle(map[y,x]*16, 0, 16, 16), Color.White);
                    }
                }
            }
        }
    }
}
