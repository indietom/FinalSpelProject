using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalSpelProject
{
    class TextBox : GameObject
    {
        string fullText;
        string visualText;

        byte addLetterCount;
        byte maxAddLetterCount;

        int currentIndex;

        bool done;

        public TextBox(Vector2 pos2, string text2, Color color2, byte maxAddLetterCount2)
        {
            Pos = pos2;
            fullText = text2;
            color = color2;
            OrginalColor = color2;
            maxAddLetterCount = maxAddLetterCount2;
        }

        public void Update()
        {
            addLetterCount += 1;
            if(addLetterCount >= maxAddLetterCount)
            {
                done = (fullText == visualText) ? true : done;
                if(!done) visualText += fullText[currentIndex];
                currentIndex += 1;
                addLetterCount = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            spriteBatch.DrawString(font, fullText, Pos, color, 0, new Vector2(0, 0), 0.7f, SpriteEffects.None, 1.0f);
        }
    }
}
