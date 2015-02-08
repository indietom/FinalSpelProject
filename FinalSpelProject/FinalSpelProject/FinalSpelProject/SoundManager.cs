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
            Explosion;

        public static void PlaySound(SoundEffect soundEffect)
        {
            if (!SoundOff) soundEffect.Play(); 
        }

        public static void Load(ContentManager content)
        {

        }
    }
}
