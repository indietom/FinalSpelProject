using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalSpelProject
{
    enum GameStates { StartScreen, Menu, Game, Credits, LevelTransition }
    class Globals
    {
        public static GameStates gameState;

        public static bool startedGame;

        public static short AmountOfEnemyTypes;

        public static float worldSpeed;

        public static int screenW;
        public static int screenH;

        public static bool blackHoleExists;

        public static byte LevelHeight()
        {
            byte[] height = new byte[4];

            height[0] = 14;
            height[1] = 14;
            height[2] = 14;
            height[3] = 24;

            return height[LevelManager.currentLevel];
        }

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
