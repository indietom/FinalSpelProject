﻿using System;
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
            SetSize(32);

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
            if(inputActive)
            {

            }
        }
        public void Update()
        {
            if(VelX >= 0.3 || VelX <= -0.3)
            {
                Pos += new Vector2(VelX, 0);
                if (VelX >= 0.3f) VelX -= 0.1f;
                if (VelX <= -0.3f) VelX += 0.1f;
            }
            if (VelY >= 0.3 || VelY <= -0.3)
            {
                Pos += new Vector2(0, VelY);
                if (VelY >= 0.3f) VelY -= 0.1f;
                if (VelY <= -0.3f) VelY += 0.1f;
            }
        }
        public void livesUpdate()
        {

        }
    }
}
