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

        public Particle(Vector2 pos2, float ang, float spe, byte type2, byte spriteType2)
        {
            Pos = pos2;
            Angle = ang;
            Speed = spe;
            type = type2;
            spriteType = spriteType2;
            AssignSprite();
        }
        public Particle(Vector2 pos2, byte type2, byte spriteType2)
        {
            Pos = pos2;
            type = type2;
            spriteType = spriteType2;
            AssignSprite();
        }
        public void Update()
        {
            switch(type)
            {
                case 0:
                    AngleMath(false);
                    if (CurrentFrame >= MaxFrame-1)
                        Destroy = true;
                    Speed -= 0.4f;
                    Pos += new Vector2(VelX, VelY);
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
            }
        }
    }
}
