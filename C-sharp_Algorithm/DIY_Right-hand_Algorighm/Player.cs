using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Right_hand_Algorighm
{
    class Player
    {
        class Pos
        {
            public Pos(int PosY, int PosX)
            {
                _PosY = PosY;
                _PosX = PosX;
            }
            public int _PosY { get; private set; }
            public int _PosX { get; private set; }
        }
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        List<Pos> PlayerPos = new List<Pos>();

        public ConsoleColor PlayerColor { get; private set; }
        public int _ROW_SIZE { get; private set; }
        public int _COLUMN_SIZE { get; private set; }
        enum _playerDir
        {
            Up,Right,Down,Left
        }
        bool _IsPossibleToGo(Board.Tile[,] tiles,int PosY, int PosX)
        {
            return tiles[PosY, PosX]._tileType == Board.TileType.empty&&(PosX>=0&&PosX<_COLUMN_SIZE)&&(PosY>=0&&PosY<_ROW_SIZE);
        }
        bool _PlayerMoveForward(Board.Tile[,] tiles, _playerDir _Dir)
        {
            int[] moveY = new int[4] { -1, 0, 1, 0 };
            int[] moveX = new int[4] { 0, 1, 0, -1 };
            if (_IsPossibleToGo(tiles, PosY + moveY[(int)_Dir], PosX + moveX[(int)_Dir]))
            {
            PosY += moveY[(int)_Dir];
            PosX += moveX[(int)_Dir];
            return true;
            }
            return false;

        }
        bool _PlayerMoveRightward(Board.Tile[,] tiles, _playerDir _Dir)
        {
            int[] moveY = new int[4] { 0, 1, 0, -1 };
            int[] moveX = new int[4] { 1, 0, -1, 0 };
            if (_IsPossibleToGo(tiles, PosY + moveY[(int)_Dir], PosX + moveX[(int)_Dir]))
            {
                PosY += moveY[(int)_Dir];
                PosX += moveX[(int)_Dir];
                return true;
            }
            return false;
        }
        _playerDir _PlayerRightDir(_playerDir _Dir)
        {
            return (_playerDir)Enum.ToObject(typeof(_playerDir), ((int)_Dir +1 +4)%4);
        }
        _playerDir _PlayerLeftDir(_playerDir _Dir)
        {
            return (_playerDir)Enum.ToObject(typeof(_playerDir), ((int)_Dir - 1 + 4) % 4);
        }
        public void Initialize(Board.Tile[,] tiles, int ROW_SIZE, int COLUMN_SIZE)
        {
            int _ExistY = Board.ExitY;
            int _ExistX = Board.ExitX;
            PosY = 1;
            PosX = 1;
            PlayerPos.Add(new Pos(PosY, PosX));
            _playerDir CurrentPlayerDir = _playerDir.Down;
            _ROW_SIZE = ROW_SIZE;
            _COLUMN_SIZE = COLUMN_SIZE;
            PlayerColor = ConsoleColor.Blue;

            while (PlayerPos.Last()._PosX != _ExistX && PlayerPos.Last()._PosX != _ExistX)
            {
                //오른쪽으로 갈 수 있으면 오른쪽으로 돌고 앞으로 간다
                if (_PlayerMoveRightward(tiles, CurrentPlayerDir)) 
                {
                    CurrentPlayerDir = _PlayerRightDir(CurrentPlayerDir);
                    PlayerPos.Add(new Pos(PosY, PosX));
                    continue; 
                }
                //앞으로 갈 수 있으면 앞으로 간다
                else if (_PlayerMoveForward(tiles, CurrentPlayerDir)) 
                { 
                    PlayerPos.Add(new Pos(PosY, PosX));
                    continue;
                }
                //왼쪽으로 돈다
                else { CurrentPlayerDir = _PlayerLeftDir(CurrentPlayerDir);}
            }

            
        }
        public Player(Board.Tile[,] tiles, int ROW_SIZE, int COLUMN_SIZE)
        {
            Initialize(tiles, ROW_SIZE, COLUMN_SIZE);
        }


        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;

        public void PlayerUpdate(int delta_tick)
        {
            _sumTick += delta_tick;
            if (_lastIndex >= PlayerPos.Count) { return; }
            if (_sumTick > MOVE_TICK) 
            {
                _sumTick = 0;
                PosY = PlayerPos[_lastIndex]._PosY;
                PosX = PlayerPos[_lastIndex]._PosX;
                _lastIndex++;
            }

        }
    }
}
