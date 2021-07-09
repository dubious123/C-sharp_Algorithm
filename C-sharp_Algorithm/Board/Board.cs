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
        public TileType[,] _tile;
        public int _ROW_SIZE, _COLUMN_SIZE;
        Random rand = new Random();
        public enum TileType
        {
            //벽 : RED
            //갈 수 있는 지역 : GREEN
            Empty,
            Wall
        }
        public void Initialize(int Row,int Column)
        {
            //이 미로생성 알고리즘은 사이즈가 홀수여야함
            if (Row % 2 == 0 || Column % 2 == 0)
            {
                return;
            }
            _tile = new TileType[Row, Column];
            _ROW_SIZE = Row;
            _COLUMN_SIZE = Column;
            GenerateBySideWinder();
        }

        private void GenerateBySideWinder()
        {

            {
                //길을 다 막아버리는 작업
                for (int y = 0; y < _ROW_SIZE; y++)
                {
                    for (int x = 0; x < _COLUMN_SIZE; x++)
                    {
                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            _tile[y, x] = TileType.Wall;
                        }
                        else
                        {
                            _tile[y, x] = TileType.Empty;
                        }
                    }
                }

                //랜덤으로 우측 혹은 아래로 길을 둟는 작업
                for (int y = 0; y < _ROW_SIZE; y++)
                {
                    int count = 1;
                    for (int x = 0; x < _COLUMN_SIZE; x++)
                    {
                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            continue;
                        }
                        if (y == _ROW_SIZE - 2)
                        {
                            if (x == _COLUMN_SIZE - 2)
                            {
                                continue;
                            }
                            _tile[y, x + 1] = TileType.Empty;
                            continue;
                        }
                        if (x == _COLUMN_SIZE - 2)
                        {
                            _tile[y + 1, x] = TileType.Empty;
                            continue;
                        }
                        if (rand.Next(2) == 0)
                        {
                            _tile[y, x + 1] = TileType.Empty;
                            count++;    
                        }
                        else
                        {
                            int randomindex = rand.Next(0,count);
                            _tile[y + 1, x - randomindex * 2] = TileType.Empty;
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
                for (int y = 0; y < _ROW_SIZE; y++)
                {
                    for (int x = 0; x < _COLUMN_SIZE; x++)
                    {
                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            _tile[y, x] = TileType.Wall;
                        }
                        else
                        {
                            _tile[y, x] = TileType.Empty;
                        }
                    }
                }

                //랜덤으로 우측 혹은 아래로 길을 둟는 작업
                for (int y = 0; y < _ROW_SIZE; y++)
                {
                    for (int x = 0; x < _COLUMN_SIZE; x++)
                    {

                        if (x % 2 == 0 || y % 2 == 0)
                        {
                            continue;
                        }
                        if (y == _ROW_SIZE - 2)
                        {
                            if(x != _COLUMN_SIZE - 2)
                            {
                                _tile[y, x + 1] = TileType.Empty;
                            }
                            continue;
                        }
                        intersectionCadinates.Add(x);
                        coinSide = rand.Next(2);
                        if (coinSide == 0 && x != _COLUMN_SIZE - 2)
                        {
                            _tile[y, x + 1] = TileType.Empty;
                            continue;
                        }
                        if (coinSide == 0 && x == _COLUMN_SIZE - 2)
                        {
                            tempindex = rand.Next(intersectionCadinates.Count);
                            _tile[y + 1, intersectionCadinates[tempindex]] = TileType.Empty;
                            intersectionCadinates.Clear();
                            continue;
                        }
                        else //coinside==1
                        {
                            if (y == _ROW_SIZE - 2 && x == _COLUMN_SIZE-2)
                            {
                                continue;
                            }
                            if (y == _ROW_SIZE - 2)
                            {
                                _tile[y, x + 1] = TileType.Empty;
                                continue;
                            }
                            tempindex = rand.Next(intersectionCadinates.Count);
                            _tile[y + 1, intersectionCadinates[tempindex]] = TileType.Empty;
                            intersectionCadinates.Clear();

                        }
                        //if (rand.Next(2) == 0)
                        //{
                        //    _tile[y, x + 1] = TileType.Empty;
                        //}
                        //else
                        //{
                        //    _tile[y + 1, x] = TileType.Empty;
                        //}
                    }
                }
            }
        }

        private void GenerateByBinaryTree()
        {
            //길을 다 막아버리는 작업
            for (int y = 0; y < _ROW_SIZE; y++)
            {
                for (int x = 0; x < _COLUMN_SIZE; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        _tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        _tile[y, x] = TileType.Empty;
                    }
                }
            }

            //랜덤으로 우측 혹은 아래로 길을 둟는 작업
            for (int y = 0; y < _ROW_SIZE; y++)
            {
                for (int x = 0; x < _COLUMN_SIZE; x++)
                {

                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        continue;
                    }

                    if (y == _ROW_SIZE - 2)
                    {
                        if (x == _COLUMN_SIZE - 2)
                        {
                            continue;
                        }
                        _tile[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == _COLUMN_SIZE - 2)
                    {
                        _tile[y + 1, x] = TileType.Empty;
                        continue;
                    }
                    if (rand.Next(2) == 0)
                    {
                        _tile[y, x + 1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y + 1, x] = TileType.Empty;
                    }
                }
            }
        }

        public void Render()
        {
            ConsoleColor DefaultColor = Console.ForegroundColor;
            Console.CursorVisible = false;
            for (int y = 0; y < _ROW_SIZE; y++)
            {
                for (int x = 0; x < _COLUMN_SIZE; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
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
