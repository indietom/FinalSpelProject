using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalSpelProject
{
    class Boss : GameObject
    {
        byte hp;
        byte type;

        short[] fireRates;
        short[] maxFireRates;

        int Health;
        int Firerate;
        int AltFirerate;
        Vector2 targetLine;
        bool targeted;

        short invisibleCount;
        short maxInvisibleCount;

        BossPart[] bossParts;

        public Boss(Vector2 pos2, byte type2)
        {
            Pos = pos2;
            type = type2;
            maxInvisibleCount = 8;
            OrginalColor = color;
            switch (type)
            {
                case 1:
                    SetSpriteCoords(1, Frame(6));
                    SetSize(64);
                    AnimationActive = true;
                    Speed = 5;
                    MaxFrame = 3;
                    MaxAnimationCount = 8;
                    Health = 100;
                    Firerate = 120;
                    AltFirerate = 300;
                    
                    break;
            }

        }

        public void Update(List<Player> player, List<Projectile> projectiles)
        {
            switch (type)
            {
                case 1:
                    if (Speed > 0)
                    {
                        if (Speed != 0)
                        {
                            Pos = new Vector2(Pos.X, Lerp(Pos.Y, 128, 0.02f));
                        }
                    }
                         
                    AltFirerate -= 1;
                    if (AltFirerate <= 100 && AltFirerate > 0 && !player[0].Dead)
                    {

                        float tempAngle = AimAt(player[0].GetCenter);
                        if (!targeted)
                        {
                            targeted = true;
                        }
                        if (targeted == true)
                        {
                            projectiles.Add(new Projectile(new Vector2(Pos.X + (Width / 2) - 3, Pos.Y + (Height / 2) - 3), tempAngle, 10, 0, 0, true, true));
                        }
                        
                    }
                    if (AltFirerate == 0)
                    {
                        AltFirerate = 300;
                        targeted = false;
                    }
                    break;
            }
        }

        public void Attack(List<Projectile> projectiles)
        {
            switch (type)
            {
                case 0:

                    break;
            }
        }

        public void CheckHealth()
        {
            if(invisibleCount >= 1)
            {
                color = Color.Red;
                if(invisibleCount >= maxInvisibleCount)
                {
                    color = OrginalColor;
                    invisibleCount = 0;
                }
            }
            if(hp <= 0)
            {
                switch(type)
                {
                    case 0:
                        // TODO: boss slowlyfalls of screen 
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            DrawSprite(spriteBatch, spritesheet);
        }

        public void AssignnValues()
        {
            switch (type)
            {
                case 0:
     
                    break;
            }
        }
    }
    struct BossPart
    {
        Vector2 pos;

        byte type;
        byte hp;
        byte width;
        byte height;
        
        public byte GetHp() { return hp; }
        public byte GetType() { return type; }

        short imx;
        short imy;

        Rectangle hitBox;

        public BossPart(Vector2 pos2, byte type2, short imx2, short imy2, byte width2, byte height2, byte hp2)
        {
            pos = pos2;
            type = type2;
            width = width2;
            height = height2;
            imx = imx2;
            imy = imy2;
            hp = hp2;
            hitBox = new Rectangle();
        }

        public void Update(List<Projectile> projectiles)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            foreach(Projectile p in projectiles)
            {
                if(!p.EnemyShot && hitBox.Intersects(p.HitBox))
                {
                    if (hp - p.Dm < 0)
                        hp = 0;
                    else
                        hp -= p.Dm;
                    p.Destroy = true;
                }
            }
            switch(type)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White);
        }
    }
}
