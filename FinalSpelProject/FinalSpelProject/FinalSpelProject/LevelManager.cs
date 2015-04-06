using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class LevelManager
    {
        public static byte currentLevel = 0;

        LevelProperty[] levelProperties = new LevelProperty[5];

        public LevelProperty GetLevelProperty(int index)
        {
            return levelProperties[index];
        }

        public bool levelCompleted;

        short changeLevelCount;
        short maxChangeLevelCount;

        public LevelManager()
        {
            levelProperties[0] = new LevelProperty(0, 15, 4);
            levelProperties[1] = new LevelProperty(0, 15, 5);
            levelProperties[2] = new LevelProperty(0, 15, 5);
            levelProperties[3] = new LevelProperty(0, 24, 7);
            maxChangeLevelCount = 64 * 3;
        }

        public void Update(List<Chunk> chunks, List<Enemy> enemies, List<Projectile> projectiles, List<Player> players, List<Boss> bosses, Level level, List<TextEffect> textEffects)
        {
            foreach(Boss b in bosses)
            {
                if (b.levelCompleted)
                {
                    levelCompleted = true;
                }
            }
            if (levelCompleted)
            {
                changeLevelCount += 1;
                foreach(Player p in players)
                {
                    p.inputActive = false;
                    if(changeLevelCount <= 64)
                    {
                        p.Pos = new Vector2(p.Lerp(p.Pos.X, Globals.screenW / 2 - p.Width / 2, 0.07f), p.Lerp(p.Pos.Y, Globals.screenH / 2 - p.Height / 2, 0.001f));
                    }
                    else
                    {
                        p.Pos = new Vector2(p.Pos.X, p.Lerp(p.Pos.Y, -p.Height*3, 0.07f));
                    }
                }
            }
            if(changeLevelCount >= maxChangeLevelCount)
            {
                foreach (Player p in players)
                    if(p.GetLevelsCompleted() < currentLevel) p.SetLevelsCompleted(currentLevel);
                currentLevel += 1;
                StartLevel(currentLevel, chunks, enemies, projectiles, players, level);
                Globals.gameState = GameStates.LevelTransition;
                players[0].inputActive = true;
                levelCompleted = false;
                changeLevelCount = 0;
            }
            if(changeLevelCount == 3)
            {
                textEffects.Add(new TextEffect(new Vector2(0, 0), "", 1, Color.White, new Vector2(Globals.screenW / 2-100, Globals.screenH / 2), 0.05f, 64 * 3, 4, 1, "LEVEL COMPLETED!"));
            }
        }

        public void ResetLevel(List<Chunk> chunks, List<Enemy> enemies, List<Projectile> projectiles, List<Player> players, Level level)
        {
            chunks.Clear();
            enemies.Clear();
            projectiles.Clear();
            foreach (Player p in players)
            {
                p.Pos = new Vector2(Globals.screenW / 2 - p.Width / 2, Globals.screenH / 2 - p.Height / 2);
            }
            level = new Level(chunks, levelProperties[currentLevel]);
        }

        public void StartLevel(byte currentLevel2, List<Chunk> chunks, List<Enemy> enemies, List<Projectile> projectiles, List<Player> players, Level level)
        {
            currentLevel = currentLevel2;
            ResetLevel(chunks, enemies, projectiles, players, level);
        }
    }

    struct LevelProperty
    {
        byte tag;
        byte height;
        byte powerUpRange;

        public LevelProperty(byte tag2, byte height2, byte powerUpRange2)
        {
            tag = tag2;
            height = height2;
            powerUpRange = powerUpRange2;
        }

        public byte GetPowerUpRange() { return powerUpRange; }
        public byte GetTag() { return tag; }
        public byte GetHeight() { return height; }
    }
}
