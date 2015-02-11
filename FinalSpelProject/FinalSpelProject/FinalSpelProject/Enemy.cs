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
        bool sideChosen;
        int rSide;
        byte explosionHurtDelay;

        byte chanceOfPowerUp;
        byte hitFlashDelay;

        bool scroll;
        bool hurtByExplosion;
        public bool OnGround { get; set; }

        public Enemy(Vector2 pos2, byte type2, Random r)
        {
            Pos = pos2;
            type = type2;
            scroll = true;
            AnimationActive = true;
            OrginalColor = color;
            switch (type)
            {
                //Follows Player.X and shoots
                case 11:
                    worth = 900;
                    SetSpriteCoords(1, Frame(4));
                    SetSize(32);
                    AnimationActive = true;
                    health = 2;
                    fireRate = 30;
                    Speed = 5;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                //Flies straight down and shoots toward players
                case 12:
                    worth = 500;
                    SetSpriteCoords(1, Frame(4));
                    SetSize(32);
                    AnimationActive = true;
                    health = 2;
                    fireRate = 30;
                    Speed = 5;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                //Kamikaze enemy
                case 13:
                    worth = 1800;
                    SetSpriteCoords(1, Frame(4));
                    SetSize(32);
                    AnimationActive = true;
                    health = 1;
                    armor = 5;
                    VelX = 5;
                    VelY = 5;
                    Speed = 2.5f;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                //Stationary Turret
                case 14:
                    worth = 1700;
                    SetSpriteCoords(1, Frame(3));
                    SetSize(32);
                    AnimationActive = true;
                    health = 2;
                    armor = 10;
                    fireRate = 50;
                    Rotated = true;
                    RoateOnRad = true;
                    OnGround = true;
                    break;
                //Sideways Dude yo
                case 15:
                    worth = 1500;
                    RoateOnRad = false;
                    Rotated = true;
                    SetSpriteCoords(1, Frame(5));
                    SetSize(32);
                    AnimationActive = true;
                    health = 1;
                    armor = 0;
                    fireRate = 80;
                    Speed = 5;
                    rSide = r.Next(0,2);
                    if (rSide == 0)
                    {
                        Pos = new Vector2(-32, Pos.Y);
                    }
                    else Pos = new Vector2(640 + 32, Pos.Y);
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                case 16:
                    worth = 500;
                    RoateOnRad = false;
                    Rotated = true;
                    SetSpriteCoords(1, Frame(5));
                    SetSize(32);
                    AnimationActive = true;
                    health = 1;
                    armor = 0;
                    fireRate = 2;
                    Speed = 5;
                    rSide = r.Next(0,2);
                    if (rSide == 0)
                    {
                        Pos = new Vector2(-10, -32);
                    }
                    else Pos = new Vector2(Game1.screenW + 10, -32);
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                case 17:
                    worth = 1800;
                    SetSpriteCoords(1, Frame(4));
                    SetSize(32);
                    AnimationActive = true;
                    health = 4;
                    armor = 5;
                    VelX = 5;
                    VelY = 5;
                    Speed = 10;
                    scroll = false;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    Pos = new Vector2(Pos.X, -32);
                    break;
            }
        }

        public void Update(List<Player> player, List<Projectile> projectile, List<Explosion> explosions, List<PowerUp> powerUps)
        {
            Random random = new Random();
            if (Pos.Y >= 480 + Height)
            {
                Destroy = true;
            }
            if(MaxFrame > 0 && AnimationActive)
            {
                Animate();
                AnimationCount += 1;
                Imx = FrameX(CurrentFrame);
            }
            explosionHurtDelay = (explosionHurtDelay >= 1) ? explosionHurtDelay = (byte)(explosionHurtDelay + 1) : explosionHurtDelay;
            explosionHurtDelay = (explosionHurtDelay >= 32) ? explosionHurtDelay = 0 : explosionHurtDelay;
            switch (type)
            {                                       
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

                    Pos += new Vector2(0, 1f);    
                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        fireRate = 50;
                        projectile.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), AimAt(player[0].GetCenter), 10, 0, 0, true, true));
                    }
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
          
                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        fireRate = 50+random.Next(100);
                        projectile.Add(new Projectile(Pos-new Vector2(3, 3), AimAt(player[0].GetCenter), 10, 0, 0, true, true));
                    }
                    break;
                case 15:
                    
                    if (rSide == 0)
                    {
                        Pos += new Vector2(Speed, -0.5f);
                        Rotation = 0;
                        if (Pos.X > 672)
                        {
                            Destroy = true;
                        }
                    }
                    else 
                    {
                        Pos += new Vector2(-Speed, -0.5f);
                        Rotation = 180;
                        if (Pos.X < -32)
                        {
                            Destroy = true;
                        }
                    }
                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        fireRate = -1;
                        projectile.Add(new Projectile(Pos, AimAt(player[0].GetCenter), 10, 0, 0, true, true));
                    }
                    break;
                case 16:
                    if (rSide == 0)
                    {
                        Pos += new Vector2(Speed, Speed);
                        Rotation = -270 - 45;
                        if (Pos.X > 672)
                        {
                            Destroy = true;
                        }
                    }
                    else
                    {
                        Pos += new Vector2(-Speed, Speed);
                        Rotation = -270 + 45;
                        if (Pos.X < -32)
                        {
                            Destroy = true;
                        }
                    }
                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        fireRate = -1;
                        projectile.Add(new Projectile(Pos-new Vector2(3, 3), AimAt(player[0].GetCenter), 3f, 0, 2, true, true));
                    }
                    break;
                    
                case 17:
                     //Pos += new Vector2(0,Speed);

                     if (Speed > 0)
                     {
                         if (health > 2)
                         {
                             
                             if (Speed != 0)
                             {
                                 Pos = new Vector2(Pos.X, Lerp(Pos.Y, 60, 0.02f));
                             }
                             
                         }
                     }
                         
                         if (health <= 2)
                         {
                             Speed = 1;
                             foreach (Player p in player)
                             {
                                 AngleMath(true);
                                 Angle = AimAt(p.GetCenter);
                                 Pos += new Vector2(VelX, VelY);
                                 if (Pos.Y < p.Pos.Y)
                                 {
                                     AngleMath(true);
                                     Angle = AimAt(p.GetCenter);
                                     Pos += new Vector2(VelX, VelY);
                                 }
                                 else
                                 {
                                     
                                     Pos += new Vector2(VelX, VelY);
                                 }

                             
                         }
                    }
                    break;
                    
            }
            if (Pos.Y < -Height)
                fireRate = 30;
            if(scroll) Pos += new Vector2(0, Game1.worldSpeed);
            if(hitFlashDelay >= 1)
            {
                hitFlashDelay += 1;
                color = Color.Red;
                if(hitFlashDelay >= 8)
                {
                    color = OrginalColor;
                    hitFlashDelay = 0;
                }
            }
            foreach (Player p in player)
            {
                if(health <= 0)
                {
                    p.Score += worth;
                    p.RaiseCurrentCombo();
                }
                if (p.Dead)
                    fireRate = 230;
            }
            if (health <= 0)
            {
                chanceOfPowerUp = (byte)random.Next(1, 4);
                if (chanceOfPowerUp == 2 && type == 14) powerUps.Add(new PowerUp(Pos, (byte)random.Next(1, 5), 1, false));
                if (!Rotated) explosions.Add(new Explosion(Pos, (byte)Width, false));
                else explosions.Add(new Explosion(new Vector2(Pos.X - Width / 2, Pos.Y - Height / 2), (byte)Width, false));
                Destroy = true;
            }
            Collision(player, projectile, explosions);
        }
        public void Collision(List<Player> player, List<Projectile> projectiles, List<Explosion> explosions)
        {
            if (!Rotated) HitBox = FullHitBox;
                  else HitBox = FullHitBoxMiddle;
            foreach(Explosion ex in explosions)
            {
                if (!ex.GetCinematic() && ex.HitBox.Intersects(HitBox) && explosionHurtDelay <= 0)
                {
                    hitFlashDelay = 1;
                    health -= 1;
                    explosionHurtDelay = 1;
                }
            }
            foreach (Player p in player)
            {
                if (p.HitBox.Intersects(HitBox) && !OnGround)
                {
                    health = 0;
                    p.Dead = true;
                }
            }
            foreach (Projectile p in projectiles)
            {
                if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
                {
                    if (p.Explosive)
                        explosions.Add(new Explosion(Pos, p.ExplosionSize, false));
                    health -= p.Dm;
                    hitFlashDelay = 1;
                    p.Destroy = true;
                }
            }
        }
    }
}
