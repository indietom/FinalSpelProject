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

        string startButtonText()
        {
            return (Globals.startedGame) ? "Resume" : "Start";
        }

        public Menu()
        {
            
        }

        public void Update()
        {
            
        }

        public void Draw()
        {

        }
    }
}
