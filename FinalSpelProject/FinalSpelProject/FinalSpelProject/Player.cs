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

        byte flashCount;
        byte gunType;
        byte fireRate;
        byte maxFireRate;

        short comboCount;
        short currentCombo;
        short respawnCount;
        short maxRespawnCount;

        bool dead;
        bool inputActive;
        public bool Flash { get; set; }

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

            maxRespawnCount = 130;

            left = Keys.Left;
            right = Keys.Right;
            down = Keys.Down;
            up = Keys.Up;
            fire = Keys.X;
        }
        public void Input(List<Projectile> projectiles)
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
                if (keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) && gunType == 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X+16-3, Pos.Y+16-3), -90, 9, 0, 0));
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
        public void LivesUpdate()
        {
            if(dead && respawnCount <= 0)
            {
                inputActive = false;
                velDown += 0.5f;
                if (Pos.Y >= 480)
                {
                    lives -= 1;
                    respawnCount = 1;
                    Pos = new Vector2(800 / 2 - 16, 550);
                }
            }
            if(flashCount >= 8)
            {
                if (Flash) Flash = false;
                    else Flash = true;
                Console.WriteLine(Flash);
                flashCount = 0;
            }
            if(respawnCount >= 1)
            {
                //Pos -= new Vector2(0, 3);
                Pos = new Vector2(Pos.X, Lerp(Pos.Y, 240, 0.04f));
                respawnCount += 1;
                flashCount += 1;
                if(respawnCount >= maxRespawnCount)
                {
                    inputActive = true;
                    dead = false;
                    Flash = false;
                    flashCount = 0;
                    respawnCount = 0;
                }
            }
        }
    }
}
