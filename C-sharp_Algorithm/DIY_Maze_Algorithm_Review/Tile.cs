using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Maze_Algorithm_Review
{
    public class Tile : IComparable<Tile>
    {
        #region property
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        //F(총 비용) = H(고정) + G(시작위치에서 이곳까지 필요한 비용) 작을수록 좋다.
        public int F { get; set; }
        public int H { get; private set; }
        public int G { get; set; }
        public E_TileType _TileType { get; internal set; }
        public ConsoleColor _TileColor { get; internal set; }
        #endregion
        public Tile(E_TileType TileType, int Y, int X)
        {
            PosY = Y;
            PosX = X;
            H = Math.Abs(MY_CONST.EXIT_Y - PosY) + Math.Abs(MY_CONST.EXIT_X - PosX);
            G = Int32.MaxValue; //기본값은 갈 수 없음으로 표시
            F = Int32.MaxValue;
            switch (TileType)
            {
                case E_TileType.Wall:
                    _TileType = E_TileType.Wall;
                    _TileColor = ConsoleColor.Red;
                    break;
                case E_TileType.Empty:
                    _TileType = E_TileType.Empty;
                    _TileColor = ConsoleColor.Green;
                    break;
                case E_TileType.Exit:
                    _TileType = E_TileType.Exit;
                    _TileColor = ConsoleColor.Yellow;
                    break;
            }
        }
        public Tile(E_TileType TileType, int Y, int X,int G) : this(TileType, Y, X)
        {
            this.G = G;
            F = H + G;
        }
        public void EditTile(E_TileType TileType)
        {
            switch (TileType)
            {
                case E_TileType.Wall:
                    _TileType = E_TileType.Wall;
                    _TileColor = ConsoleColor.Red;
                    break;
                case E_TileType.Empty:
                    _TileType = E_TileType.Empty;
                    _TileColor = ConsoleColor.Green;
                    break;
                case E_TileType.Exit:
                    _TileType = E_TileType.Exit;
                    _TileColor = ConsoleColor.Yellow;
                    break;
            }
        }
        public int CompareTo(Tile other)
        {
            if (this.F > other.F) { return -1; }
            else if (this.F == other.F) { return 0; }
            else { return 1; }
        }
    }
}