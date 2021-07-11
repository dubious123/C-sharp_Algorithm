using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Right_hand_Algorighm
{
    class Board
    {
        const char CIRCLE = '\u25cf';
        static public int ExitY { get; private set; }
        static public int ExitX { get; private set; }
        public class Tile
        {
            public TileType _tileType;
            public ConsoleColor _tileColor;
            public Tile(TileType tileType,ConsoleColor tileColor)
            {
                _tileType = tileType;
                _tileColor = tileColor;
            }
            public void setTile(TileType tileType, ConsoleColor tileColor)
            {
                _tileType = tileType;
                _tileColor = tileColor;
            }



        }
       
        public enum TileType
        {
            wall,empty
        }
        public Tile[,] _Tiles { get; private set; }

        public Board(int rOW_SIZE, int cOLUMN_SIZE)
        {
            Initialize(rOW_SIZE, cOLUMN_SIZE);
        }
        public void Initialize(int rOW_SIZE, int cOLUMN_SIZE)
        {
            ROW_SIZE = rOW_SIZE;
            COLUMN_SIZE = cOLUMN_SIZE;
            ExitY = ROW_SIZE - 2;
            ExitX = COLUMN_SIZE - 2;
            _Tiles = new Tile[ROW_SIZE, COLUMN_SIZE];
            if (ROW_SIZE % 2 == 0 || COLUMN_SIZE % 2 == 0)
            {
                Console.WriteLine("잘못된 보드 크기");
                return;
            }
            for (int x = 0; x < COLUMN_SIZE; x++)
            {
                for (int y = 0; y < ROW_SIZE; y++)
                {
                    if (x % 2 + y % 2 == 2) { _Tiles[y, x] = new Tile(TileType.empty, ConsoleColor.Green); }
                    else { _Tiles[y, x] = new Tile(TileType.wall, ConsoleColor.Red); }

                }
            }
        }
        protected int ROW_SIZE { get; private set; }
        protected int COLUMN_SIZE { get; private set; }

        public bool FrameControl(int _frame,int _current_tick)
        {
            if (System.Environment.TickCount-_current_tick>_frame) { return true; }
            return false;
        }

        public void Render(Player player, int _current_tick)
        {
            ConsoleColor _default_color = Console.ForegroundColor;
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            player.PlayerUpdate(System.Environment.TickCount - _current_tick);
            for (int y = 0; y < ROW_SIZE; y++)
            {
                for (int x = 0; x < COLUMN_SIZE; x++)
                {
                    if(player.PosY == y&&player.PosX == x) { Console.ForegroundColor = player.PlayerColor; }
                    else if (y == ExitY && x == ExitX) { Console.ForegroundColor = ConsoleColor.Yellow; }
                    else { Console.ForegroundColor = _Tiles[y, x]._tileColor; }
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = _default_color;
        }

        internal void CreateMazeBySideWinder()
        {
            Random rand = new Random();
            int count = 1;
            for(int y = 0; y < ROW_SIZE - 1; y++)
            {
                for(int x = 0; x < COLUMN_SIZE - 1; x++)
                {
                    if (x % 2 + y % 2 == 2)
                    {
                        if (x == COLUMN_SIZE - 2 && y == ROW_SIZE - 2) { continue; }
                        else if (y == ROW_SIZE - 2)
                        {
                            _Tiles[y, x + 1].setTile(TileType.empty, ConsoleColor.Green);
                        }
                        else if (x == COLUMN_SIZE - 2)
                        {
                            _Tiles[y + 1, x - rand.Next(count) * 2].setTile(TileType.empty, ConsoleColor.Green);
                            count = 1;
                        }
                        else if (rand.Next(2) == 0) 
                        { 
                            _Tiles[y, x+1].setTile(TileType.empty, ConsoleColor.Green);
                            count++;
                        }
                        else 
                        { 
                            _Tiles[y + 1, x - rand.Next(count) * 2].setTile(TileType.empty, ConsoleColor.Green);
                            count = 1;
                        }
                    }
                }
            }


        }
    }
}
