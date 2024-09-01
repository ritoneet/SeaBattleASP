namespace SeaBattleASP.Models
{
    public class Bot
    {
        public int[,] myMap;
        public int[,] enemyMap;

        public Bot(int[,] myMap, int[,] enemyMap)
        {
            this.myMap = myMap;
            this.enemyMap = enemyMap;
        }

        public bool IsInsideMap(int i, int j)
        {
            return i >= 0 && j >= 0 && i < SeaBattle.mapSize && j < SeaBattle.mapSize;
        }

        public bool IsEmpty(int i, int j, int length)
        {
            for (int k = j; k < j + length; k++)
            {
                if (myMap[i, k] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int[,] ConfigureShips()
        {
            int lengthShip = 4;
            int cycleValue = 4;
            int shipsCount = 10;
            Random r = new Random();

            while (shipsCount > 0)
            {
                for (int i = 0; i < cycleValue / 4; i++)
                {
                    int posX = r.Next(1, SeaBattle.mapSize);
                    int posY = r.Next(1, SeaBattle.mapSize);

                    while (!IsInsideMap(posX, posY + lengthShip - 1) || !IsEmpty(posX, posY, lengthShip))
                    {
                        posX = r.Next(1, SeaBattle.mapSize);
                        posY = r.Next(1, SeaBattle.mapSize);
                    }

                    for (int k = posY; k < posY + lengthShip; k++)
                    {
                        myMap[posX, k] = 1;
                    }

                    shipsCount--;
                    if (shipsCount <= 0)
                        break;
                }
                cycleValue += 4;
                lengthShip--;
            }
            return myMap;
        }

        public bool Shoot()
        {
            bool hit = false;
            Random r = new Random();

            int posX, posY;
            do
            {
                posX = r.Next(1, SeaBattle.mapSize);
                posY = r.Next(1, SeaBattle.mapSize);
            }
            while (enemyMap[posX, posY] == -1 || enemyMap[posX, posY] == -2); // -1 if miss, -2 if hit

            if (enemyMap[posX, posY] != 0) // 0 empty cell
            {
                hit = true;
                enemyMap[posX, posY] = -2; // -2 if hit
            }
            else
            {
                enemyMap[posX, posY] = -1; // -1 if miss
            }

            return hit;
        }
    }
}

