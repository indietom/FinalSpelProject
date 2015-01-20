using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChunckSeperator
{
    class Program
    {
        static public string LoadLine(int line, string path)
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
        static public void SaveLine(int line, string path, string text, bool overwrite)
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
        static void Main(string[] args)
        {
            bool running = true;

            byte chunkHeight = 0;
            byte amountOfChunks = 0;

            byte levelTag = 1;

            string orgMapPath = "";
            string[] chunksToSavePath;
            string[] chunksInfo;
            string loadedChunk = "";

            while(running)
            {
               // There used to be spagetti here, but it's gone now
            }
        }
    }
}
