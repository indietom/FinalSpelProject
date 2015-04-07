using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace FinalSpelProject
{
    enum GameStates { StartScreen, Menu, Game, Credits, LevelTransition }
    class Globals
    {
        public static GameStates gameState = GameStates.Menu;

        public static bool startedGame;

        public static short AmountOfEnemyTypes;

        public static float worldSpeed;

        public static int screenW;
        public static int screenH;

        public static bool blackHoleExists;

        public static bool startedNewLevel;

        public static Keys left, right, down, up, fire, specialFire;

        public static Keys[] WASDSet = new Keys[5];
        public static Keys[] ArrowSet = new Keys[5];
        public static Keys[] XZSet = new Keys[2];
        public static Keys[] NMSet = new Keys[2];

        public static byte currentMoveSet = 0;
        public static byte currentShootSet = 0;

        public static Keys[] MoveSet()
        {
            Keys[] tmp = new Keys[5];

            if (currentMoveSet == 0)
                tmp = ArrowSet;
            if (currentMoveSet == 1)
                tmp = WASDSet;

            return tmp;
        }

        public static Keys[] ShootSet()
        {
            Keys[] tmp = new Keys[2];

            if (currentShootSet == 0)
                tmp = XZSet;
            if (currentShootSet == 1)
                tmp = NMSet;

            return tmp;
        }

        public static string OptionTextMovment()
        {
            if (currentMoveSet == 0)
                return "<ARROW KEYS> - W, A, S, D";
            else
                return "ARROW KEYS - <W, A, S, D>";
        }

        public static string OptionTextShoot()
        {
            if (currentShootSet == 0)
                return "<X TO SHOOT, Z FOR SPECIAL-ATTACK> - N TO SHOOT, M FOR SPECIAL-ATTACK";
            else
                return "X TO SHOOT, Z FOR SPECIAL-ATTACK - <N TO SHOOT, M FOR SPECIAL-ATTACK>";
        }

        public static string GetLevelName(byte number)
        {
            string[] name = new string[4];

            name[0] = "LUNAM";
            name[1] = "PERQUAM ARENOSUM";
            name[2] = "SORTES HYACINTHO";
            name[3] = "NIMIS CALIDA";

            return name[number];
        }

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
            WASDSet[0] = Keys.W;
            WASDSet[1] = Keys.A;
            WASDSet[2] = Keys.S;
            WASDSet[3] = Keys.D;

            ArrowSet[0] = Keys.Up;
            ArrowSet[1] = Keys.Left;
            ArrowSet[2] = Keys.Down;
            ArrowSet[3] = Keys.Right;

            XZSet[0] = Keys.X;
            XZSet[1] = Keys.Z;

            NMSet[0] = Keys.N;
            NMSet[1] = Keys.M;

            screenW = 800;
            screenH = 640;

            worldSpeed = 1f;

            AmountOfEnemyTypes = 7;
        }
    }
}
