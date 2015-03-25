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
    class Menu
    {
        public enum GameStates
        {
            MainMenu,
            Game,
            Options,
            Levels,
            Quit
        }
        

        GameStates gameState = GameStates.MainMenu;

        List<Button> buttons = new List<Button>();

        public Menu()
        {
            buttons.Add(new Button(Vector2.Zero, "AYY LMAO", 1, Color.White, Color.AliceBlue, Color.Green));
        }

        public void Update()
        {
            if(buttons[0].Pressed(255))
            {
                // BLA BLA
            }
            switch (gameState)
            {
                case GameStates.MainMenu:
                    
                    break;

                case GameStates.Options:

                    break;

                case GameStates.Levels:

                    break;

                case GameStates.Game:

                    break;

                case GameStates.Quit:

                    break;
            }
        }


    }
}
