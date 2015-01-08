using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalSpelProject 
{
    class Player : GameObject 
    {
        int score;

        public int GetScore() 
        { 
            return score; 
        }

        sbyte lives;

        byte gunType;
        byte fireRate;
        byte maxFireRate;

        short respawnCount;
        short maxRespawnCount;

        byte flashingCount;

        bool dead;
        bool inputActive;

        Keys left;
        Keys right;
        Keys down;
        Keys up;
        Keys fire;

        public Player()
        {
            Pos = new Vector2(320, 240);
            SetSpriteCoords(1, 1);
            SetSize(32, 32);

            inputActive = true;
            AnimationActive = true;

            lives = 3;

            left = Keys.Left;
            right = Keys.Right;
            down = Keys.Down;
            up = Keys.Up;
        }
        public void Input()
        {

        }
        public void Update()
        {

        }
        public void livesUpdate()
        {

        }
    }
}
