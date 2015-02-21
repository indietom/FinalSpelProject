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
        
        short lakeSpawnCount;
        short maxLakeSpawnCount;
        short changeRoadDirectionCount;
        short maxChangeRoadDirectionCount;

        bool roadDirectionChange;
        bool spawningRoads;
        bool spawningLakes;
        bool canSpawnLake;
        bool verticalRoads;

        byte lakeRadius;

        Vector2 roadCoords;
        Vector2 lakeCoords;

        public ProceduralGenerationManager()
        {
            roadCoords = new Vector2(25 * 16, -16);
        }

        public void Update()
        {

        }
        
        public void SpawnLevelOne()
        {
            Random random = new Random();
            
            if(lakeSpawnCount >= maxLakeSpawnCount)
            {
                lakeRadius = (byte)random.Next(8, 16);
                lakeCoords = new Vector2(random.Next(Globals.screenW - (16 * 2), Globals.screenH - (16 * 2)));
                canSpawnLake = (DistanceTo(roadCoords, lakeCoords) >= lakeRadius + 16 * 3) ? true : false;
                if (canSpawnLake)
                {
                    // add lake
                }
                lakeSpawnCount = 0;
                if(maxLakeSpawnCount >= 128*2) maxLakeSpawnCount += (byte)random.Next(-16, 128);
                else maxLakeSpawnCount += (byte)random.Next(32, 128);
            }

            if(roadSpawnCount >= maxRoadSpawnCount)
            {
                if (!roadDirectionChange)
                {
                    // add road
                }
                else
                {
                    // whatever direction road peice 
                    maxTurnDuration = (byte)random.Next(16 * 5, 16 * 8);
                    roadDirectionChange = false;
                }
                roadSpawnCount = 0;
            }

            if(changeRoadDirectionCount >= maxChangeRoadDirectionCount)
            {
                roadDirection = (byte)random.Next(1, 3);
                roadDirectionChange = true;
                changeRoadDirectionCount = 0;
                if(maxChangeRoadDirectionCount >= 16*5) maxChangeRoadDirectionCount += (byte)random.Next(-16, 16);
                else maxChangeRoadDirectionCount += (byte)random.Next(8, 16);
            }
        }

        // hopefully this will make the actual procedural-generation algorithm more readable 
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
            for(int i = 0; i < 360; i++)
            {
                tiles.Add(new Tile(new Vector2((float)Math.Cos(pos2.X), (float)Math.Sin(pos2.Y)), 9));
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
