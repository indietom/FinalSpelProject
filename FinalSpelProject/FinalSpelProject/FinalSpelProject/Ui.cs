using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalSpelProject
{
    class Ui:GameObject
    {
        ComboCounter comboCounter;

        string scoreText;

        short powerUpFrame;
        short specialPowerUpFrame;

        byte amountOfLives;
        byte amountOfSpecialAmmo;
        byte specialGunType;

        bool printLives;

        public Ui()
        {
            comboCounter = new ComboCounter(new Vector2(220, 10));
        }

        public void Update(List<Player> player)
        {
            comboCounter.Update(player);
            foreach(Player p in player)
            {
                amountOfLives = p.GetLives();
                powerUpFrame = (short)(781 + Frame(p.GetGunType(), 16));
                specialPowerUpFrame = (short)(781 + Frame((short)(p.GetSpecialGunType()-1), 16));
                scoreText = "SCORE: " + p.Score.ToString().PadLeft(9, '0');
                amountOfSpecialAmmo = p.GetSpecialAmmo();
                specialGunType = p.GetSpecialGunType();
            }
            printLives = (amountOfLives <= 3) ? false : true;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            spriteBatch.Draw(spritesheet, new Vector2(10, 32), new Rectangle(781, 1, 32, 32), Color.White);
            spriteBatch.Draw(spritesheet, new Vector2(47, 32), new Rectangle(781, 1, 32, 32), Color.White);
            spriteBatch.Draw(spritesheet, new Vector2(10+8, 32+8), new Rectangle(powerUpFrame, 34, 16, 16), Color.White);
            if(amountOfSpecialAmmo > 1) spriteBatch.DrawString(font, "x" + amountOfSpecialAmmo.ToString(), new Vector2(47 + 38, 32), Color.Yellow, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 1.0f);
            if(specialGunType != 0) spriteBatch.Draw(spritesheet, new Vector2(47 + 8, 32 + 8), new Rectangle(specialPowerUpFrame, 50, 16, 16), Color.White);
            if(!printLives)
            {
                for(int i = 0; i < amountOfLives; i++)
                    spriteBatch.Draw(spritesheet, new Vector2(10 + i * 24, 10), new Rectangle(847, 1, 16, 16), Color.White);
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(10, 10), new Rectangle(847, 1, 16, 16), Color.White);
                spriteBatch.DrawString(font, "x" + amountOfLives.ToString(), new Vector2(30, 7), Color.Yellow, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 1.0f);
            }
            comboCounter.Draw(spriteBatch, font, spritesheet);
            spriteBatch.DrawString(font, scoreText, new Vector2(10, 32+8+20), Color.White);
        }
    }
}
