using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class LevelManager
    {
        // Don't run this code yet, it will crash
        public static byte currentLevel = 0;

        LevelProperty[] levelProperties = new LevelProperty[4];

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

        public void StartLevel(byte currentLevel2)
        {
            currentLevel = currentLevel2;
            // Call resetLevel function
            // add level
        }
    }

    struct LevelProperty
    {
        byte tag;
        byte height;

        public LevelProperty(byte tag2, byte height2)
        {
            tag = tag2;
            height = height2;
        }

        public byte GetTag() { return tag; }
        public byte GetHeight() { return height; }
    }
}
