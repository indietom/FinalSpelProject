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
        bool hasSpawnedEnemies;

        byte currentLevel;

        int[,] map;
        int[,] mapE;

        public Chunk(Vector2 pos2, string path)
        {
            Pos = pos2;

            FileManager fileManager = new FileManager();
            map = fileManager.LoadLevel(path);
            mapE = fileManager.LoadLevel(path+"E"); 
        }

        public void Update(List<Enemy> enemies)
        {
            Pos += new Vector2(0, Game1.worldSpeed);
            if (Pos.Y >= -(mapE.GetLength(0) * 16) && Pos.Y <= 480)
                active = true;
            if(Pos.Y >= 480)
                active = false;
            SpawnEnemies(enemies);
        }

        public void SpawnEnemies(List<Enemy> enemies)
        {
            if (active && !hasSpawnedEnemies)
            {
                for (int x = 0; x < mapE.GetLength(1); x++)
                {
                    for (int y = 0; y < mapE.GetLength(0); y++)
                    {
                        string enemyToSpawnString = (currentLevel+1) + mapE[y, x].ToString();
                        byte enemyTypeByte = byte.Parse(enemyToSpawnString);
                        if(enemyTypeByte > 10) enemies.Add(new Enemy(new Vector2(x * 16, y * 16), enemyTypeByte)); 
                    }
                }
            }
            hasSpawnedEnemies = true;
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
