using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Maze_Algorithm_Review
{
    public struct _PlayerPos
    {
        public int Y { get; internal set; }
        public int X { get; internal set; }

        public _PlayerPos(int v1, int v2)
        {
            Y = v1;
            X = v2;
        }
    }
    public class Player
    {
        public _PlayerPos CurrentPos = new _PlayerPos();
        public ConsoleColor PlayerColor { get; private set; }
        internal List<_PlayerPos> PlayerPos = new();

        public Player()
        {
            Initialize();
        }
        public void Initialize()
        {
            PlayerPos.Clear();
            CurrentPos = new _PlayerPos(MY_CONST.START_Y, MY_CONST.START_X);
            PlayerColor = ConsoleColor.Blue;
        }

        public _PlayerPos MovePos(E_Dir e_Dir)
        {
            int[] _DeltaY = { -1, 0, 1, 0 };
            int[] _DeltaX = { 0, 1, -1, 0 };
            return new _PlayerPos(CurrentPos.Y + _DeltaY[(int)e_Dir], CurrentPos.X + _DeltaX[(int)e_Dir]);
        }
        public static _PlayerPos MovePos(_PlayerPos CurrentPos,E_Dir e_Dir)
        {
            int[] _DeltaY = { -1, 0, 1, 0 };
            int[] _DeltaX = { 0, 1, -1, 0 };
            return new _PlayerPos(CurrentPos.Y + _DeltaY[(int)e_Dir], CurrentPos.X + _DeltaX[(int)e_Dir]);
        }
        public static _PlayerPos MovePos(int PosY,int PosX, E_Dir e_Dir)
        {
            int[] _DeltaY = { -1, 0, 1, 0 };
            int[] _DeltaX = { 0, 1, 0, -1 };
            return new _PlayerPos(PosY + _DeltaY[(int)e_Dir], PosX + _DeltaX[(int)e_Dir]);
        }
        int sumTick = 0;
        internal void update(int DeltaTick)
        {
            sumTick += DeltaTick;
            if (sumTick > MY_CONST.PLAYER_TICK)
            {
                CurrentPos = PlayerPos.Last();
                PlayerPos.RemoveAt(PlayerPos.Count - 1);
                sumTick = 0;
            }
        }
    }
}
