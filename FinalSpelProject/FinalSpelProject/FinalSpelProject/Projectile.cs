using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalSpelProject
{
    class Projectile : GameObject
    {
        Vector2 target;

        short lifeTime;
        short maxLifeTime;
        short animationOffset;

        byte spriteType;
        byte movmentType;
        byte stopFollowingCount;
        byte bleedCount;
        public byte ExplosionSize { get; set; }

        public byte GetMovmentType() { return movmentType; }
        public byte GetSpriteType() { return spriteType; }

        public byte Dm { get; set; }

        public bool EnemyShot { get; set; }
        bool rad;
        public bool Explosive { get; set; }

        float wantedAngle;
        float rotateSpeed;

        Color bloodColor;

        public Projectile(Vector2 pos2, float ang, float spe, byte spriteType2, byte movmentType2, bool rad2)
        {
            rad = rad2;
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            movmentType = movmentType2;
            spriteType = spriteType2;
            AssignSprite();
            AssignMovmentValues();
        }
        public Projectile(Vector2 pos2, float ang, float spe, byte spriteType2, byte movmentType2, bool rad2, short maxLifeTime2)
        {
            maxLifeTime = maxLifeTime2;
            rad = rad2;
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            movmentType = movmentType2;
            spriteType = spriteType2;
            AssignSprite();
            AssignMovmentValues();
        }
        public Projectile(Vector2 pos2, float ang, float spe, byte spriteType2, byte movmentType2, bool rad2, bool enemyShot2)
        {
            rad = rad2;
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            movmentType = movmentType2;
            spriteType = spriteType2;
            AssignEnemySprite();
            EnemyShot = true;
            AssignMovmentValues();
        }
        // You can't have to many constructers amrite :^)))))
        public Projectile(Vector2 pos2, Vector2 target2, float spe, byte spriteType2, byte movmentType2, bool rad2, bool enemyShot2)
        {
            rad = rad2;
            Pos = pos2;
            target = target2;
            Speed = spe;
            movmentType = movmentType2;
            spriteType = spriteType2;
            AssignEnemySprite();
            EnemyShot = true;
            AssignMovmentValues();
        }
        // :^(
        public Projectile(Vector2 pos2, float ang, float spe, Point spriteCoords, Point size, byte movmentType2, bool rad2, bool rotated, float rotation, bool rotateOnRad, Color bloodColor2)
        {
            bloodColor = bloodColor2;
            Rotated = rotated;
            Rotation = rotation;
            RoateOnRad = rotateOnRad;
            rad = rad2;
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            movmentType = movmentType2;
            SetSpriteCoords((short)spriteCoords.X, (short)spriteCoords.Y);
            SetSize((short)size.X, (short)size.Y);
            AssignMovmentValues();
        }
        public void Update(List<Particle> particles, List<Explosion> explosions, Player player)
        {
            Random random = new Random();
            if (!Rotated)
                HitBox = FullHitBox;
            else HitBox = FullHitBoxMiddle;
            Destroy = (Pos.Y < -Height) ? true : Destroy;
            Destroy = (Pos.Y > Globals.screenH) ? true : Destroy;
            if(movmentType != 5) Destroy = (Pos.Y > Globals.screenH) ? true : Destroy;
            if(MaxFrame > 0)
            {
                Animate();
                AnimationCount += 1;
                if (spriteType == 6 && CurrentFrame == MaxFrame-1)
                    AnimationCount = 0;
                Imx = (short)(animationOffset+FrameX(CurrentFrame));
            }
            if(maxLifeTime != 0)
            {
                lifeTime += 1;
                if (lifeTime >= maxLifeTime)
                    Destroy = true;
            }
            switch(movmentType)
            {
                case 0:
                    AngleMath(rad);
                    Pos += Vel;
                    break;
                case 1:
                    AngleMath(rad);
                    Pos += Vel;
                    Speed += 0.2f;
                    if (Speed > 0.5) particles.Add(new Particle(new Vector2(Pos.X + Width / 2 - 4, Pos.Y + Width / 2 - 4), 90, 3, 0, 0, Color.DarkGray));
                    break;
                case 2:
                    if (stopFollowingCount <= 48)
                    {
                        if (EnemyShot) Angle = AimAt(player.GetCenter);
                    }
                    if(stopFollowingCount <= 100)
                    stopFollowingCount += 1;
                    AngleMath(rad);
                    Pos += Vel;
                    Rotation = Angle * 180 / (float)Math.PI;
                    if (Speed > 1) Speed -= 0.02f;
                    break;
                case 3:
                    AngleMath(rad);
                    Pos += Vel;
                    if (Speed > 0.5f) Speed -= 0.04f;
                    break;
                case 4:
                    AngleMath(rad);
                    Pos += Vel;
                    if (Speed > 0.5f) Speed -= 0.04f;
                    Pos += new Vector2(0, Speed/10);
                    bleedCount += 1;
                    if(bleedCount >= 4)
                    {
                        particles.Add(new Particle(Pos + new Vector2(random.Next(Width / 2), random.Next(Height)), 0, 0, bloodColor));
                        bleedCount = 0;
                    }
                    break;
                case 5:
                    Pos -= new Vector2(0, 7);
                    break;
                case 6:
                    // Because the game needs more lerping :^))))
                    Pos = new Vector2(Lerp(Pos.X, target.X, Speed), Lerp(Pos.Y, target.Y, Speed));
                    if(DistanceTo(target) <= 8)
                    {
                        explosions.Add(new Explosion(Pos + new Vector2(-32, -32), 64, true, false, 2));
                        Destroy = true;
                    }
                    break;
            }
            switch(spriteType)
            {
                case 3:
                    if (!EnemyShot)
                        Rotation += Speed;
                    else
                    {
                        Rotation = Angle * 180 / (float)Math.PI;
                    }
                    break;
                case 6:
                    Rotation += 10f;
                    bleedCount += 1;
                    if(bleedCount >= 32)
                    {
                        particles.Add(new Particle(new Vector2(random.Next(Globals.screenW), random.Next(Globals.screenH)), GetCenter, 0.08f, 1, 1, Color.LightGray));
                        bleedCount = 0;
                    }
                    lifeTime += 1;
                    if(lifeTime < 128*3) Pos = new Vector2(Lerp(Pos.X, Globals.screenW / 2 - Width / 2, 0.05f), Lerp(Pos.Y, Globals.screenH / 2 - Width / 2, 0.05f));
                    else Pos = new Vector2(Lerp(Pos.X, Globals.screenW / 2 - Width / 2, 0.05f), Lerp(Pos.Y, 800, 0.05f));
                    break;
                case 7:
                    if (CurrentFrame == MaxFrame-1)
                        Destroy = true;
                    break;
                case 10:
                    if (CurrentFrame < (MaxFrame / 3)-1 || CurrentFrame >= 6)
                    {
                        HitBox = new Rectangle(-1000, -1000, 0, 0);
                    }
                    if (CurrentFrame == MaxFrame - 1)
                        Destroy = true;
                    break;
            }
            if(Destroy && spriteType == 6)
            {
                Globals.blackHoleExists = false;
            }
        }
        public void AssignMovmentValues()
        {
            switch(movmentType)
            {
                case 0:
                    Dm = 1;
                    break;
                case 1:
                    Explosive = true;
                    ExplosionSize = 32;
                    Dm = 3;
                    break;
                case 2:
                    Explosive = true;
                    ExplosionSize = 32;
                    Dm = 3;
                    Rotated = true;
                    break;
                case 3:
                    Dm = 2;
                    break;
                case 4:
                    Dm = 1;
                    break;
                case 5:
                    Dm = 3;
                    break;
            }
        }
        public void AssignEnemySprite()
        {
            switch (spriteType)
            {
                case 0:
                    if (!Rotated)
                    {
                        SetSize(9, 16);
                        SetSpriteCoords(340, 1);
                    }
                    else
                    {
                        SetSize(16, 8);
                        SetSpriteCoords(326, 18);
                    }
                    break;
                case 1:
                    SetSpriteCoords(359, 16);
                    SetSize(8, 14);
                    break;
                case 2:
                    SetSpriteCoords(383, 1);
                    SetSize(8, 8);
                    break;
                case 3:
                    SetSize(16, 8);
                    SetSpriteCoords(326, 18);
                    Rotated = true;
                    break;
                case 4:
                    SetSize(16, 2);
                    SetSpriteCoords(392, 4);
                    break;
                case 5:
                    MaxFrame = 12;
                    MaxAnimationCount = 2;
                    SetSpriteCoords(439, 17);
                    SetSize(16);
                    animationOffset = (short)(Imx - 1);
                    break;
                case 8:
                    SetSize(14, 8);
                    SetSpriteCoords(377, 25);
                    break;
                case 9:
                    SetSize(4, 8);
                    SetSpriteCoords(420, 1);
                    break;
                case 10:
                    MaxFrame = 10;
                    SetSize(24);
                    SetSpriteCoords(326, 326);
                    MaxAnimationCount = 4;
                    animationOffset = (short)(Imx - 1);
                    break;
            }
        }
        public void AssignSprite()
        {
            Random random = new Random();
            switch(spriteType)
            {
                case 0:
                    SetSize(9, 16);
                    SetSpriteCoords(326, 1);
                    break;
                case 1:
                    SetSize(8, 14);
                    SetSpriteCoords(359, 1);
                    break;
                case 2:
                    SetSize(16, 2);
                    SetSpriteCoords(392, 1);
                    break;
                case 3:
                    SetSize(16);
                    SetSpriteCoords(261, 1);
                    Rotated = true;
                    break;
                case 4:
                    MaxFrame = 6;
                    MaxAnimationCount = 8;
                    SetSize(32);
                    animationOffset = 326;
                    SetSpriteCoords((short)(326+FrameX(CurrentFrame)), 34);
                    break;
                case 5:
                    SetSpriteCoords(425, 1);
                    SetSize(8, 13);
                    color = new Color(random.Next(100, 256), random.Next(100, 256), random.Next(100, 256));
                    break;
                case 6:
                    SetSpriteCoords(1, 976);
                    SetSize(64);
                    MaxFrame = 8;
                    MaxAnimationCount = 4;
                    Rotated = true;
                    Globals.blackHoleExists = true;
                    break;
                case 7:
                    CurrentFrame = 5;
                    MaxFrame = 17;
                    MaxAnimationCount = 4;
                    SetSpriteCoords(Frame(5), 261);
                    SetSize(64);
                    break;
            }
        }
    }
}
