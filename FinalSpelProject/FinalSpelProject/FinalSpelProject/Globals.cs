using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalSpelProject
{
    class Globals
    {
        public static short AmountOfEnemyTypes;

        public static float worldSpeed;

        public static int screenW;
        public static int screenH;

        public static bool blackHoleExists;

        public static bool PowerUpTextExists(List<TextEffect> textEffects)
        {
            foreach (TextEffect te in textEffects)
            {
                if (te.GetTag() == 1)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public static void Load()
        {
            screenW = 800;
            screenH = 640;

            worldSpeed = 1f;

            AmountOfEnemyTypes = 7;
        }
    }
}
