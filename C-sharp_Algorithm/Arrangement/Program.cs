
using System;

namespace Arrangement
{

    //1. 매열 : 연속된방, 추가축소불가
    //2. 동적배열 List: 조금 더 많이 예약, 예약도 다 차면 이사, 중간삽입, 삭제를 하려면 또 이사를 해야한다.
    //3. 연결리스트 LinkedList: 중간 삽입, 삭제 쉽다.(유일한 장점) N번째 방을 바로 찾을수가 없다.
    //즉 임의접근 Random Access 불가.
    class Program
    {
        const int ROW = 25;
        const int COLUMN = 25;
        const int WAIT_TICK = 1000 / 30;//30프레임
        static void Main(string[] args)
        {
            int StartTick = 0;

            Board.Board board = new();
            Creature.Player player = new();
            player.Initialize(1, 1, board.ColumnSize-2, board.RowSize-2, board);
            board.Initialize(ROW, COLUMN, player);

            while (true)
            {

                if (board.FrameControl(StartTick, System.Environment.TickCount, WAIT_TICK))
                {
                    player.Update(System.Environment.TickCount- StartTick);
                    StartTick = System.Environment.TickCount;
                    Console.SetCursorPosition(0, 0);
                    board.Render(player);
                }

            }
        }
    }
}
