using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FinalSpelProject
{
    class FileManager
    {
        public List<HighScore> HighScores = new List<HighScore>();
        const byte LIST_SIZE = 5;

        public int[,] LoadLevel(string name)
        {
            int[,] map;
            string mapData = name + ".txt";
            int width = 0;
            int height = File.ReadLines(mapData).Count();

            StreamReader sReader = new StreamReader(mapData);
            string line = sReader.ReadLine();
            string[] tileNo;
            tileNo = line.Split(',');

            width = tileNo.Count();

            map = new int[height, width];

            sReader = new StreamReader(mapData);

            for (int y = 0; y < height; y++)
            {
                line = sReader.ReadLine();
                tileNo = line.Split(',');

                for (int x = 0; x < width; x++)
                {
                    if (tileNo[x] != "" || tileNo[x] != " ")
                    {
                        //Console.WriteLine(name);
                        map[y, x] = Convert.ToInt32(tileNo[x]);
                    }
                }
            }
            sReader.Close();

            return map;
        }

        public void LoadPlayer(string path, List<Player> players)
        {
            byte[] byteData = new byte[5];

            StreamReader sr = new StreamReader(path);
            for (int i = 0; i < byteData.Count(); i++)
            {
                byteData[i] = byte.Parse(sr.ReadLine());
            }
            // if there's anything this game needs it's more unused code.
            //players[0].SetGunType(byteData[0], false);
            //players[0].SetGunType(byteData[1], true);
            //players[0].SetSpecialAmmo(byteData[2]);
            //players[0].SetLives(byteData[3]);
            players[0].SetLevelsCompleted(byteData[4]);
            sr.Dispose();
        }

        public void SavePlayer(string path, List<Player> players)
        {
            byte[] byteData = new byte[5];
            byteData[0] = players[0].GetGunType();
            byteData[1] = players[0].GetSpecialGunType();
            byteData[2] = players[0].GetSpecialAmmo();
            byteData[3] = players[0].GetLives();
            byteData[4] = players[0].GetLevelsCompleted();

            StreamWriter sw = new StreamWriter(path);
            for (int i = 0; i < byteData.Count(); i++)
            {
                sw.WriteLine(byteData[i]);
            }
            sw.WriteLine(LevelManager.currentLevel);
            sw.Dispose();
        }

        public void LoadConfig()
        {
            StreamReader sr = new StreamReader("config.cfg");
            Globals.currentMoveSet = byte.Parse(sr.ReadLine());
            Globals.currentShootSet = byte.Parse(sr.ReadLine());
            sr.Dispose();
        }
        
        public void SaveConfig()
        {
            StreamWriter sw = new StreamWriter("config.cfg");
            sw.WriteLine(Globals.currentMoveSet);
            sw.WriteLine(Globals.currentShootSet);
            sw.Dispose();
        }

        // NAME[space]SCORE should be the format for the file
        // This is really unsafe, but as long as no one touches the files we should be fine
        public List<HighScore> LoadHighScores(string path)
        {
            HighScore[] highScores = new HighScore[LIST_SIZE];

            string[] currentLine = new string[2];

            StreamReader sr = new StreamReader(path);

            for (int i = 0; i < LIST_SIZE; i++)
            {
                currentLine = sr.ReadLine().Split(' ');
                highScores[i] = new HighScore(currentLine[0], int.Parse(currentLine[1]));
            }

            sr.Dispose();

            List<HighScore> tmpList = new List<HighScore>();
            
            for (int i = LIST_SIZE - 1; i > 0; i--)
                tmpList.Add(highScores[i]);

            return tmpList;
        }

        public void SaveHighScores(string path)
        {
            HighScores.Sort();
            StreamWriter sw = new StreamWriter(path);

            for (int i = 0; i < LIST_SIZE; i++ )
            {
                HighScores.Sort();
                sw.WriteLine(HighScores[i].ToString());
            }

            sw.Dispose();
        }
    }

    class HighScore
    {
        string name;
        public string GetName() { return name; }
        
        int score;
        public int GetScore() { return score; }

        public HighScore(string name2, int score2)
        {
            name = name2;
            score = score2;
        }

        public override string ToString()
        {
            return name + " " + score;
        }
    }
}
