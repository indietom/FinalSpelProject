using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Projectile : GameObject
    {
        short lifeTime;
        short maxLifeTime;

        byte spriteType;
        byte movmentType;

        bool enemyShot;

        public Projectile(Vector2 pos2, float ang, float spe, byte spriteType2, byte movmentType2)
        {
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            movmentType = movmentType2;
            spriteType = spriteType2;
            AssignSprite();
        }
        public Projectile(Vector2 pos2, float ang, float spe, byte spriteType2, byte movmentType2, bool enemyShot)
        {
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            movmentType = movmentType2;
            spriteType = spriteType2;
            AssignEnemySprite();
            enemyShot = true;
        }
        public void Update()
        {
            switch(movmentType)
            {
                case 0:
                    AngleMath(false);
                    Pos += new Vector2(VelX, VelY);
                    break;
            }
        }
        public void AssignEnemySprite()
        {

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
