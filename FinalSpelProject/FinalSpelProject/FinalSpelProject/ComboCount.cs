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

        public ComboCounter(Vector2 pos2)
        {
            pos = pos2;
            size = 0;
            color = Color.White;
        }

        public void Update(List<Player> players)
        {
            foreach (Player p in players)
            {
                color = new Color(p.GetCurrentCombo() * 5, p.GetCurrentCombo() * 2, 0);
                size = 1 + p.GetCurrentCombo() * 0.01f;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "COMBO: ", pos, color, 0, new Vector2(0, 0), size, SpriteEffects.None, 1.0f);
        }
    }
}