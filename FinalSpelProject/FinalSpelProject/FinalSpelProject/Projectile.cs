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
        short lifeTime;
        short maxLifeTime;

        byte spriteType;
        byte movmentType;
        public byte ExplosionSize { get; set; }

        public byte Dm { get; set; }

        public bool EnemyShot { get; set; }
        bool rad;
        public bool Explosive { get; set; }

        float wantedAngle;
        float rotateSpeed;

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
        public void Update(List<Particle> particles)
        {
            HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
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
                    if(Speed > 0.5) particles.Add(new Particle(new Vector2(Pos.X + Width / 2 - 4, Pos.Y + Width / 2 - 4), 90, 3, 0, 0));
                    break;
                case 2:
                    AngleMath(rad);
                    Pos += Vel;
                    //Speed += 0.2f;
                    if (Speed > 0.5) particles.Add(new Particle(new Vector2(Pos.X + Width / 2 - 4, Pos.Y + Width / 2 - 4), 90, 3, 0, 0));
                    break;
            }
        }
        public void AssignMovmentValues()
        {
            switch(movmentType)
            {
                case 1:
                    Explosive = true;
                    ExplosionSize = 32;
                    Dm = 3;
                    break;
            }
        }
        public void AssignEnemySprite()
        {
            switch (spriteType)
            {
                case 0:
                    SetSize(7, 11);
                    SetSpriteCoords(133, 1);
                    break;
            }
        }
        public void AssignSprite()
        {
            switch(spriteType)
            {
                case 0:
                    SetSize(7, 11);
                    SetSpriteCoords(133, 1);
                    
                    break;
            }
        }
    }
}
