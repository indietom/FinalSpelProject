using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FinalSpelProject
{
    class Explosion:GameObject
    {
        // 16 = 16x16, 128 = 128x128  
        byte size;

        short startOffset;

        bool cinematic;
        public bool enemy;
	    bool hasPlayedSound;

        public bool GetCinematic() { return cinematic; }

        public Explosion(Vector2 pos2, byte size2, bool cinematic2)
        {
            Pos = pos2;
            size = size2;
            SetSize(size);
            MaxAnimationCount = 2;
            AnimationActive = true;
            cinematic = true;
            AssignSprite();
        }
        public Explosion(Vector2 pos2, byte size2, bool enemy2, bool cinematic2, short maxAnimationCount2)
        {
            enemy = enemy2;
            Pos = pos2;
            size = size2;
            SetSize(size);
            MaxAnimationCount = maxAnimationCount2;
            AnimationActive = true;
            cinematic = cinematic2;
            AssignSprite();
        }
        public Explosion(Vector2 pos2, byte size2, bool cinematic2, bool soundOff)
        {
            Pos = pos2;
            size = size2;
            SetSize(size);
            MaxAnimationCount = 2;
            AnimationActive = true;
            cinematic = true;
            AssignSprite();
            hasPlayedSound = soundOff;
        }
        public void Update()
        {
            HitBox = (!cinematic) ? HitBox = FullHitBox : HitBox = HitBox;
            Imx = (short)(FrameX(CurrentFrame) + startOffset);
            Destroy = (CurrentFrame >= MaxFrame - 1) ? Destroy = true : Destroy = false;
            if (!hasPlayedSound)
            {
                SoundManager.Explosion.Play();
                hasPlayedSound = true;
            }
            Animate();
            AnimationCount += 1;
        }

        public void AssignSprite()
        {
            switch (size)
            {
                case 32:
                    MaxFrame = 9;
                    CurrentFrame = MinFrame;
                    SetSpriteCoords(326, 98);
                    startOffset = 326;
                    break;
                case 16:
                    SetSpriteCoords(326, 196);
                    MaxFrame = 7;
                    startOffset = 326;
                    break;
                case 64:
                    SetSpriteCoords(326, 131);
                    MaxFrame = 7;
                    startOffset = 326;
                    break;
            }
        }
    }
}
