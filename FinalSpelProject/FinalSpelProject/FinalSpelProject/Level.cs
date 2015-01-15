using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Level
    {
        public byte CurrentLevel { get; set; }
        byte amountOfChunks;

        string levelPath;

        public Level(List<Chunk> chunks, byte currentLevel2)
        {
            CurrentLevel = currentLevel2;
            levelPath = CurrentLevel + "\\";
            for(int i = 0; i < amountOfChunks; i++)
            {
                chunks.Add(new Chunk(new Vector2(0, (60*16)*i), levelPath+"chunk" + i));
            }
        }

        public void Update()
        {

        }
    }
}
