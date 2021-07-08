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

        public enum TileType
        {
            //벽 : RED
            //갈 수 있는 지역 : GREEN
            Empty,
            Wall
        }
        public void Initialize(int Row,int Column)
        {
            
            _tile = new TileType[Row, Column];
            _ROW_SIZE = Row;
            _COLUMN_SIZE = Column;
            for(int y = 0; y < _ROW_SIZE; y++)
            {
                for(int x = 0; x < _COLUMN_SIZE; x++)
                {
                    if (x == 0 || x == _COLUMN_SIZE - 1|| y == 0 || y == _ROW_SIZE - 1)
                    {
                        _tile[y, x] = TileType.Wall;
                    }
                    else
                    {
                        _tile[y, x] = TileType.Empty;
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
