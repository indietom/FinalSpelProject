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

        public byte Dm { get; set; }

        public bool EnemyShot { get; set; }
        bool rad;

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
        }
        public void Update()
        {
            HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);
            switch(movmentType)
            {
                case 0:
                    AngleMath(rad);
                    Pos += new Vector2(VelX, VelY);
                    break;
                case 1:
                    AngleMath(rad);
                    Pos += new Vector2(VelX, VelY);
                    Speed += 0.2f;
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
                    Dm = 1;
                    break;
            }
        }
    }
}
