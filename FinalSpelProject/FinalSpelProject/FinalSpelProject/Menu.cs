using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalSpelProject
{
    enum MenuStates { Main, LevelSelect, HighScoreScreen, Options }
    class Menu
    {
        const byte start = 0,
            levelSelect = 1,
            highScoreScreen = 2,
            options = 3,
            quit = 4;

        List<Button> mainButtons = new List<Button>();
        List<Button> optionsButtons = new List<Button>();
        List<Button> levelSelectButtons = new List<Button>();

        MenuStates menuState;

        byte currentOption;
        byte maxOption;

        string startButtonText()
        {
            return (Globals.startedGame) ? "RESUME" : "START";
        }

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        public Menu()
        {
            mainButtons.Add(new Button(new Vector2(350, 200), "", start, Color.White, Color.Green, Color.Gray));
            mainButtons.Add(new Button(new Vector2(350, 230), "LEVEL SELECT", levelSelect, Color.White, Color.Green, Color.Gray));
            mainButtons.Add(new Button(new Vector2(350, 260), "HIGHSCORES", highScoreScreen, Color.White, Color.Green, Color.Gray));
            mainButtons.Add(new Button(new Vector2(350, 290), "OPTIONS", options, Color.White, Color.Green, Color.Gray));
            mainButtons.Add(new Button(new Vector2(350, 320), "QUIT", quit, Color.White, Color.Green, Color.Gray));
        }

        public void Update()
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(Keys.Down) && prevKeyboard.IsKeyUp(Keys.Down))
            {
                currentOption += 1;
                if (currentOption > maxOption)
                    currentOption = 0;
            }

            if (keyboard.IsKeyDown(Keys.Up) && prevKeyboard.IsKeyUp(Keys.Up))
            {
                if(currentOption != 0) currentOption -= 1;
                else currentOption = maxOption;   
            }

            switch(menuState)
            {
                case MenuStates.Main:
                    maxOption = quit;
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            switch(menuState)
            {
                case MenuStates.Main:
                    mainButtons[start].Draw(spriteBatch, font, currentOption, startButtonText());
                    mainButtons[levelSelect].Draw(spriteBatch, font, currentOption);
                    mainButtons[highScoreScreen].Draw(spriteBatch, font, currentOption);
                    mainButtons[options].Draw(spriteBatch, font, currentOption);
                    mainButtons[quit].Draw(spriteBatch, font, currentOption);
                    break;
            }
        }
    }
}
