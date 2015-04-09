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

        int[] eyesHp = new int[3];

        public int GetHp() { return hp; }

        byte type;
        byte hurtCount;
        byte currentEye;

        short[] fireRates;
        short[] maxFireRates;
        short startOffset;
        short changeTargetCount;
        short eyeTransation;
        short explosionCount;

        Vector2 barrelPos;
        Vector2 target;

        // Why is this captilized? 
        int Health;
        int Firerate;
        int AltFirerate;
        Vector2 targetLine;
        bool targeted;
        float altTempAngle;
        float tempAngle;
        float orginalSpeed;
        bool Invulnerable;
        bool Spawned;
        bool goLeft = false;
        bool goRight = false;
        public bool levelCompleted;
        Random rng = new Random();

        // What are arrays?
        //dont judge me. (for boss 4)
        Rectangle[] eyes = new Rectangle[3];

        int Boss4Phase;

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
                    SetSpriteCoords(846, 651);
                    SetSize(154, 214); // ayy lmao
                    AnimationActive = true;
                    Speed = 5;
                    MaxFrame = 6;
                    MaxAnimationCount = 8;
                    hp = 100;
                    Firerate = 120;
                    AltFirerate = 300;
                    Invulnerable = true;
                    Spawned = false;
                    startOffset = 846;
                    break;
                case 3:
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
                    AnimationActive = true;
                    Speed = 0.5f;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    hp = 100;
                    Invulnerable = true;
                    Spawned = false;
                    goLeft = true;
                    Firerate = 300;
                    AltFirerate = 150;
                    break;
                case 2:
                    //What the hell am I supposed to do here???
                    // Pass it on to Tom apperently :^))))
                    SetSpriteCoords(459, 522);
                    SetSize(128);
                    startOffset = (short)(Imx - 1);
                    MaxFrame = 8;
                    MaxAnimationCount = 16;
                    hp = 50;
                    Speed = 0.03f;
                    barrelPos = new Vector2(0, 0);
                    break;
                case 4:
                    //Så tre rektanglar behövs och de ska bara "aktiveras" när bossen är i rätt fas.
                    SetSpriteCoords(716, 911);
                    SetSize(264, 368);
                    AnimationActive = true;
                    Speed = 5;
                    MaxFrame = 0;
                    MaxAnimationCount = 0;
                    hp = 200;
                    Firerate = 100;
                    AltFirerate = 300;
                    Invulnerable = true;
                    Spawned = false;
                    Boss4Phase = 1;
                    //Rekt
                    // This is a sin and should be treated as such
                    for (int i = 0; i < 3; i++)
                    {
                        eyes[i] = new Rectangle(0, 0, 0, 0);
                    }
                    eyesHp[0] = 20;
                    eyesHp[1] = 40;
                    eyesHp[2] = 80;
                    break;
            }
            orginalSpeed = Speed;
        }

        public void Update(List<Player> player, List<Projectile> projectiles, List<Explosion> explosions, List<Enemy> enemies)
        {
            if (MaxFrame > 0)
            {
                AnimationCount += 1;
                if(Width != 64)
                    Imx = (short)(FrameX(CurrentFrame) + startOffset);
                Animate();
            }
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
                    if (Pos.Y >= 120 && Spawned == false && hp > 0)
                    {
                        Speed = 5;
                        Pos = new Vector2(Lerp(Pos.X, 0, Speed / 200), Pos.Y);
                        if (Pos.X <= 1)
                        {
                            Spawned = true;
                            Invulnerable = false;
                        }
                    }
                       
                    //Boss Shooting
                    if (hp > 0)
                    {
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
                    }

                    //Boss_01 Movement
                    if (Invulnerable == false && hp > 0)
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
                            if (hp > hp / 4)
                            {
                                Pos = new Vector2(Lerp(Pos.X, 0, Speed / 200), Pos.Y);
                            }
                            if (hp < hp / 4)
                            {
                                Pos = new Vector2(Lerp(Pos.X, 0, Speed / 100), Pos.Y);
                            }
                        }
                    }
                    if (hp < 0)
                    {
                        Speed = 0;
                        Pos += new Vector2(0.05f, Globals.worldSpeed + 1);
                    }

                    break;

                case 3:
                    //Boss2 movement
                    if (Spawned == false)
                    {
                        Pos = new Vector2(Pos.X, Lerp(Pos.Y, 160, 0.005f));
                        if (Pos.Y >= 150)
                        {
                            Spawned = true;
                        }
                    }
                    if (goRight == true)
                    {
                        Pos += new Vector2(Speed, 0);
                    }
                    if (goLeft == true)
                    {
                        Pos += new Vector2(-Speed, 0);
                    }
                    if (Pos.X <= 10 && hp > 0)
                    {
                        goLeft = false;
                        goRight = true;
                    }
                    if (Pos.X >= Globals.screenW - Width - 10 && hp > 0)
                    {
                        goRight = false;
                        goLeft = true;
                    }
                    if (hp <= 0)
                    {
                        Pos += new Vector2(0, Globals.worldSpeed + 2);
                    }
                    //Boss2 attack sequence.
                    Firerate -= 1;
                    if (Firerate <= 0 && hp > 0)
                    {
                        for (int i = 0; i < 360; i += 20)
                        {
                            projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), i , 4, 3, 0, false, true));
                        }
                        Firerate = 300; 
                    }
                    //Boss2 minion spawn.
                    AltFirerate -= 1;
                    if (AltFirerate <= 0 && hp > 0)
                    {
                        enemies.Add(new Enemy(Pos, 28, rng, false));
                        AltFirerate = 150;
                    }
                    break;
                case 2:
                    if (CurrentFrame == 0 || CurrentFrame == 4)
                    {
                        barrelPos = new Vector2(Pos.X + 62, Pos.Y + 105);
                        Angle = -270;
                    }
                    if (CurrentFrame == 1 || CurrentFrame == 3)
                    {
                        barrelPos = new Vector2(Pos.X + 73, Pos.Y + 105);
                        Angle = -290;
                    }
                    if(CurrentFrame == 2)
                    {
                        barrelPos = new Vector2(Pos.X + 89, Pos.Y + 100);
                        Angle = -320;
                    }
                    if(CurrentFrame == 5 || CurrentFrame == 7)
                    {
                        barrelPos = new Vector2(Pos.X + 51, Pos.Y + 103);
                        Angle = -240;
                    }
                    if(CurrentFrame == 6)
                    {
                        barrelPos = new Vector2(Pos.X + 40, Pos.Y + 100);
                        Angle = -225;
                    }

                    Firerate += 1;

                    if (Firerate >= 128 * 2) Firerate = 0;

                    if(AnimationCount >= 10 && Firerate >= 128)
                    {
                        projectiles.Add(new Projectile(barrelPos + new Vector2(-4, -4), Angle, 8, 2, 0, false, true));
                    }

                    Pos = new Vector2(Lerp(Pos.X, target.X, Speed), Lerp(Pos.Y, target.Y, Speed));

                    if (hp <= 16)
                    {
                        if(Firerate == 64 && rng.Next(7) == 3)
                        {
                            projectiles.Add(new Projectile(GetCenter, 0, 5, 8, 2, true, true));
                        }
                    }

                    changeTargetCount += 1;

                    if(changeTargetCount >= 128 * 2 + 64 && hp > 0)
                    {
                        target = new Vector2(rng.Next(Globals.screenW - Width), rng.Next(32, 128 * 2));
                        changeTargetCount = 0;
                    }

                    if(hp <= 0)
                    {
                        Firerate = 0;
                        AnimationCount = 0;
                        if(!Spawned)
                        {
                            target = new Vector2(rng.Next(Globals.screenW), Globals.screenH + rng.Next(100));
                            Spawned = true;
                        }
                    }
                    break;
                case 4:
                    if(hp >= 0) hp = eyesHp[0]+eyesHp[1]+eyesHp[2];
                    else hp = 0;

                    if(target == Vector2.Zero)
                    {
                        target = new Vector2(400-Width/2, -100);
                    }

                    if(eyesHp[0] <= 0 && eyesHp[1] > 0 && eyesHp[2] > 0)
                    {
                        currentEye = 1;
                    }

                    if (eyesHp[0] <= 0 && eyesHp[1] <= 0 && eyesHp[2] > 0)
                    {
                        currentEye = 2;
                    }

                    if(currentEye == 0)
                    {
                        Firerate += 1;
                        if (Firerate >= 64)
                        {
                            projectiles.Add(new Projectile(GetCenter - new Vector2(3, 3), AimAt(player[0].GetCenter), 5, 3, 0, true, true));
                            Firerate = 0;
                        }
                    }

                    if(currentEye == 1)
                    {
                        Firerate += 1;
                        if (Firerate == 64 || Firerate == 64 + 16 || Firerate == 64 + 32 || Firerate == 64 + 48)
                        {
                            projectiles.Add(new Projectile(GetCenter - new Vector2(3, 3), AimAt(player[0].GetCenter)+rng.Next(-9, 10)/15, 5, 3, 0, true, true));
                        }
                        if (Firerate >= 128 + 32) Firerate = 0;
                    }

                    if(currentEye == 2)
                    {
                        changeTargetCount += 1;
                        if(changeTargetCount >= 64)
                        {
                            target = new Vector2(rng.Next(Globals.screenW-Width), Pos.Y);
                            changeTargetCount = 0;
                        }
                    }

                    Pos = new Vector2(Lerp(Pos.X, target.X, Speed/100), Lerp(Pos.Y, target.Y, Speed/100));
                    if(eyesHp[0] <= 0 && eyesHp[1] <= 0 && eyesHp[2] <= 0)
                    {
                        explosionCount += 1;
                        if(explosionCount % 8 == 0) explosions.Add(new Explosion(GetCenter + new Vector2(rng.Next(-164, 165), rng.Next(-268, 268)),32, false));
                        if (!Spawned)
                        {
                            target = new Vector2(rng.Next(Globals.screenW), Globals.screenH + Height * 2);
                            Spawned = true;
                        }
                        Speed = 0;
                        
                    }

                    eyes[0] = new Rectangle((int)Pos.X + 129, (int)Pos.Y + 285, 50, 44);
                    eyes[1] = new Rectangle((int)Pos.X + 173, (int)Pos.Y + 229, 33, 31);
                    eyes[2] = new Rectangle((int)Pos.X + 68, (int)Pos.Y + 115, 130, 71);

                    if(eyeTransation >= 1)
                    {
                        eyeTransation += 1;
                        Speed = 0;
                        explosionCount += 1;

                        if(explosionCount % 4 == 0)
                        {
                            explosions.Add(new Explosion(new Vector2(eyes[currentEye - 1].X + rng.Next(-48, 48), eyes[currentEye - 1].Y + rng.Next(-48, 48)), 32, false));
                        }

                        if(eyeTransation >= 32)
                        {
                            Speed = orginalSpeed;
                            eyeTransation = 0;
                        }
                    }

                    foreach (Projectile p in projectiles)
                    {
                        if (p.HitBox.Intersects(eyes[currentEye]) && p.EnemyShot == false && eyeTransation <= 0 && hp > 0)
                        {
                            hurtCount = 1;

                            if (eyesHp[currentEye] - p.Dm <= 0)
                            {
                                eyeTransation = 1;
                            }
                            eyesHp[currentEye] -= p.Dm;

                            if (p.GetSpriteType() != 6)
                            {
                                p.Destroy = true;
                            }
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
            CheckHealth();
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
                    if (Invulnerable == false)
                    {
                        //hp -= 10;
                        hurtCount = 1;
                        p.Dead = true;
                    }
                }
            }

            foreach (Projectile p in projectiles)
            {
                if (p.HitBox.Intersects(HitBox) && p.EnemyShot == false && type != 4)
                {
                    if (p.Explosive)
                    {
                        explosions.Add(new Explosion(Pos, p.ExplosionSize, false));
                    }
                    if (invisibleCount <= 0 && hurtCount <= 0)
                    {
                        if (p.GetSpriteType() == 2)
                            hp -= p.Dm * 4;
                        else
                            hp -= p.Dm;
                    }
                    hurtCount = 1;
                    
                    if (p.GetSpriteType() != 6)
                    {
                        p.Destroy = true;
                    }
                    if (p.GetSpriteType() == 2)
                    {
                        player[0].SetMaxLazerHeight(10);
                        player[0].SetCurrentLazerHeight(0);
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
                if(Pos.Y >= Globals.screenH) levelCompleted = true;
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
