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
        static string replaceIndex(short index, char letter, string strMod)
        {
            string str = "";

            for (int i = 0; i < strMod.Length; i++)
            {
                if (i != index)
                    str += strMod[i];
                else
                    str += letter;
            }
            return str;
        }
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

            int chunkHeight = 0;
            int amountOfChunks = 0;

            byte levelTag = 0;

            string orgMapPath = "";
            string[] chunksToSavePath;
            string[] chunksInfo;

            Console.Write("LEVEL TAG: ");
            levelTag = byte.Parse(Console.ReadLine());

            while(running)
            {
                Console.Write("MAP FILE NAME: ");
                orgMapPath = Console.ReadLine() + ".txt";

                Console.Write("AMOUNT OF CHUNKS: ");
                amountOfChunks = int.Parse(Console.ReadLine());
                chunksInfo = new string[amountOfChunks];
                Console.Write("CHUNCK HEIGHT: ");
                chunkHeight = int.Parse(Console.ReadLine());

                DirectoryInfo dirInfo = new DirectoryInfo(@"level"+levelTag.ToString());

                if (!dirInfo.Exists)
                    dirInfo.Create();

                for (int i = 0; i < amountOfChunks; i++)
                {
                    for (int j = 0; j < chunkHeight; j++)
                    {
                        chunksInfo[i] += LoadLine(j + (i * chunkHeight), orgMapPath) + Environment.NewLine;
                    }
                }
                
                for (int i = 0; i < chunksInfo.Count(); i++)
                {
                    SaveLine("level" + levelTag + "\\chunk" + i + ".txt", chunksInfo[i], true);
                }
                
                Console.ReadLine();
            }
        }
    }
}
