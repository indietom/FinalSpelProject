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
    class StartScreen
    {
        KeyboardState keyboard;

        GamePadState gamePad;

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font)
        {
            // non-draw logic in the draw fuction; crazy
            keyboard = Keyboard.GetState();

            gamePad = GamePad.GetState(PlayerIndex.One);

            if(keyboard.IsKeyDown(Keys.X) || gamePad.IsButtonDown(Buttons.A))
            {
                Globals.gameState = GameStates.Menu;
            }

            spriteBatch.Draw(spritesheet, new Vector2(89, 48), new Rectangle(196, 1496, 649, 64), Color.White);
        }
    }
}
