using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Enemy : GameObject
    {
        byte type; 
        short health;
        short armor;
        short worth;
        float fireRate;

        bool scroll;

        public Enemy(Vector2 pos2, byte type2)
        {
            Pos = pos2;
            type = type2;
            scroll = true;
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
                    SetSpriteCoords(33, 1);
                    SetSize(32);
                    AnimationActive = true;
                    health = 10;
                    armor = 10;
                    fireRate = 100;
                    Rotated = true;
                    RoateOnRad = true;
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
                        fireRate = 80;
                        projectile.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), 90, 9, 0, 0, false, true));
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
                    //Rotate the sprite towards the player
                    Rotation = AimAt(player[0].GetCenter);
                    //fires toward player(s)
                    worth = 100;
                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        fireRate = 100;
                        projectile.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), AimAt(player[0].GetCenter), 10, 0, 0, true, true));
                    }
                    break;
            }
            if(scroll) Pos += new Vector2(0, Game1.worldSpeed);
            foreach (Player p in player)
            {
                if(health <= 0)
                {
                    p.Score += worth;
                }
                if (p.Dead)
                    fireRate = 30;
            }
            if (health <= 0)
            {
                Destroy = true;
            }
            Collision(player, projectile);
        }
        public void Collision(List<Player> player, List<Projectile> projectiles)
        {
            if(!Rotated) HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
                 else HitBox = new Rectangle((int)Pos.X-Width/2, (int)Pos.Y-Height/2, Width, Height);
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
                if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
                {
                    health -= p.Dm;
                    p.Destroy = true;
                }
            }
            //switch (type)
            //{
            //    case 11:
            //        HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            //        foreach (Player p in player)
            //        {
            //            if (p.HitBox.Intersects(HitBox))
            //            {
            //                Destroy = true;
            //                p.Dead = true;
            //            }
            //        }
            //        foreach (Projectile p in projectiles)
            //        {
            //            if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
            //            {
            //                health -= p.Dm;
            //                p.Destroy = true;
            //            }
            //        }
            //        break;
            //    case 12:
            //        HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            //        foreach (Player p in player)
            //        {
            //            if (p.HitBox.Intersects(HitBox))
            //            {
            //                Destroy = true;
            //                p.Dead = true;
            //            }
            //        }
            //        foreach (Projectile p in projectiles)
            //        {
            //            if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
            //            {
            //                health -= p.Dm;
            //                p.Destroy = true;
            //            }
            //        }
            //        break;
            //    case 13:
            //        HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            //        foreach (Player p in player)
            //        {
            //            if (p.HitBox.Intersects(HitBox))
            //            {
            //                Destroy = true;
            //                p.Dead = true;
            //            }
            //        }
            //        foreach (Projectile p in projectiles)
            //        {
            //            if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
            //            {
            //                health -= p.Dm;
            //                p.Destroy = true;
            //            }
            //        }
            //        break;
            //    case 14:
            //        HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            //        foreach (Projectile p in projectiles)
            //        {
            //            if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
            //            {
            //                health -= p.Dm;
            //                p.Destroy = true;
            //            }
            //        }
            //        break;
            //}
        }

    }
}
