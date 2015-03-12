using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class LevelManager
    {
        // Don't run this code yet, it works now but it's still mostly test stuff
        public static byte currentLevel = 0;

        LevelProperty[] levelProperties = new LevelProperty[5];

        public LevelManager()
        {
            levelProperties[0] = new LevelProperty(0, 15, 7);
            levelProperties[1] = new LevelProperty(0, 15, 7);
            levelProperties[2] = new LevelProperty(0, 15, 7);
            levelProperties[3] = new LevelProperty(0, 15, 7);
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
