using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalSpelProject
{
    class PowerUp : GameObject
    {
        byte type;
        byte movmentPattern;

        bool special;

        float cosCount;

        string name;

        public PowerUp(Vector2 pos2, byte type2, byte movmentPattern2, bool special2)
        {
            special = special2;
            movmentPattern = movmentPattern2;
            Pos = pos2;
            type = type2;
            SetSize(16);
            if(!special) SetSpriteCoords((short)(780+FrameX(type)), 34);
                 else SetSpriteCoords((short)(780+FrameX(type)), 50);
            Speed = 4f;
            AnimationActive = true;
            MinFrame = 0;
            MaxFrame = 5;
            MaxAnimationCount = 2;
            name = GetName();
        }

        public void Update(List<Player> players, List<TextEffect> textEffects, List<AlliedShip> alliedShips)
        {
            cosCount += 0.01f;
            switch (movmentPattern)
            {
                case 0:
                    Pos += new Vector2((float)Math.Cos((2 * (float)Math.PI * 1.2f) * cosCount), Speed);
                    break;
                case 1:
                    Pos += new Vector2(0, Globals.worldSpeed);
                    Pos += new Vector2(0, (float)Math.Sin(30 * cosCount + 30));
                    break;
            }
            AnimationCount += 1;
            Animate();
            HitBox = FullHitBox;
            foreach(Player p in players)
            {
                if(HitBox.Intersects(p.HitBox))
                {
                    if (p.GetGunType() != type)
                    {
                        textEffects.Add(new TextEffect(new Vector2(290, -100), name, 1.0f, Color.Black, new Vector2(290, 240), 0.1f, 200, 1, 1));
                        if (type == 1 && special)
                            alliedShips.Add(new AlliedShip(new Vector2(Globals.screenW / 2 - 12, Globals.screenH / 2 - 12)));
                        else if (type == 4 && special)
                            p.SetLives((byte)(p.GetLives() + 1));
                        else
                            p.SetGunType(type, special);
                    }
                    else
                    {
                        textEffects.Add(new TextEffect(new Vector2(290, -100), "5000+ points", 1.0f, Color.Black, new Vector2(290, 240), 0.1f, 200, 1, 1));
                        p.Score += 5000;
                    }
                    if(special && type != 1 && type != 4)
                    {
                        p.SetSpecialAmmo((byte)(p.GetSpecialAmmo() + 3)); 
                    }
                    SoundManager.PowerUp.Play();
                    Destroy = true;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, Pos - new Vector2(8, 8), new Rectangle(Frame(CurrentFrame, 32), 911, 32, 32), Color.White);
            DrawSprite(spriteBatch, spritesheet);
        }
        public string GetName()
        {
            string[] names = new string[8];

            if (!special)
            {
                names[0] = "PISTOL - HOW DID YOU GET THIS?";
                names[1] = "RAPIDFIRE";
                names[2] = "SPREADGUN";
                names[3] = "MISSILE";
                names[4] = "LAZER BEAM";
                names[5] = "LAZER-SHURIKEN";
                names[6] = "DOUBLE-BARRELD CANNON";
                names[7] = "FLAME THROWER";
            }
            else
            {
                names[0] = "NUKE";
                names[1] = "ALLIED SHIP";
                names[2] = "FIREBALL-CIRCLE";
                names[3] = "BLACKHOLE";
                names[4] = "BONUS LIFE";
            }
            return names[type];
        }
    }
}
