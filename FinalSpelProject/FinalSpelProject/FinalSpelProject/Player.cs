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
        public int Score { get; set; }

        sbyte lives;

        byte flashCount;
        byte gunType;
        byte fireRate;
        byte maxFireRate;
        byte invisibleCount;

        short comboCount;
        short currentCombo;
        short respawnCount;
        short maxRespawnCount;

        public bool Dead {get; set;}
        bool inputActive;
        public bool Flash { get; set; }
        public bool Invisible { get; set; }
        public bool RasieCombo { get; set; }

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

            currentCombo = 5;

            maxVel = 4;
            Speed = 0.7f;

            gunType = 1;

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
                    projectiles.Add(new Projectile(new Vector2(Pos.X+16-3, Pos.Y+16-3), -90, 9, 0, 0, false));
                }
                if (keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) && gunType == 1 && fireRate <= 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), -90, 9, 0, 0,false));
                    fireRate = 1;
                }
                if (keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) && gunType == 2 && fireRate <= 0)
                {
                    for (int i = 0; i < 3; i++ )
                        projectiles.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), -65-i*25, 9, 0, 0, false));
                    fireRate = 1;
                }
                if (keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) && gunType == 3 && fireRate <= 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), -90, -2, 0, 1, false));
                    fireRate = 1;
                }
            }
        }
        public void Update(List<Projectile> projectiles)
        {
            HitBox = new Rectangle((int)Pos.X, (int)Pos.Y,Width,Height);
            Random random = new Random();

            switch(gunType)
            {
                case 1:
                    maxFireRate = 64;
                    break;
                case 2:
                    maxFireRate = 64;
                    break;
                case 3:
                    maxFireRate = 64 * 2;
                    break;
            }
            if(gunType == 1 && fireRate >= 1)
            {
                if(fireRate == 8 || fireRate == 16 || fireRate == 24 ) projectiles.Add(new Projectile(new Vector2(Pos.X + 16 - 3, Pos.Y + 16 - 3), -90+random.Next(-5, 5), 9, 0, 0, false));
            }
            if (fireRate >= 1)
                fireRate += 1;
            if(fireRate >= maxFireRate)
            {
                fireRate = 0;
            }

            Movment();
            UpdateInvisiblity();
            UpdateCombo();

            foreach (Projectile p in projectiles)
            {
                if (p.HitBox.Intersects(HitBox) && p.EnemyShot == true && !Invisible)
                {
                    p.Destroy = true;
                    if (gunType == 0)
                    {
                        Dead = true;
                    }
                    if (gunType >= 1)
                    {
                        gunType = 0;
                        invisibleCount = 1;
                    }
                }
            }
        }
        public void Movment()
        {
            if (velLeft <= -0.3f)
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
        public void UpdateInvisiblity()
        {
            if (invisibleCount >= 1)
            {
                Invisible = true;
                flashCount += 1;
                invisibleCount += 1;
                if (invisibleCount >= 64)
                {
                    Invisible = false;
                    invisibleCount = 0;
                    flashCount = 0;
                    Flash = false;
                }
            }
        }
        public void UpdateCombo()
        {
            if(RasieCombo)
            {
                comboCount = 0;
                currentCombo += 1;
                RasieCombo = false;
            }
            if(comboCount >= 64)
            {
                currentCombo = 0;
                comboCount = 0;
            }
            if (currentCombo >= 1)
                comboCount += 1;
            comboCount = (short)((currentCombo > 0) ? comboCount + 1 : comboCount = 0);
        }
        public void LivesUpdate()
        {
            if(Dead && respawnCount <= 0)
            {
                inputActive = false;
                velDown += 0.5f;
                if (Pos.Y >= 480)
                {
                    lives -= 1;
                    respawnCount = 1;
                    Pos = new Vector2(640 / 2 - 16, 550);
                }
            }
            if(flashCount >= 8)
            {
                if (Flash) Flash = false;
                    else Flash = true;
                flashCount = 0;
            }
            if(respawnCount >= 1)
            {
                Pos = new Vector2(Pos.X, Lerp(Pos.Y, 240, 0.04f));
                respawnCount += 1;
                flashCount += 1;
                Invisible = true;
                if(respawnCount >= maxRespawnCount)
                {
                    inputActive = true;
                    Dead = false;
                    Flash = false;
                    flashCount = 0;
                    respawnCount = 0;
                    Invisible = false;
                    invisibleCount = 0;
                }
            }
        }
    }
}
