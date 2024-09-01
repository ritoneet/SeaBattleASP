using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SeaBattleASP.Models
{
    public class SeaBattle
    {
        public const int mapSize = 10;
        public int cellSize = 30;
        public string alphabet = "ABCDEFGHIJ";
        public int[,] myMap = new int[mapSize, mapSize];
        public int[,] enemyMap = new int[mapSize, mapSize];
        public Dictionary<int, int> save = new Dictionary<int, int>();
        public bool isPlaying = false;

        public Bot bot;

        public SeaBattle()
        {
            Init();
        }

        public void Init()
        {
            isPlaying = false;
            bot = new Bot(enemyMap, myMap);
            enemyMap = bot.ConfigureShips();
        }

        public void ConfigureShips(int x, int y)
        {
            if (!isPlaying && CanPlaceShip(x, y))
            {

                if (myMap[x, y] == 0)
                { 
                    myMap[x, y] = 1;
                    save[myMap[x, y]] = 1;
                }
                else
                {
                    myMap[x, y] = 0;
                    //save.Remove();
                }
            }
        }
        private bool CanPlaceShip(int x, int y)
        {
            if (myMap[x, y] != 0) return false;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = x + i;
                    int newY = y + j;

                    if (newX >= 0 && newX < SeaBattle.mapSize && newY >= 0 && newY < SeaBattle.mapSize)
                    {
                        if (myMap[newX, newY] != 0) return false; 
                    }
                }
            }

            return true;
        }

        public bool PlayerShoot(int x, int y)
        {
            bool hit = false;
            if (isPlaying)
            {
                if (enemyMap[y, x] != 0)
                {
                    hit = true;
                    enemyMap[y, x] = 0;
                }
            }
            return hit;
        }

        public bool CheckIfMapIsNotEmpty()
        {
            for (int i = 1; i < mapSize; i++)
            {
                for (int j = 1; j < mapSize; j++)
                {
                    if (myMap[i, j] != 0 || enemyMap[i, j] != 0)
                        return true;
                }
            }
            return false;
        }
    }
}
