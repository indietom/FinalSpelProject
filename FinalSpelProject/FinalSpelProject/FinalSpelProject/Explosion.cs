using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Explosion:GameObject
    {
        // 8 = 8x8, 128 = 128x128  
        byte size;

        public Explosion(Vector2 pos2, byte size2)
        {
            Pos = pos2;
            size = size2;
            SetSize(size);
            MinFrame = 4;
            MaxFrame = 13;
            CurrentFrame = MinFrame;
            SetSpriteCoords(FrameX(CurrentFrame), AssignSprite());
            MaxAnimationCount = 2;
            AnimationActive = true;
        }
        public void Update()
        {
            Imx = FrameX(CurrentFrame);
            Destroy = (CurrentFrame >= MaxFrame-1) ? Destroy = true : Destroy = false;
            Animate();
            AnimationCount += 1;
        }
        public short AssignSprite()
        {
            switch(size)
            {
                case 32:
                    return FrameY(1);
                    break;
            }
            return 0;
        }
    }
}
