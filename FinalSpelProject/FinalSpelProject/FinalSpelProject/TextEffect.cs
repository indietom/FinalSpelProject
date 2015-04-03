using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalSpelProject
{
    class TextEffect:GameObject
    {
        Vector2 target;
       
        short lifeTime;
        short maxLifeTime;
        short flashCount;
        short textIndex;

        float size;
        float r;
        float g;
        float b;
        float fallSpeed;

        Color color;

        byte tag;
        byte type;
        byte addLetterCount;

        public byte GetTag() { return tag; }

        string text;
        string fullText;

        public TextEffect(Vector2 pos2, string text2, float size2, Color color2, float ang, float speed2, short maxLifeTime2, byte tag2, byte type2)
        {
            type = type2;
            text = text2;
            Pos = pos2;
            color = color2;
            size = size2;
            Angle = ang;
            Speed = speed2;
            maxLifeTime = maxLifeTime2;
            tag = tag2;
            OrginalColor = color;
        }
        public TextEffect(Vector2 pos2, string text2, float size2, Color color2, Vector2 target2, float speed2, short maxLifeTime2, byte tag2, byte type2)
        {
            target = target2;
            type = type2;
            text = text2;
            Pos = pos2;
            color = color2;
            size = size2;
            Speed = speed2;
            maxLifeTime = maxLifeTime2;
            tag = tag2;
            OrginalColor = color;
        }
        public TextEffect(Vector2 pos2, string text2, float size2, Color color2, Vector2 target2, float speed2, short maxLifeTime2, byte tag2, byte type2, string fullText2)
        {
            target = target2;
            type = type2;
            text = text2;
            fullText = fullText2;
            Pos = pos2;
            color = color2;
            size = size2;
            Speed = speed2;
            maxLifeTime = maxLifeTime2;
            tag = tag2;
            OrginalColor = color;
        }
        public void Update()
        {
            lifeTime += 1;
            if (lifeTime >= maxLifeTime)
            {
                Destroy = true;
            }
            switch(type)
            {
                case 0:
                    AngleMath(false);
                    Pos += Vel;
                    break;
                case 1:
                    Pos = new Vector2(Lerp(Pos.X, target.X, Speed), Lerp(Pos.Y, target.Y, Speed));  
                    break;
            }
            switch(tag)
            {
                case 1:
                    if (lifeTime + 100 >= maxLifeTime)
                    {
                        Rotation += fallSpeed / 1000;
                        fallSpeed += 0.3f;
                        target += new Vector2(0, fallSpeed);
                    }
                    color = new Color(r, g, b);
                    r = Lerp(r, 178, 0.0001f);
                    b = Lerp(b, 255, 0.0001f);
                    break;
                case 2:
                    color = new Color(255 - lifeTime, 216 - lifeTime, 0);
                    if(lifeTime + 200 >= maxLifeTime)
                    {
                        Rotation += fallSpeed/1000;
                        fallSpeed += 0.3f;
                        target += new Vector2(0, fallSpeed);
                    }
                    break;
                case 3:
                    FlashColor(8);
                    break;
                case 4:
                    AddLetters(4);
                    break;
                    
            }
        }
        public void FlashColor(int interval)
        {
            Random random = new Random();
            flashCount += 1;
            if(flashCount % interval == 0)
            {
                color = new Color(random.Next(255), random.Next(255), random.Next(255));
            }
        }
        public void Flash()
        {

        }
        public void Bounce()
        {

        }
        public void AddLetters(byte maxCount)
        {
            if (fullText != text)
            {
                addLetterCount += 1;
                if (addLetterCount >= maxCount)
                {
                    textIndex += 1;
                    text += fullText[textIndex-1];
                    addLetterCount = 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, text, Pos, color, Rotation, new Vector2(0, 0), size, SpriteEffects.None, 1.0f);
        }
    }
}
