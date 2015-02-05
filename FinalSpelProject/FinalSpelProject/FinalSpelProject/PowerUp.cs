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
            if(!special) SetSpriteCoords((short)(462+FrameX(type)), 1);
                 else SetSpriteCoords(FrameX(type), Frame(5));
            Speed = 4f;
            name = GetName();
        }

        public void Update(List<Player> players, List<TextEffect> textEffects)
        {
            // TODO: Make it flash to attract the player's attention
            cosCount += 0.01f;
            switch (movmentPattern)
            {
                case 0:
                    Pos += new Vector2((float)Math.Cos((2 * (float)Math.PI * 1.2f) * cosCount), Speed);
                    break;
                case 1:
                    Pos += new Vector2(0, Game1.worldSpeed);
                    break;
            }
            HitBox = FullHitBox;
            foreach(Player p in players)
            {
                if(HitBox.Intersects(p.HitBox))
                {
                    textEffects.Add(new TextEffect(new Vector2(290, -100), name, 1.0f, Color.Black, new Vector2(290, 240), 0.1f, 100, 1, 1));
                    if (p.GetGunType() != type) p.SetGunType(type, special);
                    else p.Score += 5000;
                    Destroy = true;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet)
        {
            spriteBatch.Draw(spritesheet, Pos - new Vector2(8, 8), new Rectangle(496, 34, 32, 32), Color.White);
            DrawSprite(spriteBatch, spritesheet);
        }
        public string GetName()
        {
            string[] names = new string[4];
            string[] specialNames = new string[4];

            names[0] = "PISTOL - HOW DID YOU GET THIS?";
            names[1] = "SUB-MACHINEGUN";
            names[2] = "SPREADGUN";
            names[3] = "ROCKETLAUNCHER";

            specialNames[0] = "NUKE";

            if (!special)
                return names[type];
            else
                return specialNames[type];
        }
    }
}
