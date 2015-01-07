using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FinalSpelProject
{
    class FileManager
    {
        public string loadLine(int line, string path)
        {
            string returnValue = "";
            StreamReader sr = new StreamReader(path);
            for (int i = 0; i < 1000; i++)
                if (i == line) returnValue = sr.ReadLine();
            sr.Close();
            return returnValue;
        }
        public void saveLine(int line, string path, string text, bool overwrite)
        {
            StreamWriter sw = new StreamWriter(path, overwrite);
            for (int i = 0; i < 1000; i++)
                if (i == line) sw.WriteLine(text); 
            sw.Close();
        }
    }
}
