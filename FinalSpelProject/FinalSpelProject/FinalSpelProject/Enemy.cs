﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Enemy : GameObject
    {
        sbyte type; 
        short health;
        short armor; 
        float fireRate;

        public Enemy(Vector2 pos2, sbyte type2)
        {
            Pos = pos2;
            type = type2;
            switch (type)
            {
                //Examples, no real purpose but to test.
                // case 11 idicates enemyType 1 on lvl 1.
                case 11:
                    health = 2;
                    fireRate = 30;
                    break;

                case 12:
                    health = 4;
                    fireRate = 30;
                    break;
                //13 spawns and starts going towards the player, kamikaze style.
                case 13:
                    SetSpriteCoords(1, 1);
                    SetSize(32);
                    AnimationActive = true;
                    health = 5;
                    armor = 5;
                    VelX = 5;
                    VelY = 5;
                    Speed = 2.5f;
                    break;

                case 14:
                    health = 10;
                    armor = 10;
                    fireRate = 60;
                    break;
            }
        }

        public void Update(List<Player> player)
        {
            switch (type)
            {
                    //Examples, no real purpose but to test.
                    // case 11 idicates enemyType 1 on lvl 1.
                case 11:

                    break;
                case 12:

                    break;
                case 13:
                    foreach (Player p in player)
                    {
                        AngleMath(true);
                        Angle = AimAt(p.Pos);
                        Pos += new Vector2(VelX, VelY);
                    }
                   
                    break;
                case 14:

                    break;
            }
        }

    }
}