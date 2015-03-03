using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalSpelProject
{
    class LevelManager
    {
        public static byte currentLevel = 0;

        public void ResetLevel()
        {

        }

        public void StartLevel()
        {

        }
    }

    struct LevelProperty
    {
        byte tag;
        byte height;

        public LevelProperty(byte tag2, byte height)
        {

        }

        public byte GetTag() { return tag; }
        public byte GetHeigt() { return height; }
    }
}
