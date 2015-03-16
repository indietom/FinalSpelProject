using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

// TODO: Add muffeld effects to not "confuse"/overwhelm the player, I think

namespace FinalSpelProject
{
    enum Material { None, OrganicAlien, Metal }
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

        Material material;

        byte direction;
        byte chanceOfPowerUp;
        byte hitFlashDelay;
        byte chanceOfUTurn;

        bool splitEnemy;
        bool scroll;
        bool hurtByExplosion;
        bool uTurnChanceGiven;
        bool carryingPowerUp;
        public bool OnGround { get; set; }

        short uTurnHeight;
        short uTurnMinHeight;
        short turningCount;
        short maxTurningCount;


        Color bloodColor = new Color(0, 0, 0);

        public Enemy(Vector2 pos2, byte type2, Random r)
        {
            Pos = pos2;
            type = type2;
            scroll = true;
            AnimationActive = true;
            OrginalColor = color;
            AssignType(r);   
        }

        public Enemy(Vector2 pos2, byte type2, Random r, bool carryingPowerUp2)
        {
            Pos = pos2;
            type = type2;
            scroll = true;
            AnimationActive = true;
            OrginalColor = color;
            carryingPowerUp = true;
            AssignType(r);
        }

        public void AssignType(Random r)
        {
            switch (type)
            {
                //Follows Player.X and shoots
                case 11:
                    material = Material.OrganicAlien;
                    worth = 900;
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
                    AnimationActive = true;
                    health = 2;
                    fireRate = 30;
                    Speed = 5;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                //Flies straight down and shoots toward players
                case 12:
                    material = Material.OrganicAlien;
                    worth = 500;
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
                    AnimationActive = true;
                    health = 2;
                    fireRate = 30;
                    Speed = 5;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                //Kamikaze enemy
                case 13:
                    material = Material.OrganicAlien;
                    worth = 1800;
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
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
                    SetSpriteCoords(1, 261);
                    SetSize(32);
                    health = 2;
                    armor = 10;
                    fireRate = 50;
                    Rotated = true;
                    RoateOnRad = true;
                    OnGround = true;
                    MaxFrame = 4;
                    MaxAnimationCount = 4;
                    material = Material.Metal;
                    break;
                //Sideways Dude yo
                case 15:
                    material = Material.OrganicAlien;
                    worth = 1500;
                    RoateOnRad = false;
                    Rotated = true;
                    SetSpriteCoords(1, Frame(7));
                    SetSize(64);
                    AnimationActive = true;
                    health = 1;
                    armor = 0;
                    fireRate = 80;
                    Speed = 5;
                    rSide = r.Next(0, 2);
                    if (rSide == 0)
                    {
                        Pos = new Vector2(-32, Pos.Y);
                    }
                    else Pos = new Vector2(640 + 32, Pos.Y);
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                case 16:
                    material = Material.OrganicAlien;
                    worth = 500;
                    RoateOnRad = false;
                    Rotated = true;
                    SetSpriteCoords(1, Frame(7));
                    SetSize(64);
                    AnimationActive = true;
                    health = 1;
                    armor = 0;
                    fireRate = 2;
                    Speed = 5;
                    rSide = r.Next(0, 2);
                    if (rSide == 0)
                    {
                        Pos = new Vector2(-10, -32);
                    }
                    else Pos = new Vector2(Globals.screenW + 10, -32);
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    break;
                case 17:
                    material = Material.OrganicAlien;
                    worth = 1800;
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
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
                case 18:
                    health = 2;
                    SetSize(32);
                    SetSpriteCoords(1, 294);
                    MaxAnimationCount = 4;
                    MinFrame = 0;
                    MaxFrame = 3;
                    AnimationActive = true;
                    worth = 500;
                    scroll = true;
                    material = Material.Metal;
                    break;
                case 21:
                    maxTurningCount = (short)r.Next(64*3, 64*5);
                    uTurnHeight = (short)r.Next(Globals.screenH / 2, Globals.screenH / 2 + 64*3);
                    uTurnMinHeight = (short)r.Next(64 * 2);
                    Speed = r.Next(3, 6);
                    Angle = -270;
                    SetSpriteCoords(1, 716);
                    Rotated = true;
                    Rotation = Angle;
                    SetSize(64);
                    MaxFrame = 4;
                    MaxAnimationCount = 4;
                    health = 3;
                    worth = 1000;
                    material = Material.Metal;
                    direction = (byte)r.Next(0, 2);
                    break;
            }
            switch(material)
            {
                case Material.OrganicAlien:
                    bloodColor = Color.Green;
                    break;
                case Material.Metal:
                    bloodColor = Color.DarkGray;
                    break;
            }
        }

