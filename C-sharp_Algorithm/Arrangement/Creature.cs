using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creature
{
    public class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }
    public class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Board.Board _board;
        Random _random = new();
        enum Direction
        {
            Up, Left, Down, Right
        }
        int _dir = (int)Direction.Up;
        List<Pos> _points = new();
        public void Initialize(int posY, int posX, Board.Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;
            //RightHand();
            BFS();
        }
        void BFS()
        {
            int[] deltaY = new int[] { -1, 0, 1, 0 };
            int[] deltaX = new int[] { 0, -1, 0, 1 };
            bool[,] found = new bool[_board.RowSize, _board.ColumnSize];
            Pos[,] parent = new Pos[_board.RowSize, _board.ColumnSize];
            parent[PosY, PosX] = new Pos(PosY,PosX);
            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosY, PosX));
            found[PosY, PosX] = true;

            while (q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;
                for(int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];
                    if (nextY < 0 || nextY >= _board.ColumnSize || nextX < 0 || nextX >= _board.RowSize) { continue; }
                    if (_board.Tile[nextY, nextX] == Board.Board.TileType.Wall || found[nextY, nextX]) { continue; }
                    q.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }

            }
            int y = _board.DestY;
            int x = _board.DestX;
            //부모좌표가 나와 같으면 끝
            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));
                y = parent[y, x].Y;
                x = parent[y, x].X;
            }
            _points.Add(new Pos(y, x)); // 처음 점

            _points.Reverse();

        }
        void RightHand()
        {
            // 현재 바라보고 있는 방향을 기준으로 좌표 변화를 나타낸다.
            int[] frontY = new int[4] { -1, 0, 1, 0 };
            int[] frontX = new int[4] { 0, -1, 0, 1 };
            int[] rightY = new int[4] { 0, -1, 0, 1 };
            int[] rightX = new int[4] { 1, 0, -1, 0 };
            _points.Add(new Pos(PosY, PosX));
            //오른손 법칙 알고리즘
            //목적지 도착 전까지는 계속 실행
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                //1. 현재 바라보는 방향을 기준으로 계속 오른쪽으로 살 수 있는지 확인
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.Board.TileType.Empty)
                {
                    //오른쪽 방향으로 90도 회전
                    _dir = (_dir + 3) % 4;
                    //앞으로 한 보 전진
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));

                }
                //2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.Board.TileType.Empty)
                {
                    //앞으로 한 보 전진
                    PosY += frontY[_dir];
                    PosX += frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));

                }
                else
                {
                    //왼쪽 방향으로 90도 회전
                    _dir = (_dir + 5) % 4;
                }
            }
        }
        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastindex = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_lastindex >= _points.Count) { return; }
            if (_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastindex].Y;
                PosX = _points[_lastindex].X;
                _lastindex++;
            }

        }
    }
}
