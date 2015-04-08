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
    class GameOverScreen
    {
        KeyboardState keyboard;

        GamePadState gamePad;

        string pressButtonToRestart;

        public void Draw(SpriteBatch spriteBatch, Texture2D spritesheet, SpriteFont font, Menu menu)
        {
            // Even more non-draw logic in draw functions; in before F in programming 
            keyboard = Keyboard.GetState();

            gamePad = GamePad.GetState(PlayerIndex.One);

            if (gamePad.IsConnected)
            {
                pressButtonToRestart = "PRESS A TO GO BACK TO THE MENU";
            }
            else
            {
                pressButtonToRestart = "PRESS X TO GO BACK TO THE MENU";
            }

            if (keyboard.IsKeyDown(Keys.X) || gamePad.IsButtonDown(Buttons.A))
            {
                Globals.gameState = GameStates.Menu;
                menu.SetDelay(1);
            }
        }
    }
}
