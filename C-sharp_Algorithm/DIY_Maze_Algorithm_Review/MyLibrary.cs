
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Maze_Algorithm_Review
{
    public static class MyLibrary
    {
        public static int Costs(E_Dir Dir)
        {
            int[] cost = { 1, 1, 1, 1 };
            return cost[(int)Dir];
        }
        public static int Costs(int i)
        {
            int[] cost = { 1, 1, 1, 1 };
            return cost[i];
        }
        public static void Swap<T>(T a, T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        public static void Swap<T>(List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }
        public static bool FrameControl(int currentTick)
        {
            if(System.Environment.TickCount - currentTick < MY_CONST.FRAME) 
            { 
                return false; 
            }
            return true;

        }

        //input : board , T : Tile
        //ouput : playerPos , S : _Pos

        public static void Astar<T,S>(T[,] input, List<S> Output) where T : IComparable<T>
        {
            
        }



        struct PathInfo
        {
            public PathInfo(bool isClosed, bool isOpened, _PlayerPos parentPos)
            {
                this.isClosed = isClosed;
                this.isOpened = isOpened;
                this.parentPos = parentPos;
            }
            public bool isClosed; //true : 방문한적이 있기 때문에 갈 수 없음
            public bool isOpened; //true : 이미 F를 계산한 적이 있고 pq에 예약되었음
            public _PlayerPos parentPos;
        }


        public static void Astar(Maze._Board board,Player Player)
        {
            PathInfo[,] PathInfo = new PathInfo[MY_CONST.COLUMN_SIZE, MY_CONST.ROW_SIZE];
            PriorityQueue<Tile> Opened = new PriorityQueue<Tile>();

            Opened.Push(board.Tiles[MY_CONST.START_Y, MY_CONST.START_X]);
            Opened.Peek().G = 0; //시작지점의 이동비용은 0
            Opened.Peek().F = Opened.Peek().H;
            //시작지점의 부모좌표는 시작좌표
            PathInfo[MY_CONST.START_Y, MY_CONST.START_X] = new PathInfo(false, true, new _PlayerPos(MY_CONST.START_Y, MY_CONST.START_X));
            //더이상 갈 후보타일이 없을 때까지
            while (Opened.Count>0)
            {
                Tile nowTile = Opened.Pop();
                //현재좌표가 출구좌표랑 같다.
                if (nowTile.PosY == MY_CONST.EXIT_Y && nowTile.PosX == MY_CONST.EXIT_X)
                {
                    //이동할 때 언제나 현재 갈 수 있는 최단의 경로만을 택하였다.
                    //현재 좌표가 출구라면 이미 최단경로를 찾았으므로 더이상 반복문을 진행할 필요가 없다.

                    //출구좌표가 힙에 있다는 뜻은 이미 출구좌표의 부모노드도 작성되었음을 의미한다.
                    break;
                }
                //방문했으니 해당 좌표는 Closed = true로 바꾼다. 이 좌표로는 더이상 Open하지 않는다.
                PathInfo[nowTile.PosY, nowTile.PosX].isClosed = true;
                for(int i = 0; i < MY_CONST.DIR_NUM; i++)
                {
                    _PlayerPos nextPos = Player.MovePos(nowTile.PosY, nowTile.PosX, (E_Dir)i);
                    Tile nextTile = board.Tiles[nextPos.Y, nextPos.X];
                    //만약 다음타일이 벽이 아니고 isClosed가 true 가 아니라면
                    if (nextTile._TileType!=E_TileType.Wall && PathInfo[nextTile.PosY,nextTile.PosX].isClosed==false)
                    {
                        //이동 가능
                        //다음 타일의 우선순위가 기존 해당좌표 타일의 우선순위보다 높다면
                        //즉 F가 더 작다면
                        //더 빠른 경로를 찾은 것이므로 
                        //해당 타일의 pathinfo에서 부모좌표를 바꾸고 F, G를 바꾼다.                
                        if (nowTile.F+Costs(i)<nextTile.F)
                        {
                            //다음 타일 좌표의 부모좌표를 바꾼다.
                            PathInfo[nextTile.PosY, nextTile.PosX].parentPos = new _PlayerPos(nowTile.PosY, nowTile.PosX);
                            //해당 타일의 F 와 G를 갱신한다.
                            nextTile.G = nowTile.G + Costs(i);
                            nextTile.F = nextTile.G + nextTile.H;  
                            //다음 타일이 한번도 예약된적이 없으면, isOpened == false라면 push하고 true로 갱신한다.
                            if(PathInfo[nextTile.PosY, nextTile.PosX].isOpened==false)
                            {
                                PathInfo[nextTile.PosY, nextTile.PosX].isOpened = true;
                                Opened.Push(nextTile);
                            }
                            //이미 예약된적이 있다면 그대로 둔다.
                        }
                        //새로 찾은 경로가 기존의 경로보다 우선순위가 낮다.
                        //아무것도 바꾸지 않고 다음 타일을 찾는다.
                    }
                    //다음 타일의 tileType이 wall이다. 이동할 수 없으니 아무것도 바꾸지 않고 다음으로 이동할 수 있는 타일을 조사한다.
                }
                //현재타일에서 이동할 수 있는 모든 경로를 조사했다.
            }
            //지금까지 찾은 결과를 Player경로에 넣어주자
            _PlayerPos now = new _PlayerPos(MY_CONST.EXIT_Y, MY_CONST.EXIT_X);
            _PlayerPos next ;
            //부모의 좌표와 자신의 좌표가 같아질 때 까지
            while (true) 
            {
                //현재 좌표를 삽입한다.
                Player.PlayerPos.Add(now);
                //다음 좌표는 자신의 부모 좌표이다.
                next = PathInfo[now.Y, now.X].parentPos;
                //자신의 좌표와 자신의 부모의 좌표가 같다면 그곳은 시작점이다.
                if(now.Y== next.Y && now.X == next.X)
                {
                    break;
                }
                //현재 좌표를 현재좌표의 부모의 좌표로 바꾼다.
                now = PathInfo[now.Y, now.X].parentPos;
            }
        }
    }
}
