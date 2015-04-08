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

        const byte move = 0,
            shoot = 1,
            back = 2;

        List<Button> mainButtons = new List<Button>();
        List<Button> optionsButtons = new List<Button>();
        List<Button> levelSelectButtons = new List<Button>();

        MenuStates menuState = MenuStates.Main;

        byte currentOption;
        byte maxOption;
        byte delay;

        public byte GetDelay() { return delay; }

        string startButtonText()
        {
            return (Globals.startedGame) ? "RESUME" : "START";
        }

        KeyboardState keyboard;
        KeyboardState prevKeyboard;

        FileManager fileManager = new FileManager();

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
            levelSelectButtons.Add(new Button(new Vector2(300, 200 + 5 * 30), "BACK", 4, Color.White, Color.Green, Color.Gray));

            optionsButtons.Add(new Button(new Vector2(100, 200), "", move, Color.White, Color.Green, Color.Gray, 0.7f));
            optionsButtons.Add(new Button(new Vector2(100, 230), "", shoot, Color.White, Color.Green, Color.Gray, 0.7f));

            optionsButtons.Add(new Button(new Vector2(100, 290), "BACK", back, Color.White, Color.Green, Color.Gray));
        }

        // ayy lmao, I'm so sorry
        public void Update(LevelManager levelManager, List<Chunk> chunks, List<Enemy> enemies, List<Projectile> projectiles, List<Player> player, Level level, List<Tile> tiles)
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

            if(delay <= 0 && keyboard.IsKeyDown(Keys.Escape) && prevKeyboard.IsKeyUp(Keys.Escape) && Globals.startedGame)
            {
                menuState = MenuStates.Main;
                currentOption = 0;
                Globals.gameState = GameStates.Game;
                delay = 1;
            }

            if (delay >= 1) delay += 1;
            delay = (delay >= 8) ? (byte)0 : delay;

            switch(menuState)
            {
                case MenuStates.Main:
                    maxOption = quit;
                    foreach(Button mb in mainButtons)
                    {
                        if(mb.Pressed(currentOption) && delay <= 0)
                        {
                            switch(mb.GetTag())
                            {
                                case start:
                                    Globals.gameState = GameStates.Game;
                                    Globals.startedGame = true;
                                    break;
                                case levelSelect:
                                    menuState = MenuStates.LevelSelect;
                                    currentOption = 0;
                                    break;
                                case options:
                                    menuState = MenuStates.Options;
                                    currentOption = 0;
                                    break;
                                case quit:
                                    Environment.Exit(0);
                                    break;
                            }
                            delay = 1;
                        }
                    }
                    break;
                case MenuStates.LevelSelect:
                    maxOption = 4;
                    foreach(Button lb in levelSelectButtons)
                    {
                        if (lb.GetTag() == 4 && lb.Pressed(currentOption) && delay <= 0)
                        {
                            menuState = MenuStates.Main;
                            currentOption = levelSelect;
                            delay = 1;
                        }
                        if (lb.GetTag() <= 3)
                        {
                            if (!lb.unavalible && lb.Pressed(currentOption) && delay <= 0)
                            {
                                Globals.gameState = GameStates.Game;
                                levelManager.StartLevel(lb.GetTag(), chunks, enemies, projectiles, player, ref level, tiles);
                                player[0] = new Player();
                                menuState = MenuStates.Main;
                                currentOption = 0;
                                delay = 1;
                            }
                            if (player[0].GetLevelsCompleted() <= lb.GetTag())
                            {
                                lb.unavalible = true;
                            }
                            else lb.unavalible = false;
                        }
                    }
                    break;
                case MenuStates.Options:
                    maxOption = back;
                    foreach(Button ob in optionsButtons)
                    {
                        if (ob.GetTag() == back && ob.Pressed(currentOption) && delay <= 0)
                        {
                            menuState = MenuStates.Main;
                            currentOption = options;
                            delay = 1;
                        }
                        if(ob.Pressed(currentOption) && ob.GetTag() == move && delay <= 0)
                        {
                            if (Globals.currentMoveSet == 0)
                                Globals.currentMoveSet = 1;
                            else
                                Globals.currentMoveSet = 0;
                            delay = 1;
                            player[0].AssignKeys();
                            fileManager.SaveConfig();
                        }
                        if (ob.Pressed(currentOption) && ob.GetTag() == shoot && delay <= 0)
                        {
                            if (Globals.currentShootSet == 0)
                                Globals.currentShootSet = 1;
                            else
                                Globals.currentShootSet = 0;
                            delay = 1;
                            player[0].AssignKeys();
                            fileManager.SaveConfig();
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
                case MenuStates.Options:
                    optionsButtons[move].Draw(spriteBatch, font, currentOption, Globals.OptionTextMovment());
                    optionsButtons[shoot].Draw(spriteBatch, font, currentOption, Globals.OptionTextShoot());
                    optionsButtons[back].Draw(spriteBatch, font, currentOption);
                    break;
            }
        }
    }
}
