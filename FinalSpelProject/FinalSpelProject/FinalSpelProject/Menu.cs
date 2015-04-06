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

        MenuStates menuState = MenuStates.LevelSelect;

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

            for(int i = 0; i < 4; i++)
            {
                levelSelectButtons.Add(new Button(new Vector2(300, 200 + i * 30), Globals.GetLevelName((byte)i), (byte)i, Color.White, Color.Green, Color.Gray));
            }
        }

        // ayy lmao, I'm so sorry
        public void Update(LevelManager levelManager, List<Chunk> chunks, List<Enemy> enemies, List<Projectile> projectiles, List<Player> player, Level level)
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
                    foreach(Button mb in mainButtons)
                    {
                        if(mb.Pressed(currentOption))
                        {
                            switch(mb.GetTag())
                            {
                                case start:
                                    Globals.gameState = GameStates.Game;
                                    Globals.startedGame = true;
                                    break;
                                case quit:
                                    Environment.Exit(0);
                                    break;
                            }
                        }
                    }
                    break;
                case MenuStates.LevelSelect:
                    maxOption = 5;
                    foreach(Button lb in levelSelectButtons)
                    {
                        if (lb.GetTag() <= 4)
                        {
                            if(!lb.unavalible && lb.Pressed(currentOption))
                            {
                                Globals.gameState = GameStates.Game;
                                levelManager.StartLevel(lb.GetTag(), chunks, enemies, projectiles, player, level);
                            }
                            if (player[0].GetLevelsCompleted() <= lb.GetTag())
                            {
                                lb.unavalible = true;
                            }
                            else lb.unavalible = false;
                        }
                    }
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
                case MenuStates.LevelSelect:
                    spriteBatch.DrawString(font, "LEVELS-", new Vector2(300, 150), Color.Violet, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 1.0f);                    foreach(Button lb in levelSelectButtons)
                    {
                        lb.Draw(spriteBatch, font, currentOption);
                    }
                    break;
            }
        }
    }
}
