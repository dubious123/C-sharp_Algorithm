using PriorityQueue;
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
            //BFS();
            Astar();
        }
        struct PQnode : IComparable<PQnode>
        {
            public int F;
            public int G;
            public int Y;
            public int X;

            public int CompareTo(PQnode other)
            {
                if (F == other.F) { return 0; }
                return F < other.F ? 1 : -1;
            }
        }
        
        private void Astar()
        {
            // U L D R UL DL DR UR
            int[] deltaY = new int[] { -1, 0, 1, 0 , -1, 1, 1, -1};
            int[] deltaX = new int[] { 0, -1, 0, 1 , -1, -1, 1, 1};
            int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14};
            //점수 매기기
            //F = G + H
            //F = 최종 점수(작을 수록 좋음, 경로에 따라 달라짐)
            //G = 시작점에서 해당 좌표까지 이동해는데 드는 비용(작을 수록 좋음, 경로에 따라 달라짐)
            //H = 목적지에서 얼마나 가까운지(고정)

            //(y,x) 이미 방문했는지 여부 (qkdnas = closed 상태)
            bool[,] closed = new bool[_board.RowSize, _board.ColumnSize]; //ClosedList
            //(y,x) 가는 길을 한번이라도 발견했는지
            //발견X => MaxValue
            //발견O => F = G + H
            int[,] open = new int[_board.RowSize, _board.ColumnSize]; //OpneList
            for (int y = 0; y < _board.RowSize; y++)
                for (int x = 0; x < _board.ColumnSize; x++)
                    open[y, x] = Int32.MaxValue;
            Pos[,] parent = new Pos[_board.RowSize, _board.ColumnSize];

            //오픈리스트에 있는 정보들 중에서 가장 좋은 후보를 빠르게 뽑아오기 위한 도구
            PriorityQueue<PQnode> pq = new PriorityQueue<PQnode>();
            //시작점 발견(예약 진행)
            open[PosY, PosX] = (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX))*10;
            pq.Push(new PQnode() { F = (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX))*10, G = 0, Y = PosY, X = PosX }) ;
            parent[PosY, PosX] = new Pos(PosY, PosX);
            while (pq.Count>0)
            {
                //제일 좋은 후보를 찾는다.
                PQnode node = pq.Pop();
                //동일한 좌표를 여러 경로로 찾아서, 더 빠른 경로로 인해서 이미 방문(closed)된 경우 스킵
                if (closed[node.Y, node.X]) { continue; }
                //방문한다.
                closed[node.Y, node.X] = true;
                if (node.Y == _board.DestY && node.X == _board.DestX) { break; }

                //상하좌우등 이동할 수 있는 좌표인지 확인해서 예약(open)한다
                for (int i = 0; i < deltaY.Length; i++)
                {
                    int nextY = node.Y + deltaY[i];
                    int nextX = node.X + deltaX[i];
                    //유효범위 체크
                    if (nextY < 0 || nextY >= _board.RowSize || nextX < 0 || nextX >= _board.ColumnSize) { continue; }
                    //벽체크 + 방문한 곳 체크
                    if (_board.Tile[nextY, nextX] == Board.Board.TileType.Wall || closed[nextY, nextX]) { continue; }
                    //비용계산 
                    int g = node.G + cost[i];
                    int h = (Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX))*10;
                    //다른 경로에서 더 빠른 길 이미 찾았으면 스킵
                    if(open[nextY,nextX] < g + h) { continue; }
                    //가장 좋은 케이스, 예약
                    open[nextY, nextY] = g + h;
                    pq.Push(new PQnode() { F = g + h, G = g, Y = nextY, X = nextX });
                    parent[nextY, nextX] = new Pos(node.Y, node.X);
                }

            }
            CalcPathFromParent(parent);

        }



        public void CalcPathFromParent(Pos[,] parent)
        {
            int y = _board.DestY;
            int x = _board.DestX;
            //부모좌표가 나와 같으면 끝
            while (parent[y, x].Y != y || parent[y, x].X != x)
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;
            }
            _points.Add(new Pos(y, x)); // 처음 점

            _points.Reverse();
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

            CalcPathFromParent(parent);
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
        const int MOVE_TICK = 30;
        int _sumTick = 0;
        int _lastindex = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if (_lastindex >= _points.Count) 
            {
                _lastindex = 0;
                _points.Clear();
                _board.Initialize(_board.RowSize,_board.ColumnSize,this);
                Initialize(1, 1, _board);
            }
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
