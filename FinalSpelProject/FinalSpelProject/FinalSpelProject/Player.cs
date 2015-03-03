using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        byte muzzleFlashCount;

        public byte GetLives() { return lives; }
        public byte GetGunType() { return gunType; }
        public byte GetSpecialGunType() { return specialGunType; }
        public byte GetSpecialAmmo() { return specialAmmo; }
        public byte GetFireRate() { return fireRate; }

        public void SetSpecialAmmo(byte specialAmmo2) { specialAmmo = specialAmmo2; }

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
        short muzzleFlashFrame;

        public short GetCurrentCombo() { return currentCombo; }
        public short GetComboCount() { return comboCount; }
        public short GetMaxComboCount() { return maxComboCount; }
        public short GetCurrentLaserHeigt() { return currentLaserHeigt; }

        public bool Dead {get; set;}
        bool inputActive;
        public bool Flash { get; set; }
        public bool Invisible { get; set; }
        public bool RasieCombo { get; set; }
        public bool NukeDropped { get; set; }
        bool reverseLaser;
        bool spawnedGetReadyText;

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

            gunType = 0;
            specialGunType = 2;
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
                if ((keyboard.IsKeyDown(specialFire) && prevKeyboard.IsKeyUp(specialFire) || gamePad.IsButtonDown(Buttons.B) && prevGamePad.IsButtonUp(Buttons.B)) && specialGunType == 0 && specialFireRate <= 0)
                {
                    NukeDropped = true;
                    specialAmmo -= 1;
                    specialFireRate = 1;
                }
                if ((keyboard.IsKeyDown(fire) && prevKeyboard.IsKeyUp(fire) || gamePad.IsButtonDown(Buttons.X) && prevGamePad.IsButtonUp(Buttons.X)) && gunType == 5 && fireRate <= 0)
                {
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2), Pos.Y + (Height / 2)), -90, 8, 3, 3, false));
                    fireRate = 1;
                }
                if ((keyboard.IsKeyDown(specialFire) && prevKeyboard.IsKeyUp(specialFire) || gamePad.IsButtonDown(Buttons.B) && prevGamePad.IsButtonUp(Buttons.B)) && specialGunType == 2 && specialFireRate <= 0)
                {
                    for (int i = 0; i < 360; i += 20 )
                        projectiles.Add(new Projectile(new Vector2((Pos.X + (Width / 2)-16) + (float)Math.Cos(i)*100, (Pos.Y + (Height / 2))+ (float)Math.Sin(i)*100), -90, 8, 4, 5, false));
                    specialFireRate = 1;
                    specialAmmo -= 1;
                }
            }
        }
        public void Update(List<Projectile> projectiles, List<Enemy> enemies, List<Explosion> explosions, List<TextEffect> textEffects)
        {
            HitBox = FullHitBox;
            Random random = new Random();

            UpdateMuzzeflash();

            if (specialAmmo <= 0)
            {
                specialGunType = 255;
            }
            
            if(fireRate == 1 && (gunType >= 0 && gunType <= 2))
            {
                SoundManager.NormalShot.Play();
            }

            switch(gunType)
            {
                case 0:
                    maxFireRate = 16;
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
                case 5:
                    maxFireRate = 24;
                    break;
            }
            switch(specialGunType)
            {
                case 0:
                    specialMaxFireRate = 64*2;
                    break;
                case 2:
                    specialMaxFireRate = 64 * 3;
                    break;
            }
            if(gunType == 1 && fireRate >= 1)
            {
                if (fireRate == 8 || fireRate == 16 || fireRate == 24)
                {
                    SoundManager.NormalShot.Play();
                    projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), -90 + random.Next(-5 - (fireRate / 5), 5 + (fireRate / 5)), 9, 0, 0, false));
                    muzzleFlashCount = 1;
                }
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
            LivesUpdate(explosions, textEffects);
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
                        SoundManager.Hit.Play();
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
        public void UpdateMuzzeflash()
        {
            muzzleFlashCount = (fireRate == 1) ? muzzleFlashCount = 1 : muzzleFlashCount;
            muzzleFlashCount = (muzzleFlashCount >= 1) ? (byte)(muzzleFlashCount + 1) : muzzleFlashCount;
            if(muzzleFlashCount <= 1 || muzzleFlashCount >= 16 || muzzleFlashFrame > 3)
            {
                muzzleFlashFrame = 0;
            }
            if(muzzleFlashCount >= 16)
            {
                muzzleFlashCount = 0;
            }

            if (muzzleFlashCount % 4 == 0 && muzzleFlashCount != 0)
            {
                muzzleFlashFrame += 1;
            }
        }
        public void Movment()
        {
            if(Pos.X <= -velLeft && !Dead)
            {
                velLeft = 0;
            }
            if (Pos.X >= Globals.screenW - Width - velRight && !Dead)
            {
                velRight = 0;
            }
            if (Pos.Y <= -velUp && !Dead)
            {
                velUp = 0;
            }
            if (Pos.Y >= Globals.screenH - Height - velDown && !Dead)
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
        public void LivesUpdate(List<Explosion> explosions, List<TextEffect> textEffects)
        {
            Random random = new Random();
            if(!spawnedGetReadyText && Dead)
            {
                SoundManager.PlayerDeath.Play();
                textEffects.Add(new TextEffect(new Vector2(-200, Globals.screenH / 2), "GET", 1.0f, Color.Red, new Vector2((Globals.screenW / 2)-50, Globals.screenH / 2), 0.03f, (short)(maxRespawnCount+200), 2, 1));
                textEffects.Add(new TextEffect(new Vector2(Globals.screenW + 200, Globals.screenH / 2 + 32), "READY", 1.0f, Color.Red, new Vector2((Globals.screenW / 2) - 50, Globals.screenH / 2 + 32), 0.03f, (short)(maxRespawnCount + 200), 2, 1));
            }
            if(Dead && respawnCount <= 0)
            {
                currentCombo = 0;
                gunType = 0;
                explosionDelay = (explosionDelay >= 4) ? (byte)0 : (byte)(explosionDelay + 1);
                if (explosionDelay % 16 == 0) explosions.Add(new Explosion(Pos + new Vector2(random.Next(-16, Width), random.Next(-16, Height)), 32, false, true));
                inputActive = false;
                velDown += 0.5f;
                spawnedGetReadyText = true;
                if (Pos.Y >= Globals.screenH + Height * 3)
                {
                    if(lives != 0) lives -= 1;
                    respawnCount = 1;
                    Pos = new Vector2(Globals.screenW / 2 - Width / 2, Globals.screenH + Height * 3);
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
                Pos = new Vector2(Pos.X, Lerp(Pos.Y, Globals.screenH / 2 - Height / 2, 0.04f));
                respawnCount += 1;
                flashCount += 1;
                Invisible = true;
                if(respawnCount >= maxRespawnCount)
                {
                    inputActive = true;
                    Dead = false;
                    spawnedGetReadyText = false;
                    Flash = false;
                    flashCount = 0;
                    respawnCount = 0;
                    Invisible = false;
                    invisibleCount = 1;
                }
            }
        }
        public void UpdateSounds()
        {

        }
        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            if(muzzleFlashCount > 0)
            {
                spriteBatch.Draw(spritesheet, new Vector2(Pos.X + 19, Pos.Y), new Rectangle(66+Frame(muzzleFlashFrame, 24), 1, 24, 9), Color.White);
            }
            DrawSprite(spriteBatch, spritesheet);
        }
    }
}
