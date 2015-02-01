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
        byte amountOfGuns;
        byte amountOfParts;

        short[] fireRates;
        short[] maxFireRates;

        short invisibleCount;
        short maxInvisibleCount;

        BossPart[] bossParts;

        public Boss()
        {

        }

        public void Update(List<Player> player, List<Projectile> projectiles)
        {

        }

        public void Attack(List<Projectile> projectiles)
        {

        }

        public void CheckHealth()
        {

        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            DrawSprite(spriteBatch, spritesheet);
            if (bossParts.Count() != 0)
                for (int i = 0; i < bossParts.Count(); i++)
                    bossParts[i].Draw(spriteBatch, spritesheet);
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
                if(p.EnemyShot && hitBox.Intersects(p.HitBox))
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
                case 0:
                    imx = (hp <= 0) ? (short)430 : (short)397;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, pos, new Rectangle(imx, imy, width, height), Color.White);
        }
    }
}
