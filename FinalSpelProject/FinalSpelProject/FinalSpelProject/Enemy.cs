using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalSpelProject
{
    class Enemy : GameObject
    {
        sbyte Type {get; set;}
        short Health { get; set; }
        short Armor { get; set; }
        float FireRate { get; set; }

        public Enemy(sbyte type2)
        {
            Type = type2;
            switch (Type)
            {
                //Examples, no real purpose but to test.
                // case 11 idicates enemyType 1 on lvl 1.
                case 11:
                    Health = 2;
                    FireRate = 30;
                    break;

                case 12:
                    Health = 4;
                    FireRate = 30;
                    break;
                //13 spawns and starts going towards the player, kamikaze style.
                case 13:
                    SetSpriteCoords(1, 1);
                    SetSize(32);
                    AnimationActive = true;
                    Health = 5;
                    Armor = 5;
                    VelX = 5;
                    VelY = 5;
                    Speed = 2.5f;
                    break;

                case 14:
                    Health = 10;
                    Armor = 10;
                    FireRate = 60;
                    break;
            }
        }

        public void Update(List<Player> player)
        {
            switch (Type)
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
