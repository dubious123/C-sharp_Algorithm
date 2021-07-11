
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    
    public class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] Tile { get; private set; }
        public int RowSize { get; private set; }
        public int ColumnSize { get; private set; }
        public int DestY { get; private set; }
        public int DestX { get; private set; }
        Random rand = new Random();

        public enum TileType
        {
            //벽 : RED
            //갈 수 있는 지역 : GREEN
            Empty,
            Wall
        }
        public void Initialize(int Row,int Column, Creature.Player player)
        {
            //이 미로생성 알고리즘은 사이즈가 홀수여야함
            if (Row % 2 == 0 || Column % 2 == 0)
            {
                return;
            }
            Tile = new TileType[Row, Column];
            RowSize = Row;
            ColumnSize = Column;
            DestY = RowSize - 2;
            DestX = ColumnSize - 2;
            GenerateBySideWinder();
        }

        private void GenerateBySideWinder()
        {

            {
                //길을 다 막아버리는 작업
                for (int y = 0; y < RowSize; y++)
                {
                    for (int x = 0; x < ColumnSize; x++)
                    {
                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            Tile[y, x] = TileType.Wall;
                        }
                        else
                        {
                            Tile[y, x] = TileType.Empty;
                        }
                    }
                }

                //랜덤으로 우측 혹은 아래로 길을 둟는 작업
                for (int y = 0; y < RowSize; y++)
                {
                    int count = 1;
                    for (int x = 0; x < ColumnSize; x++)
                    {
                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            continue;
                        }
                        if (y == RowSize - 2)
                        {
                            if (x == ColumnSize - 2)
                            {
                                continue;
                            }
                            Tile[y, x + 1] = TileType.Empty;
                            continue;
                        }
                        if (x == ColumnSize - 2)
                        {
                            Tile[y + 1, x] = TileType.Empty;
                            continue;
                        }
                        if (rand.Next(2) == 0)
                        {
                            Tile[y, x + 1] = TileType.Empty;
                            count++;    
                        }
                        else
                        {
                            int randomindex = rand.Next(0,count);
                            Tile[y + 1, x - randomindex * 2] = TileType.Empty;
                            count = 1;
                        }
                    }
                }
            }
        }

        private void GenerateBySideWinderMyVersion()
        {

            {
                List<int> intersectionCadinates = new List<int>();
                int tempindex;
                int coinSide;
                //길을 다 막아버리는 작업
                for (int y = 0; y < RowSize; y++)
                {
                    for (int x = 0; x < ColumnSize; x++)
                    {
                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            Tile[y, x] = TileType.Wall;
                        }
                        else
                        {
                            Tile[y, x] = TileType.Empty;
                        }
                    }
                }

                //랜덤으로 우측 혹은 아래로 길을 둟는 작업
                for (int y = 0; y < RowSize; y++)
                {
                    for (int x = 0; x < ColumnSize; x++)
                    {

                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            continue;
                        }
                        if (y == RowSize - 2)
                        {
                            if(x != ColumnSize - 2)
                            {
                                Tile[y, x + 1] = TileType.Empty;
                            }
                            continue;
                        }
                        intersectionCadinates.Add(x);
                        coinSide = rand.Next(2);
                        if (coinSide == 0 && x != ColumnSize - 2)
                        {
                            Tile[y, x + 1] = TileType.Empty;
                            continue;
                        }
                        if (coinSide == 0 && x == ColumnSize - 2)
                        {
                            tempindex = rand.Next(intersectionCadinates.Count);
                            Tile[y + 1, intersectionCadinates[tempindex]] = TileType.Empty;
                            intersectionCadinates.Clear();
                            continue;
                        }
                        else //coinside==1
                        {
                            if (y == RowSize - 2 && x == ColumnSize-2)
                            {
                                continue;
                            }
                            if (y == RowSize - 2)
                            {
                                Tile[y, x + 1] = TileType.Empty;
                                continue;
                            }
                            tempindex = rand.Next(intersectionCadinates.Count);
                            Tile[y + 1, intersectionCadinates[tempindex]] = TileType.Empty;
                            intersectionCadinates.Clear();

                        }
                        //if (rand.Next(2) == 0)
                        //{
                        //    Tile[y, x + 1] = TileType.Empty;
                        //}
                        //else
                        //{
                        //    Tile[y + 1, x] = TileType.Empty;
                        //}
                    }
                }
            }
        }

        private void GenerateByBinaryTree()
        {
            //길을 다 막아버리는 작업
            for (int y = 0; y < RowSize; y++)
            {
                for (int x = 0; x < ColumnSize; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        Tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        Tile[y, x] = TileType.Empty;
                    }
                }
            }

            //랜덤으로 우측 혹은 아래로 길을 둟는 작업
            for (int y = 0; y < RowSize; y++)
            {
                for (int x = 0; x < ColumnSize; x++)
                {

                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        continue;
                    }

                    if (y == RowSize - 2)
                    {
                        if (x == ColumnSize - 2)
                        {
                            continue;
                        }
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == ColumnSize - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    if (rand.Next(2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render(Creature.Player _player)
        {
            ConsoleColor DefaultColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            for (int y = 0; y < RowSize; y++)
            {
                for (int x = 0; x < ColumnSize; x++)
                {
                    //플레이어 좌표를 갖고와서, 그 좌표라면 player 전용 색상으로 표시
                    if(y==_player.PosY && x == _player.PosX) { Console.ForegroundColor = ConsoleColor.Blue; }
                    else if (y == DestY && x == DestY) { Console.ForegroundColor = ConsoleColor.Yellow; }
                    else { Console.ForegroundColor = GetTileColor(Tile[y, x]); }
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = DefaultColor;
        }
        public static ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Red;
            }
        }
        public bool FrameControl(int start_tick, int current_tick,int frame)
        {
            if (current_tick - start_tick <= frame)
            {
                return false;
            }
            return true;
        }
    }
}
