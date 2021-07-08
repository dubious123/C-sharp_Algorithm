using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board;

namespace Rendering
{

    public class Create_Map
    {
        Initialize initial_Setting = new Initialize();
        const char CIRCLE = '\u25cf';
        const int WAIT_TICK = 1000 / 30;
        int lastTick = 0;
        
        public Create_Map(int row, int column)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            while (true)
            {
                if (WaitedEnough())
                {
                    Board_DynamicArray board_DynamicArray = new Board_DynamicArray();
                    Board_LinkedList board_LinkedList = new();
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < column; j++)
                        {
                            Console.Write(CIRCLE);
                        }
                        Console.WriteLine(); ;
                    }
                }
            }

        }
        protected bool WaitedEnough()
        {
            #region 프레임 관리
            //FPS 프레임 (60프레임 OK 30프레임 NO)


            int currentTick = System.Environment.TickCount;
            //문지기, 일정 시간이 지나지 않았다면 문을 열어주지 않는다.
            //만약에 경과한 시간이 1/30초보다 작다면
            if (currentTick - lastTick < WAIT_TICK) { return false; }
            lastTick = currentTick;
            #endregion
            //입력
            //출력
            //렌더링
            Console.SetCursorPosition(0, 0);
            return true;
        }
    }
}
