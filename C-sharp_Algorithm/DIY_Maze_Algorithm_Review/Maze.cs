using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Maze_Algorithm_Review
{
    public class Maze
    {
        public class _Board
        {
            internal Tile[,] Tiles = new Tile[MY_CONST.ROW_SIZE, MY_CONST.COLUMN_SIZE];
            public bool CanMove(_PlayerPos pos,E_Dir Dir)
            {
                return Tiles[Player.MovePos(pos,Dir).Y, Player.MovePos(pos,Dir).X]._TileType != E_TileType.Wall;
            }
        }

        public _Board Board;
        public void Initialize()
        {
            Console.CursorVisible = false;
            Board = new _Board();
            //초기 보드 생성, 홀수번째 좌표의 Tile을 TileType.Empty로 바꾼다.
            //각각의 좌표에 H값을 넣는다.
            for (int y = 0; y < MY_CONST.ROW_SIZE; y++)
            {
                for (int x = 0; x < MY_CONST.COLUMN_SIZE; x++)
                {
                    if (y * x % 2 == 0) //Wall
                    {
                        Board.Tiles[y, x] = new Tile(E_TileType.Wall, y, x);
                        continue;
                    }
                    else if (y == MY_CONST.ROW_SIZE - 1 && x == MY_CONST.COLUMN_SIZE - 1)
                    {
                        Board.Tiles[y, x] = new Tile(E_TileType.Exit, y, x);
                        continue;
                    }
                    Board.Tiles[y, x] = new Tile(E_TileType.Empty, y, x);
                }
            }
            //미로 생성 알고리즘 시작, SideWinder()
            GenerateBySideWinder();
        }

        private void GenerateBySideWinder()
        {
            Random rand = new();

            for (int y = 0; y < MY_CONST.ROW_SIZE / 2; y++)
            {
                int count = 0;
                for (int x = 0; x < MY_CONST.COLUMN_SIZE / 2; x++)
                {
                    int _Y = y * 2 + 1;
                    int _X = x * 2 + 1;
                    if(y== MY_CONST.ROW_SIZE / 2 - 1&& x == MY_CONST.COLUMN_SIZE / 2 - 1)
                    {
                        continue;
                    }
                    if (y == MY_CONST.ROW_SIZE / 2 - 1)
                    {
                        Board.Tiles[_Y, _X + 1].EditTile(E_TileType.Empty);
                        continue;
                    }
                    int RandInt = rand.Next(2);
                    //연속된 가로줄 개수
                    //오른쪽 채우기
                    if (RandInt == 0 && x != MY_CONST.COLUMN_SIZE / 2 - 1)
                    {
                        Board.Tiles[_Y, _X + 1].EditTile(E_TileType.Empty);
                        count++;
                        continue;
                    }
                    //아래 채우기
                    if (RandInt == 1 || x == MY_CONST.COLUMN_SIZE / 2 - 1)
                    {
                        count++;
                        Board.Tiles[_Y + 1, _X - rand.Next(count) * 2].EditTile(E_TileType.Empty);
                        count = 0;
                    }
                }
            }
        }

        void Render(Player player)
        {
            Console.SetCursorPosition(0, 0);
            ConsoleColor _DefaultColor = Console.ForegroundColor;
            //StringBuilder Output = new();
            for (int y = 0; y < MY_CONST.ROW_SIZE; y++)
            {
                for (int x = 0; x < MY_CONST.COLUMN_SIZE; x++)
                {
                    Console.ForegroundColor = Board.Tiles[y, x]._TileColor;
                    //현재 좌표가 player의 좌표라면 색을 바꾼다
                    if(player.CurrentPos.Y==y&& player.CurrentPos.X == x)
                    {
                        Console.ForegroundColor = player.PlayerColor;
                    }
                    Console.Write(MY_CONST.CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = _DefaultColor;
            //만약 현재 player의 현재 좌표가 목적지라면 모두 초기화한다.
            if (player.CurrentPos.Y == MY_CONST.EXIT_Y && player.CurrentPos.X == MY_CONST.EXIT_X)
            {
                Initialize();
                player.Initialize();
                MyLibrary.Astar(Board, player);
            }
        }
        internal void Run()
        {
            Initialize();
            Player Player = new();
            MyLibrary.Astar(Board, Player);
            int currentTick = 0;
            while (true)
            {
                if (MyLibrary.FrameControl(currentTick))
                {
                    //여기서부터 렌터링
                    Render(Player);
                    Player.update(System.Environment.TickCount-currentTick);
                    currentTick = System.Environment.TickCount;
                }
            }
        }
    }
}
