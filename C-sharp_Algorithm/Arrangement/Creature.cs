using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creature
{
    public class Pos
    {
        public Pos(int y, int x) { Y = y;X = x; }
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
            Up,Left,Down,Right
        }
        int _dir = (int)Direction.Up;
        List<Pos> _points = new();
        public void Initialize(int posY,int posX, Board.Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;

            // 현재 바라보고 있는 방향을 기준으로 좌표 변화를 나타낸다.
            int[] frontY = new int[4] { -1, 0, 1, 0 };
            int[] frontX = new int[4] { 0, -1, 0, 1 };
            int[] rightY = new int[4] { 0, -1, 0, 1 };
            int[] rightX = new int[4] { 1, 0, -1, 0 };
            _points.Add(new Pos(posY, posX));
            //오른손 법칙 알고리즘
            //목적지 도착 전까지는 계속 실행
            while (PosY != board.DestY || PosX != board.DestX)
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
                else if (_board.Tile[PosY + frontY[_dir], PosX+ frontX[_dir]]==Board.Board.TileType.Empty)
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
            if(_lastindex >= _points.Count) { return; }
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
