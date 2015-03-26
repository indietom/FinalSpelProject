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

        bool looping;

        public Level(List<Chunk> chunks, byte currentLevel2, byte amountOfChunks2)
        {
            amountOfChunks = amountOfChunks2;
            CurrentLevel = currentLevel2;
            levelPath = "Content\\level"+CurrentLevel + "\\";
            for(int i = 0; i < amountOfChunks; i++)
            {
                chunks.Add(new Chunk(new Vector2(0, ((40*16)*i)-(40*16)*(amountOfChunks-1)), levelPath+"chunk" + i));
            }
        }

        public Level(List<Chunk> chunks, LevelProperty levelProperty)
        {
            amountOfChunks = levelProperty.GetHeight(); 
            CurrentLevel = LevelManager.currentLevel;
            levelPath = "Content\\level" + CurrentLevel + "\\";
            for (int i = 0; i < amountOfChunks; i++)
            {
                chunks.Add(new Chunk(new Vector2(0, ((40 * 16) * i) - (40 * 16) * (amountOfChunks - 1)), levelPath + "chunk" + i));
            }
        }

        public void Update(List<Tile> tiles, List<Chunk> chunks, ProceduralGenerationManager pgm, SpawnManager spawnManager, List<Enemy> enemies, List<PowerUp> powerUps, LevelManager levelManager)
        {
            if(chunks.Count == 1)
            {
                looping = true;
            }
            if(looping && CurrentLevel == 0)
            {
                pgm.SpawnLevelOne(tiles);
            }
            spawnManager.Update(enemies, powerUps, levelManager);
        }
    }
}
