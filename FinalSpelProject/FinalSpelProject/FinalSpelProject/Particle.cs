using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Particle : GameObject
    {
        byte type;
        byte spriteType;
        byte lifeTime;
        byte maxLifeTime;

        bool verticalAnimation;

        Vector2 target;

        public Particle(Vector2 pos2, float ang, float spe, byte type2, byte spriteType2, Color color2)
        {
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            type = type2;
            spriteType = spriteType2;
            AssignSprite();
        }
        public Particle(Vector2 pos2, byte type2, byte spriteType2, Color color2)
        {
            color = color2;
            Pos = pos2;
            type = type2;
            spriteType = spriteType2;
            AssignSprite();
        }
        public Particle(Vector2 pos2, Vector2 target2, float spe, byte type2, byte spriteType2, Color color2)
        {
            Speed = spe;
            target = target2;
            color = color2;
            Pos = pos2;
            type = type2;
            spriteType = spriteType2;
            AssignSprite();
        }
        public void Update(List<Projectile> projectiles)
        {
            switch(type)
            {
                case 0:
                    AngleMath(false);
                    if (CurrentFrame >= MaxFrame-1)
                        Destroy = true;
                    //Speed -= 0.4f;
                    Pos += new Vector2(VelX, VelY);
                    break;
                case 1:
                    Pos = new Vector2(Lerp(Pos.X, target.X, Speed), Lerp(Pos.Y, target.Y, Speed));
                    break;
            }
            switch(spriteType)
            {
                case 1:
                    Rotation += 10;
                    foreach(Projectile p in projectiles)
                    {
                        if((p.FullHitBox.Intersects(FullHitBox) || p.Pos.Y >= Globals.screenH/2+100) && p.GetSpriteType() == 6)
                        {
                            Destroy = true;
                        }
                    }
                    break;
            }
            if (MaxFrame > 0)
            {
                if (verticalAnimation) Imy = FrameY(CurrentFrame);
                    else Imx = FrameX(CurrentFrame);
                AnimationCount += 1; 
                Animate();
            }
        }
       
        public void AssignSprite()
        {
            switch(spriteType)
            {
                case 0:
                    SetSpriteCoords(1, 131);
                    SetSize(8);
                    MaxAnimationCount = 4;
                    MaxFrame = 5;
                    verticalAnimation = false;
                    break;
                case 1:
                    Rotated = true;
                    SetSize(8);
                    SetSpriteCoords(1, 122);
                    break;
            }
        }
    }
}
