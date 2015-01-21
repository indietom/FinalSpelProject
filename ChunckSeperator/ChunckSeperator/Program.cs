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
            for (int i = 0; i < 1000; i++)
            {
                sr.ReadLine();
                if (i == line)
                {
                    returnValue = sr.ReadLine();
                }
            }
            sr.Dispose();
            return returnValue;
        }
        static public void SaveLine(int line, string path, string text, bool overwrite)
        {
            StreamWriter sw = new StreamWriter(path, overwrite);
            for (int i = 0; i < 1000; i++)
            {
                sw.WriteLine(" ");
                if (i == line)
                {
                    sw.WriteLine(text);
                    break;
                }
            }
            sw.Dispose();
        }
        static public void SaveLine(string path, string text, bool overwrite)
        {
            StreamWriter sw = new StreamWriter(path, overwrite);
            sw.WriteLine(text);
            sw.Dispose();
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

            while(running)
            {
                chunksInfo = new string[5];
               
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        chunksInfo[i] += LoadLine(j + (i * 15), "map.txt") + "\n";
                        //Console.WriteLine(chunksInfo[i]);
                        //SaveLine("assåwat" + i +".txt", chunksInfo[i], true);
                    }
                }
                
                for (int i = 0; i < chunksInfo.Count(); i++)
                {
                    Console.WriteLine(chunksInfo[i]);
                    SaveLine("assåwat" + i + ".txt", chunksInfo[i], true);
                    //SaveLine(1, "assåwat.txt", chunksInfo[0], false);
                }
                
                Console.ReadLine();
            }
        }
    }
}
