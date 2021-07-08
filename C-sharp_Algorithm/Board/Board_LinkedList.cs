using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    public class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next, Prev;
    }
    public class MyLinkedList<T>
    {
        public int Count = 0;
        public MyLinkedListNode<T> Head =null;
        public MyLinkedListNode<T> Tail =null;

        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newNode = new();
            newNode.Data = data;
            if (Head == null)
            {
                Head = newNode;
            }
            if(Tail != null)
            {
                Tail.Next = newNode;
                Tail = newNode;
            }
            Tail = newNode;
            Count++;
            return newNode;
        }
        public void Remove(MyLinkedListNode<T> wantedNode)
        {
            if (Head == wantedNode)
            {
                Head = Head.Next;
            }
            if (Tail == wantedNode)
            {
                Tail = Tail.Prev;
            }
            if(wantedNode.Prev != null)
            {
                wantedNode.Prev.Next = wantedNode.Next;
            }
            if(wantedNode.Next != null)
            {
                wantedNode.Next.Prev = wantedNode.Prev;
            }
            if (Count > 0)
            {
                Count--;
            }
        }
    }
    public class Board_LinkedList
    {
        public LinkedList<int> _data2 = new LinkedList<int>();
        public MyLinkedList<int> _data3 = new();

        public Board_LinkedList()
        {
            _data3.AddLast(101);
            _data3.AddLast(102);
            MyLinkedListNode<int> node103 = _data3.AddLast(103);
            _data3.AddLast(104);
            _data3.AddLast(105);
            _data3.AddLast(106);
            _data3.Remove(node103);

            
        }
    }
}
