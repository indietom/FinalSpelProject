using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalSpelProject
{
    class Boss : GameObject
    {
        int hp;
        
        byte type;
        byte hurtCount;

        short[] fireRates;
        short[] maxFireRates;

        int Health;
        int Firerate;
        int AltFirerate;
        Vector2 targetLine;
        bool targeted;
        float altTempAngle;
        float tempAngle;
        bool Invulnerable;
        bool Spawned;
        bool goLeft = false;
        bool goRight = false;
        Random rng = new Random();

        short invisibleCount;
        short maxInvisibleCount;

        BossPart[] bossParts;

        public Boss(Vector2 pos2, byte type2)
        {
            Pos = pos2;
            type = type2;
            maxInvisibleCount = 8;
            OrginalColor = color;
            switch (type)
            {
                case 1:
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
                    AnimationActive = true;
                    Speed = 5;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    hp = 100;
                    Firerate = 120;
                    AltFirerate = 300;
                    Invulnerable = true;
                    Spawned = false;
                    
                    break;
            }

        }

        public void Update(List<Player> player, List<Projectile> projectiles, List<Explosion> explosions)
        {
            switch (type)
            {
                case 1:
                    if (Speed > 0 && Invulnerable == true)
                    {
                        if (Speed != 0)
                        {
                            Pos = new Vector2(Pos.X, Lerp(Pos.Y, 128, 0.02f));
                        }

                    }
                    if (Pos.Y >= 120 && Spawned == false)
                    {
                        Invulnerable = false;
                        Speed = 5;
                        Pos = new Vector2(Lerp(Pos.X, 0, 0.04f), Pos.Y);
                        if (Pos.X <= 1)
                        {
                            Spawned = true;
                        }
                    }
                         
                    AltFirerate -= 1;
                    if (AltFirerate <= 35 && AltFirerate > 0)
                    {

                        
                        if (!targeted)
                        {
                            targeted = true;
                            altTempAngle = AimAt(player[0].GetCenter);
                            Speed = 0;
                        }
                        if (targeted == true)
                        {
                            projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), altTempAngle, 15, 0, 0, true, true));
                        }
                        
                    }
                    if (AltFirerate == 0)
                    {
                        AltFirerate = 335;
                        targeted = false;
                        Speed = 5;
                    }

                    Firerate -= 1;
                    if (Firerate <= 0)
                    {
                        tempAngle = AimAt(player[0].GetCenter);
                        projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), tempAngle, 8, 0, 0, true, true));
                        if (hp > hp / 4)
                        {
                            Firerate = 120;
                        }
                        if (hp < hp / 4)
                        {
                            Firerate = 60;
                        }
                        

                    }

                    //Boss_01 Movement
                    if (Invulnerable == false)
                    {
                        if (Pos.X <= 1)
                        {
                            goLeft = false;
                            goRight = true;
                        }
                        if (goRight == true)
                        {
                            if (hp > hp / 4)
                            {
                                Pos = new Vector2(Lerp(Pos.X, Globals.screenW - Width, Speed / 200), Pos.Y);
                            }
                            if (hp < hp / 4)
                            {
                                Pos = new Vector2(Lerp(Pos.X, Globals.screenW - Width, Speed / 100), Pos.Y);
                            }
                        }

                        if (Pos.X >= Globals.screenW - Width - 1)
                        {
                            goRight = false;
                            goLeft = true;
                        }
                        if (goLeft == true)
                        {
                            Pos = new Vector2(Lerp(Pos.X, 0, Speed / 200), Pos.Y);
                        }

                       
                    }

                    break;
                    
            }
            Collision(player, projectiles, explosions);

            if (Pos.Y > Globals.screenH + Height + 10)
            {
                Destroy = true;
            }
            HurtUpdate();
        }

        public void HurtUpdate()
        {
            if(hurtCount >= 1)
            {
                hurtCount += 1;
                color = Color.Red;
            }
            if(hurtCount >= 8)
            {
                hurtCount = 0;
                color = OrginalColor;
            }
        }

        public void Collision(List<Player> player, List<Projectile> projectiles, List<Explosion> explosions)
        {
            if (!Rotated) HitBox = FullHitBox;
                else HitBox = FullHitBoxMiddle;
            foreach (Player p in player)
            {
                if (p.HitBox.Intersects(HitBox))
                {
                    hp -= 10;
                    hurtCount = 1;
                    p.Dead = true;
                }
            }
            foreach (Projectile p in projectiles)
            {
                if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false)
                {
                    if (p.Explosive)
                    {
                        explosions.Add(new Explosion(Pos, p.ExplosionSize, false));
                    }
                    hp -= p.Dm;
                    hurtCount = 1;
                    Console.WriteLine(hp);
                    if (p.GetSpriteType() != 6)
                    {
                        p.Destroy = true;
                    }
                }
            }

        }

        public void Attack(List<Projectile> projectiles)
        {
            switch (type)
            {
                case 0:

                    break;
            }
        }

        public void CheckHealth()
        {
            if(invisibleCount >= 1)
            {
                color = Color.Red;
                if(invisibleCount >= maxInvisibleCount)
                {
                    color = OrginalColor;
                    invisibleCount = 0;
                }
            }
            if(hp <= 0)
            {
                switch(type)
                {
                    case 0:
                        // TODO: boss slowlyfalls of screen 
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            DrawSprite(spriteBatch, spritesheet);
        }

        public void AssignnValues()
        {
            switch (type)
            {
                case 0:
     
                    break;
            }
        }
    }
    struct BossPart
    {
        Vector2 pos;

        byte type;
        byte hp;
        byte width;
        byte height;
        
        public byte GetHp() { return hp; }
        public byte GetType() { return type; }

        short imx;
        short imy;

        Rectangle hitBox;

        public BossPart(Vector2 pos2, byte type2, short imx2, short imy2, byte width2, byte height2, byte hp2)
        {
            pos = pos2;
            type = type2;
            width = width2;
            height = height2;
            imx = imx2;
            imy = imy2;
            hp = hp2;
            hitBox = new Rectangle();
        }

        public void Update(List<Projectile> projectiles)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            foreach(Projectile p in projectiles)
            {
                if(!p.EnemyShot && hitBox.Intersects(p.HitBox))
                {
                    if (hp - p.Dm < 0)
                        hp = 0;
                    else
                        hp -= p.Dm;
                    p.Destroy = true;
                }
            }
            switch(type)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White);
        }
    }
}
