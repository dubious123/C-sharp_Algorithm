using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board
{
    public class MyDList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] _data = new T[DEFAULT_SIZE];
        public int Count = 0; //사용중인 데이터 개수
        public int Capacity { get { return _data.Length; } } // 예약된 개수
        //예외케이스, 이사는 가끔 하니깐 이사비용은 무시한다. O(1)

        public void Add(T item)
        {
            // 1. 공간이 충분히 남아 있는지 확인한다.
            if (Count >= Capacity)
            {
                //공간을 다시 늘려서 확보한다.
                T[] newArray = new T[Count * 2];
                for(int i = 0; i < Count; i++)
                {
                    newArray[i] = _data[i];
                }
                _data = newArray;
            }
            // 2. 공간에다가 데이터를 넣어준다.
            _data[Count++] = item;
        }

        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }


        //애매하네, 최악을 생각, O(N)
        public void ReMoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i + 1];
            }
            //메모리 제거?
            _data[Count - 1] = default(T);
            Count--;

        }
    }
    public class Board_DynamicArray
    {
        public List<int> _data = new List<int>();
        public MyDList<int> _data2 = new MyDList<int>();
     

        public Board_DynamicArray()
        {
            _data2.Add(101);
            _data2.Add(102);
            _data2.Add(103);
            _data2.Add(104);
            _data2.Add(105);
            _data2.Add(106);
        }
    }
}
