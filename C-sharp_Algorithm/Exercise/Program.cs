using System;
using System.Collections.Generic;

namespace Exercise
{
    class Program
    {
        //스택 : LIFO
        //큐 : FIFO

        //[1] [2] [3] [4] -> [주차장] : 큐   온라인 패키지 순차적으로 처리

        //[1] [2] [3] [4] //사고// : 스택   UI팝업창 나중에 뜬 창부터 꺼야함
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(101);
            stack.Push(101);
            stack.Push(101);
            stack.Push(101);
            stack.Push(101);
            stack.Push(101);

            int data = stack.Peek();
            int data1 = stack.Pop();

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(101);
            queue.Enqueue(101);
            queue.Enqueue(101);
            queue.Enqueue(101);
            queue.Enqueue(101);
            int data2 = queue.Dequeue();
            int data3 = queue.Peek();
            //linkedlist도 다 제공하는것인데 왜 배우냐?

            //의사소통에 좋다, 성능도 조금 더 좋다.

            //List를 이용하여 stack 구현 -> ok
            //List를 이용하여 queue 구현 -> 문제
            //queue는 순환 버퍼를 이용하여 구현



            Graph graph = new Graph();
            //graph.SearchAll();

            //graph.BFS2(0);
            graph.Dijikstra(0);
        }
    }
}
