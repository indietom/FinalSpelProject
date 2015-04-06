using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalSpelProject
{
    class LevelTransitionScreen
    {
        short delay;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public void Update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if(delay < 10) delay += 1;

            if(delay >= 10 && keyboard.IsKeyDown(Keys.X) && prevKeyboard.IsKeyUp(Keys.X))
            {
                Globals.gameState = GameStates.Game;
                delay = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D background)
        {
            spriteBatch.DrawString(font, "MISSION " + Globals.GetLevelName((byte)(LevelManager.currentLevel - 1)) + ": COMPLETED", new Vector2(280, 100), Color.Yellow);
            
            spriteBatch.DrawString(font, "PRESS X TO GO TO THE NEXT LEVEL", new Vector2(280, 400), Color.White, 0, new Vector2(0, 0), 0.9f, SpriteEffects.None, 1.0f);
        }
    }
}
