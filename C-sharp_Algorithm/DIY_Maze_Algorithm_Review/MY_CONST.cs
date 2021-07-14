using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Maze_Algorithm_Review
{
    static class MY_CONST
    {
        public const char CIRCLE = '\u25cf';
        public const int FRAME = 1000 / 30;
        public const int PLAYER_TICK= 30;
        public const int ROW_SIZE = 25;
        public const int COLUMN_SIZE = 25;
        public const int START_Y = 1;
        public const int START_X = 1;
        public const int EXIT_Y = ROW_SIZE - 2;
        public const int EXIT_X = COLUMN_SIZE - 2;
        public const int DIR_NUM = 4;
    }
}
