using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class ProceduralGenerationManager:GameObject
    {
        byte roadDirection;
        byte roadSpawnCount;
        byte maxRoadSpawnCount;
        byte turnDuration;
        byte maxTurnDuration;
        byte grassSpawnCount;
        byte maxGrassSpawnCount;
        
        short lakeSpawnCount;
        short maxLakeSpawnCount;
        short changeRoadDirectionCount;
        short maxChangeRoadDirectionCount;
        short treeSpawnCount;
        short maxTreeSpawnCount;

        bool roadDirectionChange;
        bool spawningRoads;
        bool spawningLakes;
        bool canSpawnLake;
        bool verticalRoads;
        bool canSpawnTree;

        byte lakeRadius;

        Vector2 roadCoords;
        Vector2 lakeCoords;
        Vector2 treeCoords;

        public ProceduralGenerationManager()
        {
            roadCoords = new Vector2(25 * 16, -16);
            maxRoadSpawnCount = 16;
            maxGrassSpawnCount = 16;
            maxLakeSpawnCount = 128 * 3;
            maxTreeSpawnCount = 64 * 3;
            grassSpawnCount = 16;
        }

        public void Update()
        {

        }
        
        public void SpawnLevelOne(List<Tile> tiles)
        {
            Random random = new Random();

            grassSpawnCount += 1;
            if(grassSpawnCount >= maxGrassSpawnCount/Globals.worldSpeed)
            {
                for(int i = 0; i < Globals.screenW/16; i++)
                {
                    tiles.Add(new Tile(new Vector2(i * 16, -16), 1));
                }
                grassSpawnCount = 0;
            }

            if(LevelManager.currentLevel == 0) lakeSpawnCount += 1;

            if (lakeSpawnCount >= maxLakeSpawnCount)
            {
                lakeRadius = (byte)random.Next(4, 17);
                lakeCoords = new Vector2(random.Next(Globals.screenW - (16 * 2)), (16*-lakeRadius)-16*2);
                canSpawnLake = (DistanceTo(treeCoords, lakeCoords) >= lakeRadius + 16*3) ? true : false;
                if (canSpawnLake)
                {
                    SpawnLake(lakeCoords, lakeRadius, tiles);
                }
                lakeSpawnCount = 0;
                if (maxLakeSpawnCount >= 128 * 2) maxLakeSpawnCount += (byte)random.Next(-16, 128*2);
                else maxLakeSpawnCount += (byte)random.Next(128, 128*2);
            }

            //treeSpawnCount += 1;

            if (treeSpawnCount >= maxTreeSpawnCount)
            {
                treeCoords = new Vector2(random.Next(Globals.screenW - 32), random.Next(-Globals.screenH, 0));
                canSpawnTree = (DistanceTo(treeCoords, lakeCoords) >= lakeRadius + 16*3) ? true : false;
                if(canSpawnTree)
                {
                    SpawnTree(treeCoords, (byte)random.Next(1, 3), tiles);
                }
                maxTreeSpawnCount = (short)random.Next(64, 128 * 2);
                treeSpawnCount = 0;    
            }
            
            if(!verticalRoads)
            {
                roadCoords = new Vector2(Pos.X, -16);
            }
            else
            {
                roadCoords = new Vector2(Pos.X, -16*3);
            }

            if(roadSpawnCount >= maxRoadSpawnCount)
            {
                if (!roadDirectionChange)
                {
                    // add road
                    if(verticalRoads)
                    {
                        roadCoords += new Vector2(16*(-1*roadDirection), 0);
                    }
                }
                else
                {
                    // whatever direction road peice 
                    verticalRoads = true;
                    maxTurnDuration = (byte)random.Next(16 * 5, 16 * 8);
                    roadDirectionChange = false;
                }
                roadSpawnCount = 0;
            }

            if(changeRoadDirectionCount >= maxChangeRoadDirectionCount)
            {
                roadDirection = (byte)random.Next(0, 2);
                roadDirectionChange = true;
                changeRoadDirectionCount = 0;
                if(maxChangeRoadDirectionCount >= 16*5) maxChangeRoadDirectionCount += (byte)random.Next(-16, 16);
                else maxChangeRoadDirectionCount += (byte)random.Next(8, 16);
            }
        }

        // hopefully this will make the actual procedural-generation algorithm more readable 
        public void SpawnTree(Vector2 pos2, byte height, List<Tile> tiles)
        {
            for (int i = 0; i < height + 2; i++ )
            {
                if (i == 0)
                    tiles.Add(new Tile(new Vector2(pos2.X, pos2.Y), 14));
                if (i != (height + 2) - 1 && i != 0)
                    tiles.Add(new Tile(new Vector2(pos2.X, pos2.Y - i * 16), 15));
                if (i == (height + 2) - 1)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        tiles.Add(new Tile(new Vector2((pos2.X - 16) + 16 * j, (pos2.Y - i * 16)), (short)(16 + j)));
                        tiles.Add(new Tile(new Vector2((pos2.X - 8) + 8 * j, (pos2.Y - i * 16) - 6), 19));
                    }
                }
            }
        }
        public void SpawnLake(Vector2 pos2, byte radius, List<Tile> tiles)
        {
            for(int y = -radius; y < radius; y++)
            {
                for(int x = -radius; x < radius; x++)
                {
                    if(x*x+y*y <= radius*radius)
                    {
                        tiles.Add(new Tile(new Vector2(pos2.X+x*16,pos2.Y+y*16), 6));
                    }
                }
            }
            for (int i = 0; i < 360; i++)
            {
                //tiles.Add(new Tile(new Vector2(pos2.X + ((float)Math.Cos(i) * 16) * radius, pos2.Y + ((float)Math.Sin(i)) * 16 * radius), 9));
            }
        }

        public void SpawnRoadTurn(Vector2 pos2, bool left, bool up, List<Tile> tiles)
        {
            Point turnTile = new Point(0, 0);
            Point enteryTile = new Point(0, 0);

            if(up)
                enteryTile = new Point(1, 2);
            else
                enteryTile = new Point(1, 0);

            if (left)
                turnTile = new Point(0, 1);
            else
                turnTile = new Point(2, 1);

            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x < 3; x++)
                {
                    if (new Point(x, y) != turnTile && new Point(1, 1) != new Point(x, y) && enteryTile != new Point(x, y))
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 2));
                    if (new Point(x, y) == turnTile)
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 4));
                    if (new Point(1, 1) == new Point(x, y))
                        tiles.Add(new Tile(pos2 + new Vector2(x * 16, y * 16), 5));
                    if (enteryTile == new Point(x, y))
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
