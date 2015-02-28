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
            visualText = "";
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
            if (Height != (short)font.MeasureString(fullText).X || Width != (short)font.MeasureString(fullText).Y)
                SetSize((short)(font.MeasureString(fullText).X/16), (short)(font.MeasureString(fullText).Y/16));
            spriteBatch.Draw(spritesheet, new Rectangle((int)Pos.X, (int)Pos.Y, Width*16, Height*16+3), new Rectangle(1, 1496, 32, 32), Color.Black);
            for (int y = 0; y < Height; y++)
            {
                for(int x = 0; x < Width; x++)
                {
                    if (x == 0) spriteBatch.Draw(spritesheet, new Vector2(Pos.X - 3, (Pos.Y + 3) + y * 16), new Rectangle(1041, 1, 3, 16), Color.White);
                    if (x == Width-1) spriteBatch.Draw(spritesheet, new Vector2((Pos.X)+Width*16, (Pos.Y + 3) + y * 16), new Rectangle(1041, 18, 3, 16), Color.White);
                    if (y == 0) spriteBatch.Draw(spritesheet, new Vector2(Pos.X + x * 16, Pos.Y), new Rectangle(1041, 39, 16, 3), Color.White);
                    if (y == Height-1) spriteBatch.Draw(spritesheet, new Vector2(Pos.X + x * 16, (Pos.Y + 3)+Height*16), new Rectangle(1041, 35, 16, 3), Color.White);
                }
            }
            spriteBatch.Draw(spritesheet, Pos-new Vector2(3, 0), new Rectangle(1041, 59, 4, 4), Color.White);
            spriteBatch.Draw(spritesheet, Pos - new Vector2(3, -Height*16-2), new Rectangle(1041, 44, 4, 4), Color.White);
            spriteBatch.Draw(spritesheet, Pos - new Vector2(3-Width*16-2, 0), new Rectangle(1041, 54, 4, 4), Color.White);
            spriteBatch.Draw(spritesheet, Pos - new Vector2(3-Width*16-2, -Height * 16 - 2), new Rectangle(1041, 49, 4, 4), Color.White);
            spriteBatch.DrawString(font, visualText, Pos, color, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 1.0f);
        }
    }
}
