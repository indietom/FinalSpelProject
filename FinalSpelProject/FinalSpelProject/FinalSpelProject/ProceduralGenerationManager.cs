﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class ProceduralGenerationManager
    {
        byte roadDirection;
        byte lakeSize;

        short roadSpawnCount;
        short maxRoadSpawnCount;
        short lakeSpawnCount;
        short maxLakeSpawnCount;

        bool roadDirectionChange;
        bool spawningRoads;
        bool spawningLakes;

        public ProceduralGenerationManager()
        {

        }

        public void Update()
        {

        }
        
        public void SpawnRoad()
        {

        }

        public void SpawnLakes()
        {

        }

        // hopefully this will make the actual procedural-generation algorithm more readable 
        public void RoadTurn(Vector2 pos2, bool left, List<Tile> tiles)
        {
            Point turnTile = new Point(0, 0);
            if (left)
                turnTile = new Point(0, 1);
            else
                turnTile = new Point(2, 1);
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    if (new Point(x, y) != turnTile && new Point(1, 1) != new Point(x, y) && new Point(1, 2) != new Point(x, y))
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 2));
                    if (new Point(x, y) == turnTile)
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 4));
                    if (new Point(1, 1) == new Point(x, y))
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 5));
                    if (new Point(1, 2) == new Point(x, y))
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 3));
                }
            }
        }

        public void SimpelRoad(Vector2 pos2, bool vertical, List<Tile> tiles)
        {
            for(int i = 0; i < 3; i++)
            {
                if (vertical)
                {
                    if (i != 1)
                        tiles.Add(new Tile(pos2 + new Vector2(0, i * 16), 2));
                    else
                        tiles.Add(new Tile(pos2 + new Vector2(0, i * 16), 4));
                }
                else
                {
                    if (i != 1)
                        tiles.Add(new Tile(pos2 + new Vector2(i * 16, 0), 2));
                    else
                        tiles.Add(new Tile(pos2 + new Vector2(i * 16, 0), 3));
                }
            }
        }
    }
}
