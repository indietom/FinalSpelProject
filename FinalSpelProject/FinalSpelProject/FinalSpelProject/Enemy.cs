using System;
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
                    SetSpriteCoords(1, 1);
                    SetSize(32);
                    AnimationActive = true;
                    health = 2;
                    fireRate = 30;
                    Speed = 1;
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

        public void Update(List<Player> player, List<Projectile> projectile)
        {
            switch (type)
            {
                    //Examples, no real purpose but to test.
                    // case 11 idicates enemyType 1 on lvl 1.
                case 11:
                    foreach (Player p in player)
                    {
                        AngleMath(true);
                        Angle = AimAt(p.Pos);
                        Pos += new Vector2(VelX, 0.1f);
                    }
                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        fireRate = 30;
                        projectile.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), -90, 9, 0, 1, true));
                    }
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
        public void Collision(List<Player> player, List<Enemy> enemy, List<Projectile> projectiles)
        {
            HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            foreach (Player p in player)
            {
                if (p.HitBox.Intersects(HitBox)) 
                {
                    Destroy = true;
                    p.Dead = true;
                }
            }
            foreach (Projectile p in projectiles)
            {
                if (p.HitBox.Intersects(HitBox) && p.enemyShot == false)
                {
                    p.Destroy = true;
                    Destroy = true;
                }
            }
        }

    }
}
