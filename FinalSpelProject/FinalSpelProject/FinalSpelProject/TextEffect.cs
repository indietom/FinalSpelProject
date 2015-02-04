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
        short lifeTime;
        short maxLifeTime;

        float size;

        Color color;

        byte tag;

        string text;

        public TextEffect(Vector2 pos2, string text2,float size2, Color color2, float ang, float speed2, short lifeTime2, short maxLifeTime2, byte tag2)
        {
            text = text2;
            Pos = pos2;
            color = color2;
            size = size2;
            Angle = ang;
            Speed = speed2;
            lifeTime = lifeTime2;
            maxLifeTime = maxLifeTime2;
            tag = tag2;
        }
        public void Update()
        {
            lifeTime += 1;
            if (lifeTime >= maxLifeTime)
            {
                Destroy = true;
            }
            switch(tag)
            {
                case 0:
                    AngleMath(false);
                    Pos += Vel;
                    break;
                case 1:
                    Pos = new Vector2(Pos.X, Lerp(Pos.Y, 240, 0.1f));
                    color = new Color(Lerp(color.R, 255, 0.001f), Lerp(color.G, 255, 0.001f), 0);
                    break;
            }
        }
        public void FlashColor(Color color2)
        {

        }
        public void Flash()
        {

        }
        public void Bounce()
        {

        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, text, Pos, color, Rotation, new Vector2(0, 0), size, SpriteEffects.None, 1.0f);
        }
    }
}
