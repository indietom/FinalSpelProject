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

        byte amountOfLives;

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
                powerUpFrame = (short)(462 + Frame(p.GetGunType(), 16));
                scoreText = "SCORE: " + p.Score.ToString().PadLeft(8, '0');
            }
            printLives = (amountOfLives <= 3) ? false : true;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            spriteBatch.Draw(spritesheet, new Vector2(10, 32), new Rectangle(463, 34, 32, 32), Color.White);
            spriteBatch.Draw(spritesheet, new Vector2(10+8, 32+8), new Rectangle(powerUpFrame, 1, 16, 16), Color.White);
            if(!printLives)
            {
                for(int i = 0; i < amountOfLives; i++)
                    spriteBatch.Draw(spritesheet, new Vector2(10 + i * 24, 10), new Rectangle(529, 34, 16, 16), Color.White);
            }
            else
            {
                spriteBatch.Draw(spritesheet, new Vector2(10, 10), new Rectangle(529, 34, 16, 16), Color.White);
                spriteBatch.DrawString(font, "x" + amountOfLives.ToString(), new Vector2(30, 7), Color.Yellow, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 1.0f);
            }
            comboCounter.Draw(spriteBatch, font, spritesheet);
            spriteBatch.DrawString(font, scoreText, new Vector2(10, 32+8+20), Color.White);
        }
    }
}
