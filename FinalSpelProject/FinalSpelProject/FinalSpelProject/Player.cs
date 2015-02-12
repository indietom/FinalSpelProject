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
        public int Score { get; set; }

        byte lives;
        byte flashCount;
        byte gunType;
        byte fireRate;
        byte maxFireRate;
        byte specialFireRate;
        byte specialMaxFireRate;
        byte invisibleCount;
        byte specialAmmo;
        byte specialGunType;
        byte explosionDelay;

        public byte GetLives() { return lives; }
        public byte GetGunType() { return gunType; }
        public byte GetSpecialGunType() { return specialGunType; }
        public byte GetSpecialAmmo() { return specialAmmo; } 

        public void SetGunType(byte gunType2, bool special2)
        {
            if (!special2) gunType = gunType2;
            else specialGunType = gunType2;
        }

        short comboDecc;
        short comboCount;
        short currentCombo;
        short maxComboCount;
        short respawnCount;
        short maxRespawnCount;
        short currentLaserHeigt;
        short maxLaserHeight;
        short rasieLaserCount;
        short maxRaiseLaserCount;

        public short GetCurrentCombo() { return currentCombo; }
        public short GetComboCount() { return comboCount; }
        public short GetMaxComboCount() { return maxComboCount; }

        public bool Dead {get; set;}
        bool inputActive;
        public bool Flash { get; set; }
        public bool Invisible { get; set; }
        public bool RasieCombo { get; set; }
        public bool NukeDropped { get; set; }
        bool reverseLaser;

        float velLeft;
        float velRight;
        float velUp;
        float velDown;

        float thumbStickMax;

        float maxVel;
        float deccelerate;

        Keys left;
        Keys right;
        Keys down;
        Keys up;
        Keys fire;
        Keys specialFire;

        PlayerIndex playerIndex;

        GamePadState gamePad;
        GamePadState prevGamePad;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Player()
        {
            Pos = new Vector2(320-16, 240);
            SetSpriteCoords(1, 1);
            SetSize(64);

            inputActive = true;
            AnimationActive = true;

            lives = 5;

            maxVel = 5;
            Speed = 1f;
            deccelerate = 0.4f;

            gunType = 4;
            specialGunType = 1;
            specialAmmo = 2;

            maxRespawnCount = 130;

            maxRaiseLaserCount = 1;
            maxLaserHeight = 200;

            playerIndex = PlayerIndex.One;

            thumbStickMax = 0.1f;

            left = Keys.Left;
            right = Keys.Right;
            down = Keys.Down;
            up = Keys.Up;
            fire = Keys.X;
            specialFire = Keys.Z;
        }
        public void Input(List<Projectile> projectiles)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            prevGamePad = gamePad;
            gamePad = GamePad.GetState(playerIndex);

            if (keyboard.IsKeyDown(Keys.Space))
            {
                comboCount += 10;
            }
            if(inputActive)
            {
                if((keyboard.IsKeyDown(left) || gamePad.ThumbSticks.Left.X <= -thumbStickMax) && velLeft >= -maxVel)
                {
                    velLeft -= Speed;
                }
                if ((keyboard.IsKeyDown(right) || gamePad.ThumbSticks.Left.X >= thumbStickMax) && velRight <= maxVel)
                {
                    velRight += Speed;
                }
                if ((keyboard.IsKeyDown(up) || gamePad.ThumbSticks.Left.Y >= thumbStickMax) && velUp >= -maxVel)
                {
                    velUp -= Speed;
                }
                if ((keyboard.IsKeyDown(down) || gamePad.ThumbSticks.Left.Y <= -thumbStickMax) && velDown <= maxVel)
                {
                    velDown += Speed;
                }
                if ((keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) || gamePad.IsButtonDown(Buttons.X) && prevGamePad.IsButtonUp(Buttons.X)) && gunType == 0 && fireRate <= 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90, 9, 0, 0, false));
                    fireRate = 1;
                }
                if ((keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) || gamePad.IsButtonDown(Buttons.X) && prevGamePad.IsButtonUp(Buttons.X)) && gunType == 1 && fireRate <= 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90, 9, 0, 0, false));
                    fireRate = 1;
                }
                if ((keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) || gamePad.IsButtonDown(Buttons.X) && prevGamePad.IsButtonUp(Buttons.X)) && gunType == 2 && fireRate <= 0)
                {
                    for (int i = 0; i < 3; i++ )
                        projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -80 - i * 10, 9, 0, 0, false));
                    fireRate = 1;
                }
                if ((keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) || gamePad.IsButtonDown(Buttons.X) && prevGamePad.IsButtonUp(Buttons.X)) && gunType == 3 && fireRate <= 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90, -2, 1, 1, false));
                    fireRate = 1;
                }
                if((keyboard.IsKeyDown(fire) || gamePad.IsButtonDown(Buttons.X)) && gunType == 4 && currentLaserHeigt < maxLaserHeight && !reverseLaser)
                {
                    rasieLaserCount += 1;
                    if(rasieLaserCount >= maxRaiseLaserCount)
                    {
                        currentLaserHeigt += 3;
                        rasieLaserCount = 0;
                    }
                }
                if (!keyboard.IsKeyDown(fire) && gamePad.IsButtonUp(Buttons.X) && gunType == 4 && currentLaserHeigt > 10 && !reverseLaser)
                {
                    currentLaserHeigt -= 1;
                }
                if ((keyboard.IsKeyDown(specialFire) && prevKeyboard.IsKeyUp(specialFire) || gamePad.IsButtonDown(Buttons.B) && prevGamePad.IsButtonUp(Buttons.B)) && specialGunType == 1 && specialFireRate <= 0)
                {
                    NukeDropped = true;
                    specialAmmo -= 1;
                    specialFireRate = 1;
                }
            }
        }
        public void Update(List<Projectile> projectiles, List<Enemy> enemies, List<Explosion> explosions)
        {
            HitBox = FullHitBox;
            Random random = new Random();

            if (specialAmmo <= 0)
            {
                specialGunType = 0;
            }

            switch(gunType)
            {
                case 0:
                    maxFireRate = 12;
                    break;
                case 1:
                    maxFireRate = 64-16;
                    break;
                case 2:
                    maxFireRate = 32;
                    break;
                case 3:
                    maxFireRate = 64;
                    break;
            }
            switch(specialGunType)
            {
                case 1:
                    specialMaxFireRate = 64*2;
                    break;
            }
            if(gunType == 1 && fireRate >= 1)
            {
                if (fireRate == 8 || fireRate == 16 || fireRate == 24)
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90 + random.Next(-5 - (fireRate / 5), 5 + (fireRate / 5)), 9, 0, 0, false));
            }
            if(gunType == 4)
            {
                if(currentLaserHeigt >= maxLaserHeight)
                    reverseLaser = true;
                if (currentLaserHeigt <= 100)
                    reverseLaser = false;
                if (reverseLaser)
                {
                    currentLaserHeigt -= 1;
                }
                
                maxLaserHeight = (reverseLaser) ? (short)100 : (short)200;
                for(int i = 0; i < currentLaserHeigt; i++)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 8, Pos.Y - i), 0, 0, 2, 0, false, 1));
                }
            }

            if (specialFireRate >= 1)
                specialFireRate += 1;
            if (specialFireRate >= specialMaxFireRate)
                specialFireRate = 0;
            if (fireRate >= 1)
                fireRate += 1;
            if(fireRate >= maxFireRate)
                fireRate = 0;

            Movment();
            UpdateInvisiblity();
            UpdateCombo();
            LivesUpdate(explosions);
            Input(projectiles);

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
            if(NukeDropped)
            {
                foreach(Enemy e in enemies)
                {
                    if (e.Pos.Y >= -e.Height)
                        e.Destroy = true;
                }
                foreach(Projectile p in projectiles)
                {
                    if (p.Pos.Y >= -p.Height && p.EnemyShot)
                        p.Destroy = true;
                }
                Game1.flashScreenCount = 1;
                NukeDropped = false;
            }
        }
        public void Movment()
        {
            if(Pos.X <= -velLeft && !Dead)
            {
                velLeft = 0;
            }
            if (Pos.X >= Game1.screenW - Width - velRight && !Dead)
            {
                velRight = 0;
            }
            if (Pos.Y <= -velUp && !Dead)
            {
                velUp = 0;
            }
            if (Pos.Y >= Game1.screenH - Height - velDown && !Dead)
            {
                velDown = 0;
            }
            if (velLeft <= -0.3f)
            {
                Pos += new Vector2(velLeft, 0);
                velLeft += deccelerate;
            }
            if (velRight >= 0.3f)
            {
                Pos += new Vector2(velRight, 0);
                velRight -= deccelerate;
            }
            if (velUp <= -0.3f)
            {
                Pos += new Vector2(0, velUp);
                velUp += deccelerate;
            }
            if (velDown >= 0.3f)
            {
                Pos += new Vector2(0, velDown);
                velDown -= deccelerate;
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
        public void RaiseCurrentCombo()
        {
            currentCombo += 1;
            comboCount = (short)(maxComboCount);
        }
        public void UpdateCombo()
        {
            maxComboCount = (short)(200 - currentCombo * 5);
            if (comboCount > maxComboCount)
            {
                currentCombo += 1;
                comboCount = 8;
            }
            if (currentCombo >= 1)
            {
                comboDecc += 1;
                if (comboDecc >= 1)
                {
                    comboCount -= 1;
                    comboDecc = 0;
                }
                if (comboCount <= -1)
                {
                    currentCombo -= 1;
                    comboCount = (short)(maxComboCount/2);
                }
            }
        }
        public void LivesUpdate(List<Explosion> explosions)
        {
            Random random = new Random();
            if(Dead && respawnCount <= 0)
            {
                currentCombo = 0;
                gunType = 0;
                explosionDelay = (explosionDelay >= 4) ? (byte)0 : (byte)(explosionDelay + 1);
                if (explosionDelay % 2 == 0) explosions.Add(new Explosion(Pos + new Vector2(random.Next(-16, Width), random.Next(-16, Height)), 32, false));
                inputActive = false;
                velDown += 0.5f;
                if (Pos.Y >= Game1.screenH+Height*3)
                {
                    if(lives != 0) lives -= 1;
                    respawnCount = 1;
                    Pos = new Vector2(Game1.screenW / 2 - Width / 2, Game1.screenH + Height * 3);
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
                Pos = new Vector2(Pos.X, Lerp(Pos.Y, Game1.screenH/2-Height/2, 0.04f));
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
                    invisibleCount = 1;
                }
            }
        }
    }
}
