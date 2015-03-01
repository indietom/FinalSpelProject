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
        byte lineHeight;

        short[] spawnEnemyCounters;
        short[] MaxSpawnEnemyCounters;

        bool active;

        public SpawnManager()
        {
            amountOfEnemySpawners = 3;
            spawnEnemyCounters = new short[amountOfEnemySpawners];
            MaxSpawnEnemyCounters = new short[amountOfEnemySpawners];
            active = true;
            MaxSpawnEnemyCounters[0] = 128 * 3;
        }

        public void Update(List<Enemy> enemies, List<PowerUp> powerUps)
        {
            if(active)
            {
                EnemySpawnUpdate(enemies);
                PowerUpSpawnUpdate(powerUps);
            }
        }

        public void PowerUpSpawnUpdate(List<PowerUp> powerUps)
        {

        }

        public void EnemySpawnUpdate(List<Enemy> enemies)
        {
            Random random = new Random();
            spawnEnemyCounters[0] += 1;
            if(spawnEnemyCounters[0] >= MaxSpawnEnemyCounters[0])
            {
                SpawnEnemiesLine(new Vector2(random.Next(Globals.screenW - 32), random.Next(-640, -400)), (byte)random.Next(3, 6), 18, 64, enemies);
                spawnEnemyCounters[0] = 0;
                MaxSpawnEnemyCounters[0] = (short)random.Next(128 * 5, 128 * 8);
            }
        }

        public void SpawnEnemiesLine(Vector2 pos2, byte height, byte type, byte enemySize, List<Enemy> enemies)
        {
            Random random = new Random();
            for(int i = 0; i < height; i++)
            {
                if (i != 0) enemies.Add(new Enemy(new Vector2(pos2.X, pos2.Y + i * (enemySize + 3)), type, random));
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
