using System;

namespace DIY_Right_hand_Algorighm
{
    class Program
    {
        const int ROW_SIZE = 25;
        const int COLUMN_SIZE = 25;
        const int FRAME = 1000/30;
        static void Main(string[] args)
        {
            Board board = new(ROW_SIZE,COLUMN_SIZE);
            board.CreateMazeBySideWinder();
            Player player = new(board._Tiles, ROW_SIZE, COLUMN_SIZE);
            int current_tick = 0;
            while (true)
            {
                if (board.FrameControl(FRAME,current_tick))
                {
                    board.Render(player,current_tick);
                    current_tick = System.Environment.TickCount;
                }
            }
        }
    }
}
