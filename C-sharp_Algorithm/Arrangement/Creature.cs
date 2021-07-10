using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creature
{
    public class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Board.Board _board;
        Random _random = new();
        public void Initialize(int posY,int posX, int destX, int destY, Board.Board board)
        {
            PosX = posX;
            PosY = posY;
            _board = board;
        }


        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                // 0.1초마다 실행될 로직
                int randValue = _random.Next(0, 5);
                switch (randValue)
                {
                    case 0:
                        if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.Board.TileType.Empty) { PosY -= 1; }
                        break;
                    case 1:
                        if (PosY + 1 < _board.RowSize && _board.Tile[PosY + 1, PosX] == Board.Board.TileType.Empty) { PosY += 1; }
                        break;
                    case 2:
                        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.Board.TileType.Empty) { PosX -= 1; }
                        break;
                    case 3:
                        if (PosX + 1 < _board.ColumnSize && _board.Tile[PosY, PosX + 1] == Board.Board.TileType.Empty) { PosX += 1; }
                        break;
                }
            }
        }
    }
}
