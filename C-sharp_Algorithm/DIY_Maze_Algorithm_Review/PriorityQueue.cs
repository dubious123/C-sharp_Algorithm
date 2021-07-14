using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY_Maze_Algorithm_Review
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> _heap = new List<T>();
        public PriorityQueue() { Count = _heap.Count; }
        public int Count { get; private set; }
        private int LastIndex { get; set; }
        public bool isEmpty() { return Count == 0; }
        public T Pop()
        {
            T root = _heap[0];
            _heap[0] = _heap[LastIndex];
            _heap.RemoveAt(LastIndex);
            Count--;
            LastIndex--;
            if (Count == 0) { return root; } //단일 노드 힙
            //downheap
            int nowIndex = 0;
            int leftChildIndex;
            int rightChildIndex;
            int succIndex = 0;
            while (true)
            {

                leftChildIndex = nowIndex * 2 + 1;
                //단일노드 downheap 끝
                if (leftChildIndex > LastIndex) { break; }
                //왼쪽 자식만 존재
                if (leftChildIndex == LastIndex)
                {
                    //왼쪽 자식이 더 크면 바꾼다
                    if (_heap[nowIndex].CompareTo(_heap[leftChildIndex]) < 0)
                    {
                        MyLibrary.Swap(_heap, nowIndex, leftChildIndex);
                    }
                    //결과와 상관없이 끝
                    break;
                }
                //두 자식 모두 존재
                rightChildIndex = leftChildIndex + 1;
                //부모노드가 두 자식노드보다 크면 나간다.
                if(_heap[nowIndex].CompareTo(_heap[leftChildIndex]) >= 0 && _heap[nowIndex].CompareTo(_heap[rightChildIndex]) >= 0)
                {
                    break;
                }
                if (_heap[nowIndex].CompareTo(_heap[leftChildIndex]) < 0)
                {
                    MyLibrary.Swap(_heap, nowIndex, leftChildIndex);
                    succIndex = leftChildIndex;
                }
                if (_heap[nowIndex].CompareTo(_heap[rightChildIndex]) < 0)
                {
                    MyLibrary.Swap(_heap, nowIndex, rightChildIndex);
                    succIndex = rightChildIndex;
                }
                
                nowIndex = succIndex;
            }

            return root;
        }
        public void Push(T data)
        {
            _heap.Add(data);
            Count++;
            LastIndex = Count - 1;
            if (Count == 1) { return; }
            int nowIndex = LastIndex; //삽입된 데이터의 초기 인덱스
            int parentIndex = (nowIndex - 1) / 2; //부모 인덱스
            while (nowIndex != 0) //현재 인덱스가 0이 될 때까지
            {
                //부모의 데이터와 자신의 데이터를 비교해서
                //부모의 우선순위가 더 크거나 같으면 그대로, 반복문 종료
                if (_heap[parentIndex].CompareTo(_heap[nowIndex]) >= 0) { break; }
                //자식의 우선순위가 더 크면 두 데이터를 바꾸고
                //자식의 인덱스를 부모 인덱스로 바꾸고 부모인덱스도 바꾼다.

                MyLibrary.Swap<T>(_heap, nowIndex, parentIndex);
                nowIndex = parentIndex;
                parentIndex = (nowIndex - 1) / 2;
            }
        }
        public T Peek() { return _heap[0]; }
    }
}
