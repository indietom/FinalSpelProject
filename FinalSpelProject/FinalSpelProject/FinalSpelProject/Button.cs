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

        Color selectedColor;
        Color unavalibeColor;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Button(Vector2 pos2, string text2, byte tag2, Color color2, Color selectedColor2, Color unavalibeColor2)
        {
            color = color2;
            OrginalColor = color;
            selectedColor = selectedColor2;
            unavalibeColor = unavalibeColor2;
            Pos = pos2;
            text = text2;
            tag = tag2;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, byte currentTag)
        {
            if (unavalible) color = unavalibeColor;
            if (currentTag == tag) color = selectedColor;
            if (!unavalible && currentTag != tag) color = OrginalColor;

            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            spriteBatch.DrawString(font, text, Pos, color);
        }

        public bool Pressed(byte currentTag)
        {
            return (keyboard.IsKeyDown(Keys.X) && prevKeyboard.IsKeyUp(Keys.X) && currentTag == tag);
        }
    }
}