        public void Update(List<Player> player, List<Projectile> projectile, List<Explosion> explosions, List<PowerUp> powerUps, List<Gib> gibs)
        {
            Random random = new Random();
           
            if (Pos.Y >= Globals.screenH + Height)
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
                        projectile.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), 90, 9, 0, 0, false, true));
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
                        projectile.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), AimAt(player[0].GetCenter), 10, 0, 0, true, true));
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
          
                    if(CurrentFrame >= MaxFrame-1)
                    {
                        AnimationActive = false;
                        CurrentFrame = 0;
                        AnimationCount = 0;
                    }

                    if(!AnimationActive)
                    {
                        Imx = 0;
                    }

                    if (fireRate != 0)
                    {
                        fireRate -= 1;
                    }
                    if (fireRate == 0)
                    {
                        AnimationActive = true;
                        fireRate = 160;
                        projectile.Add(new Projectile(Pos-new Vector2(3, 3), AimAt(player[0].GetCenter), 5, 0, 0, true, true));
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
                case 18:

                    break;
                case 21:
                    AngleMath(false);
                    Pos += Vel;
                    Rotation = Angle;
                    if (Pos.Y >= uTurnHeight && !uTurnChanceGiven)
                    {
                        chanceOfUTurn = (byte)random.Next(0, 3);
                        uTurnChanceGiven = true;
                    }
                    if(chanceOfUTurn == 2)
                    {
                        if (Angle <= -450 && Pos.Y <= uTurnMinHeight && direction == 0 || Angle >= -90 && Pos.Y <= uTurnMinHeight && direction == 1)
                        {
                            Angle = -90;
                            chanceOfUTurn = 0;
                        }

                        if (direction == 0)
                        {
                            if (Angle > -450) Angle -= 1.5f;
                        }
                        else
                        {
                            if (Angle < -90) Angle += 1.5f;
                        }
                        
                    }
                    else
                    {
                        fireRate += 1;
                        if (Angle <= -260)
                        {
                            if (fireRate == 32 || fireRate == 48 || fireRate == 48 + 16)
                                projectile.Add(new Projectile(new Vector2(Pos.X - 2, Pos.Y - 2), Angle + random.Next(-8, 9), 8, 0, 0, false, true));
                            if (fireRate >= 48 + 16 * 2)
                                fireRate = 0;
                        }

                        if (Angle > -270 && direction == 0)
                            Angle -= 1.5f;
                        if (Angle < 90 && direction == 1 && uTurnChanceGiven)
                            Angle += 1.5f;
                    }
                    break;
                    
            }
            if (Pos.Y < -Height)
                fireRate = 30;
            if(scroll) Pos += new Vector2(0, Globals.worldSpeed);
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
            foreach(Projectile p in projectile)
            {
                if(p.GetSpriteType() == 6)
                {
                    Pos = new Vector2(Lerp(Pos.X, p.Pos.X, 0.05f), Lerp(Pos.Y, p.GetCenter.Y, 0.05f));
                }
            }
            foreach (Player p in player)
            {
                if(health <= 0)
                {
                    p.RaiseScore(worth);
                    p.RaiseCurrentCombo();
                }
                if (p.Dead)
                    fireRate = 230;
            }
            if (health <= 0)
            {
                switch(material)
                {
                    case Material.OrganicAlien:
                        for (int i = 0; i < 20; i++ )
                        {
                            gibs.Add(new Gib(GetCenter + new Vector2(random.Next(-Width / 2, Width / 2), random.Next(-Height / 2, Height / 2)), (short)random.Next(5), 140, random.Next(6, 12), random.Next(360)));   
                        }
                        break;
                    case Material.Metal:
                        for (int i = 0; i < 20; i++)
                        {
                            gibs.Add(new Gib(GetCenter + new Vector2(random.Next(-Width / 2, Width / 2), random.Next(-Height / 2, Height / 2)), (short)random.Next(5), 157, random.Next(6, 12), random.Next(360)));
                        }
                        break;
                }
                if(splitEnemy && !OnGround)
                {
                    if (!Rotated)
                    {
                        projectile.Add(new Projectile(Pos, -180, 8, new Point(Imx, Imy), new Point(Width / 2, Height), 4, false, Rotated, Rotation, RoateOnRad, bloodColor));
                        projectile.Add(new Projectile(Pos + new Vector2(Width / 2, 0), 0, 8, new Point(Imx + Width / 2, Imy), new Point(Width / 2, Height), 4, false, Rotated, Rotation, RoateOnRad, bloodColor));
                    }
                    else
                    {
                        projectile.Add(new Projectile(Pos, Rotation, 8, new Point(Imx, Imy), new Point(Width / 2, Height), 4, false, Rotated, Rotation, RoateOnRad, bloodColor));
                        projectile.Add(new Projectile(Pos + new Vector2(Width / 2, 0), Rotation + 180, 8, new Point(Imx + Width / 2, Imy), new Point(Width / 2, Height), 4, false, Rotated, Rotation, RoateOnRad, bloodColor));
                    }
                    splitEnemy = false;
                }
                chanceOfPowerUp = (byte)random.Next(1, 4);
                if (chanceOfPowerUp == 2 && type == 14 || carryingPowerUp) powerUps.Add(new PowerUp(Pos, (byte)random.Next(1, 6), 1, false));
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
                    if(health <= 0 && p.GetMovmentType() == 3)
                    {
                        splitEnemy = true;
                        Console.WriteLine(splitEnemy);
                    }
                    hitFlashDelay = 1;
                   if(p.GetSpriteType() != 6) p.Destroy = true;
                }
            }
        }
    }
}
