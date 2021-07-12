using System;
using System.Collections.Generic;

namespace Exercise
{
    public class knight : IComparable<knight>
    {
        public int Id { get; set; }

        public int CompareTo(knight other)
        {
            if (Id == other.Id) { return 0; }
            return Id > other.Id ? 1 : -1;
        }
    }
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

            //TreeNode<string> root = new TreeNode<string>("R1 개발실",
            //        new TreeNode<string>("디자인팀",
            //    new TreeNode<string>("전투"),
            //    new TreeNode<string>("경제"),
            //    new TreeNode<string>("스토리")
            //    ),
            //        new TreeNode<string>("프로그래밍",
            //    new TreeNode<string>("서버"),
            //    new TreeNode<string>("클라"),
            //    new TreeNode<string>("엔진")),
            //        new TreeNode<string>("아트팀",
            //    new TreeNode<string>("배경"),
            //    new TreeNode<string>("캐릭터"))
            //    );
            //Tree<string> R1 = new Tree<string>();
            //R1.Initialize(root);
            //Tree<string>.PrintTree(R1._Root);

            PriorityQueue<knight> q = new PriorityQueue<knight>();
            q.Push(new knight() { Id = 20 });
            q.Push(new knight() { Id = 30 });
            q.Push(new knight() { Id = 40 });
            q.Push(new knight() { Id = 10 });
            q.Push(new knight() { Id = 90 });
            q.Push(new knight() { Id = 20 });
            while (q.Count() > 0) { Console.WriteLine(q.Pop().Id); }

        }
    }
}
