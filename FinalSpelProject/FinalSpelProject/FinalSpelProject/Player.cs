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

        float velLeft;
        float velRight;
        float velUp;
        float velDown;

        float maxVel;

        Keys left;
        Keys right;
        Keys down;
        Keys up;
        Keys fire;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Player()
        {
            Pos = new Vector2(320, 240);
            SetSpriteCoords(1, 1);
            SetSize(32);

            inputActive = true;
            AnimationActive = true;

            lives = 3;

            maxVel = 4;
            Speed = 0.7f;

            left = Keys.Left;
            right = Keys.Right;
            down = Keys.Down;
            up = Keys.Up;
            fire = Keys.X;
        }
        public void Input()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();
            if(inputActive)
            {
                if(keyboard.IsKeyDown(left) && velLeft >= -maxVel)
                {
                    velLeft -= Speed;
                }
                if (keyboard.IsKeyDown(right) && velRight <= maxVel)
                {
                    velRight += Speed;
                }
                if (keyboard.IsKeyDown(up) && velUp >= -maxVel)
                {
                    velUp -= Speed;
                }
                if (keyboard.IsKeyDown(down) && velDown <= maxVel)
                {
                    velDown += Speed;
                }
            }
        }
        public void Update()
        {
            if(velLeft <= -0.3f)
            {
                Pos += new Vector2(velLeft, 0);
                velLeft += 0.2f;
            }
            if (velRight >= 0.3f)
            {
                Pos += new Vector2(velRight, 0);
                velRight -= 0.2f;
            }
            if (velUp <= -0.3f)
            {
                Pos += new Vector2(0, velUp);
                velUp += 0.2f;
            }
            if (velDown >= 0.3f)
            {
                Pos += new Vector2(0, velDown);
                velDown -= 0.2f;
            }
        }
        public void livesUpdate()
        {

        }
    }
}
