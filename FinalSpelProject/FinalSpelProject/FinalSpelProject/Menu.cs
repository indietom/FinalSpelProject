using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalSpelProject
{
    enum MenuStates { Main, Options, LevelSelect, HighScoreScreen }
    class Menu
    {
        const byte start = 0,
            levelSelect = 1,
            highScoreScreen = 2,
            quit = 3;

        List<Button> mainButtons = new List<Button>();
        List<Button> optionsButtons = new List<Button>();
        List<Button> levelSelectButtons = new List<Button>();

        MenuStates menuState;

        byte currentOption;

        string startButtonText()
        {
            return (Globals.startedGame) ? "Resume" : "Start";
        }

        public Menu()
        {
            mainButtons.Add(new Button(new Vector2(Globals.screenW / 2, Globals.screenH / 2), "", start, Color.White, Color.Green, Color.Gray));
        }

        public void Update()
        {
            switch(menuState)
            {

            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            switch(menuState)
            {
                case MenuStates.Main:
                    mainButtons[start].Draw(spriteBatch, font, currentOption, startButtonText());
                    break;
            }
        }
    }
}
