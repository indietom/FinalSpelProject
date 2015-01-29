using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalSpelProject
{
    struct ComboCounter
    {
        Vector2 pos;
        Color color;

        float size;
        float barLength;

        short currentCombo;

        public ComboCounter(Vector2 pos2)
        {
            pos = pos2;
            size = 0;
            barLength = 0;
            currentCombo = 0;
            color = Color.White;
        }

        public void Update(List<Player> players)
        {
            foreach (Player p in players)
            {
                color = new Color(p.GetCurrentCombo() * 50, 0, 0);
                size = 1 + p.GetCurrentCombo() * 0.01f;
                currentCombo = p.GetCurrentCombo();
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D spritesheet)
        {
            if(color.R > 0) spriteBatch.DrawString(font, "COMBO: " + currentCombo.ToString(), pos, color, 0, new Vector2(0, 0), size, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(spritesheet, new Rectangle((int)pos.X, (int)pos.Y, (int)barLength, 8), new Rectangle(430, 1, 1, 8), Color.White);
        }
    }
}