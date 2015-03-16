﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalSpelProject
{
    class Projectile : GameObject
    {
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
        public void Update(List<Particle> particles, Player player)
        {
            Random random = new Random();
            if (!Rotated)
                HitBox = FullHitBox;
            else HitBox = FullHitBoxMiddle;
            Destroy = (Pos.Y < -Height) ? true : Destroy;
            if(movmentType != 5) Destroy = (Pos.Y > Globals.screenH) ? true : Destroy;
            if(MaxFrame > 0)
            {
                Animate();
                AnimationCount += 1;
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
                    if (stopFollowingCount <= 64*2)
                    {
                        if (EnemyShot) Angle = AimAt(player.GetCenter);
                    }
                    else
                    {
                        stopFollowingCount += 1;
                    }
                    AngleMath(rad);
                    Pos += Vel;
                    Rotation = Angle;
                    if (Speed > 0.5) particles.Add(new Particle(new Vector2(Pos.X + Width / 2 - 4, Pos.Y + Width / 2 - 4), 90, 3, 0, 0, Color.DarkGray));
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
            }
            switch(spriteType)
            {
                case 3:
                    Rotation += Speed;
                    break;
                case 6:
                    bleedCount += 1;
                    if(bleedCount >= 32)
                    {
                        particles.Add(new Particle(new Vector2(random.Next(Globals.screenW), random.Next(Globals.screenH)), GetCenter, 0.08f, 1, 1, Color.LightGray));
                        bleedCount = 0;
                    }
                    lifeTime += 1;
                    if(lifeTime < 128*3) Pos = new Vector2(Lerp(Pos.X, Globals.screenW / 2 - Width / 2, 0.05f), Lerp(Pos.Y, Globals.screenH / 2 - Width / 2, 0.05f));
                    else Pos = new Vector2(Lerp(Pos.X, Globals.screenW / 2 - Width / 2, 0.05f), Lerp(Pos.Y, 800, 0.005f));
                    break;
                case 7:
                    if (CurrentFrame == MaxFrame-1)
                        Destroy = true;
                    break;
            }
            if(Destroy && spriteType == 6)
            {
                Globals.blackHoleExists = true;
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
                    SetSize(9, 16);
                    SetSpriteCoords(326, 1);
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
                    SetSpriteCoords(196, 1);
                    SetSize(64);
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
