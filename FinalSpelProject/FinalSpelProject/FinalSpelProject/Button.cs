using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace FinalSpelProject
{
    class Button : GameObject
    {
        byte tag;

        public byte GetTag() { return tag; }

        string text;

        bool unavalible;

        float scale;

        Color selectedColor;
        Color unavalibeColor;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        MouseState mouse;
        MouseState prevMouse;

        public Button(Vector2 pos2, string text2, byte tag2, Color color2, Color selectedColor2, Color unavalibeColor2)
        {
            color = color2;
            OrginalColor = color;
            selectedColor = selectedColor2;
            unavalibeColor = unavalibeColor2;
            scale = 1;
            Pos = pos2;
            text = text2;
            tag = tag2;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, byte currentTag, string newText)
        {
            text = newText;
            if (unavalible) color = unavalibeColor;
            if (currentTag == tag)
            {
                color = selectedColor;
                scale = 1.4f;
            }
            if (!unavalible && currentTag != tag) color = OrginalColor;
            if (currentTag != tag) scale = 1.0f;

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);

            spriteBatch.DrawString(font, text, Pos, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1.0f);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, byte currentTag)
        {
            if (unavalible) color = unavalibeColor;
            if (currentTag == tag)
            {
                color = selectedColor;
                scale = 1.4f;
            }
            if (!unavalible && currentTag != tag) color = OrginalColor;
            if (currentTag != tag) scale = 1.0f;

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            HitBox = new Rectangle((int)Pos.X, (int)Pos.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);

            spriteBatch.DrawString(font, text, Pos, color, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1.0f);
        }

        public bool Pressed(byte currentTag)
        {
            return (keyboard.IsKeyDown(Keys.X) && prevKeyboard.IsKeyUp(Keys.X) && currentTag == tag);
        }
    }
}
