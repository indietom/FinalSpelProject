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
        byte chanceOfPowerUp;
        byte chanceOfEnemy;

        short[] spawnEnemyCounters;
        short[] MaxSpawnEnemyCounters;

        // long variable names > abbreviations amrite 
        short spawnPowerUpCount;
        short maxSpawnPowerUpCount;

        bool active;

        public SpawnManager()
        {
            amountOfEnemySpawners = 3;
            maxSpawnPowerUpCount = 128 * 10;
            spawnEnemyCounters = new short[amountOfEnemySpawners];
            MaxSpawnEnemyCounters = new short[amountOfEnemySpawners];
            active = true;
            MaxSpawnEnemyCounters[0] = 128 * 3;
            MaxSpawnEnemyCounters[1] = 128 * 5;
        }

        public void Update(List<Enemy> enemies, List<PowerUp> powerUps, LevelManager levelManager)
        {
            if(active)
            {
                EnemySpawnUpdate(enemies);
                PowerUpSpawnUpdate(powerUps, levelManager);
            }
        }

        public void PowerUpSpawnUpdate(List<PowerUp> powerUps, LevelManager levelManager)
        {
            Random random = new Random();
            spawnPowerUpCount += 10;
            if(spawnPowerUpCount >= maxSpawnPowerUpCount)
            {
                chanceOfPowerUp = (byte)random.Next(1, 16);
                //Console.WriteLine("LEL");
                if(chanceOfPowerUp == 4)
                {
                    // ayy lmao
                    powerUps.Add(new PowerUp(new Vector2(random.Next(Globals.screenW-32), random.Next(-128*2, -128)), (byte)random.Next(1, levelManager.GetLevelProperty(LevelManager.currentLevel).GetPowerUpRange()+1), 0, false));
                    chanceOfPowerUp = 0;
                }
                if (chanceOfPowerUp == 5)
                {
                    powerUps.Add(new PowerUp(new Vector2(random.Next(Globals.screenW - 32), random.Next(-128 * 2, -128)), (byte)random.Next(0, 5), 0, true));
                    chanceOfPowerUp = 0;
                }
                spawnPowerUpCount = 0;
            }
        }

        public void EnemySpawnUpdate(List<Enemy> enemies)
        {
            Random random = new Random();
            spawnEnemyCounters[0] += 1;
            if(spawnEnemyCounters[0] >= MaxSpawnEnemyCounters[0])
            {
                SpawnEnemiesLine(new Vector2(random.Next(Globals.screenW - 32), random.Next(-640, -400)), (byte)random.Next(3, 6), 18, 64, enemies);
                spawnEnemyCounters[0] = 0;
                MaxSpawnEnemyCounters[0] = (short)random.Next(128 * 8, 128 * 12);
            }
            spawnEnemyCounters[1] += 1;
            if(spawnEnemyCounters[1] >= MaxSpawnEnemyCounters[1])
            {
                chanceOfEnemy = (byte)random.Next(9);
                if(chanceOfEnemy < 4)
                {
                    SpawnEnemiesTriangle(new Vector2(random.Next(Globals.screenW - 128), -300), (byte)random.Next(3, 6), 18, 32, enemies);
                }
                spawnEnemyCounters[1] = (byte)random.Next(-128*3, 128 * 2);
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
