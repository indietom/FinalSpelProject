using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class SpawnManager
    {
        byte amountOfEnemySpawners;

        short[] spawnEnemyTimers;
        short[] MaxSpawnEnemyTimers;

        public SpawnManager()
        {
            amountOfEnemySpawners = 3;
            spawnEnemyTimers = new short[amountOfEnemySpawners];
            MaxSpawnEnemyTimers = new short[amountOfEnemySpawners];
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

        public void SpawnEnemiesLine(Vector2 pos2, byte height, byte type, byte enemySize, List<Enemy> enemies)
        {
            Random random = new Random();
            for(int i = 0; i < height; i++)
            {
                if(i != 0) enemies.Add(new Enemy(new Vector2(pos2.X, pos2.Y+i*(enemySize+3)), type, random));
                else enemies.Add(new Enemy(new Vector2(pos2.X, pos2.Y + i * (enemySize + 3)), type, random, true));
            }
        }

        public void SpawnEnemiesLine(Vector2 pos2, byte height, byte type, List<Enemy> enemies)
        {
            SpawnEnemiesLine(pos2, height, type, enemies);
        }
        public void SpawnEnemiesTriangle(Vector2 pos2, byte height, byte type, byte enemySize, List<Enemy> enemies)
        {
            Random random = new Random();
            for (int i = 0; i < height; i++ )
            {
                enemies.Add(new Enemy(new Vector2(pos2.X + (i + 1) * enemySize, pos2.Y - i * enemySize), type, random));
                enemies.Add(new Enemy(new Vector2(pos2.X - (i - 1) * enemySize, pos2.Y - i * enemySize), type, random));
            }
        }
        public void SpawnEnemiesTriangle(Vector2 pos2, byte height, byte type, List<Enemy> enemies)
        {
            SpawnEnemiesTriangle(pos2, height, type, 64, enemies);
        }
    }
}
