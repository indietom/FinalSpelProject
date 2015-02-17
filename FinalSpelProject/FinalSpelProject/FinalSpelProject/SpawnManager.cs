using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class SpawnManager
    {
        byte amountOfEnemySpawners = 3;

        short[] spawnEnemyTimers = new short[amountOfEnemySpawners];
        short[] MaxSpawnEnemyTimers = new short[amountOfEnemySpawners];

        public SpawnManager()
        {

        }

        public void Update()
        {

        }

        public void PowerUpSpawnUpdate()
        {

        }

        public void EnemySpawnUpdate()
        {
 
        }

        public void SpawnEnemiesLine(Vector2 pos2, byte height, byte type, List<Enemy> enemies)
        {

        }

        public void SpawnEnemiesTriangle(Vector2 pos2, byte width, byte height, byte type, byte enemySize, List<Enemy> enemies)
        {
            Random random = new Random();
            for (int i = 0; i < height; i++ )
            {
                enemies.Add(new Enemy(new Vector2(pos2.X + (i + 1) * enemySize, pos2.Y - i * enemySize), type, random));
                enemies.Add(new Enemy(new Vector2(pos2.X - (i - 1) * enemySize, pos2.Y - i * enemySize), type, random));
            }
        }
        public void SpawnEnemiesTriangle(Vector2 pos2, byte width, byte height, byte type, List<Enemy> enemies)
        {
            SpawnEnemiesTriangle(pos2, width, height, type, 64, enemies);
        }
    }
}
