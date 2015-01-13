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

        public Particle()
        {

        }
        public void Update()
        {
            switch(type)
            {

            }
            if (MaxFrame <= 0)
                Animate();
        }
       
        public void AssignSprite()
        {
            switch(spriteType)
            {
                case 0:
                    SetSpriteCoords(1, 67);
                    SetSize(8);
                    MaxAnimationCount = 4;
                    MaxFrame = 4;
                    break;
            }
        }
    }
}
