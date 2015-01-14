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
        
        Chunk[] chunks;

        string levelPath;

        public Level()
        {
            levelPath = CurrentLevel + "\\";
            chunks = new Chunk[amountOfChunks];
            for(int i = 0; i < amountOfChunks; i++)
            {
                chunks[i] = new Chunk(new Vector2(0, (60*16)*i), levelPath+"chunk" + i);
            }
        }

        public void Update()
        {

        }
    }
}
