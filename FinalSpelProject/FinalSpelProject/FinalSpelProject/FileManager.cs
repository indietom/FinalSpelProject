using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FinalSpelProject
{
    class FileManager
    {
        public int[,] LoadLevel(string name)
        {
            int[,] map;
            string mapData = name + ".txt";
            int width = 0;
            int height = File.ReadLines(mapData).Count();

            StreamReader sReader = new StreamReader(mapData);
            string line = sReader.ReadLine();
            string[] tileNo = line.Split(',');

            width = tileNo.Count();

            map = new int[height, width];
            sReader = new StreamReader(mapData);

            for (int y = 0; y < height; y++)
            {
                line = sReader.ReadLine();
                tileNo = line.Split(',');

                for (int x = 0; x < width; x++)
                {
                    map[y, x] = Convert.ToInt32(tileNo[x]);
                }
            }
            sReader.Close();

            return map;
        }
        public string LoadLine(int line, string path)
        {
            string returnValue = "";
            StreamReader sr = new StreamReader(path);
            for (int i = 0; i < File.ReadLines(path).Count(); i++)
                if (i == line)
                {
                    returnValue = sr.ReadLine();
                    break;
                }
            sr.Close();
            return returnValue;
        }
        public void SaveLine(int line, string path, string text, bool overwrite)
        {
            StreamWriter sw = new StreamWriter(path, overwrite);
            for (int i = 0; i < File.ReadLines(path).Count(); i++)
                if (i == line)
                {
                    sw.WriteLine(text);
                    break;
                }
            sw.Close();
        }
    }
}
