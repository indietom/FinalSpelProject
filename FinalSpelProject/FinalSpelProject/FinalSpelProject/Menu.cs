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
        List<Button> buttons = new List<Button>();

        MenuStates menuState;

        string startButtonText()
        {
            return "";
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
