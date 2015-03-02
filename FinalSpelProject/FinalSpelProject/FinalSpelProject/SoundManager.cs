using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace FinalSpelProject
{
    class SoundManager
    {
        public static bool SoundOff;

        public static SoundEffect
            NormalShot,
            Explosion,
            Hit,
            PowerUp;

        public static void Load(ContentManager content)
        {
            NormalShot = content.Load<SoundEffect>("shoot");
            Hit = content.Load<SoundEffect>("hit");
            Explosion = content.Load<SoundEffect>("explosion");
            PowerUp = content.Load<SoundEffect>("level-up"); 
        }
    }
}
