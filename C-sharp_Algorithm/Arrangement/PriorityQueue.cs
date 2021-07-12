using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> _heap = new List<T>();

        //log(N)
        public void Push(T data)
        {
            //힙의 맨 끝에 새로운 데이터를 삽입한다.
            _heap.Add(data);
            int now = _heap.Count - 1;
            while (now > 0)
            {
                int next = (now - 1) / 2; //부모
                if (_heap[now].CompareTo(_heap[next])<0) { break; }
                //두 값 교체
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;

                //검사위치 이동
                now = next;
            }
        }
        //log(N)
        public T Pop()
        {
            T ret = _heap[0];
            //마지막 데이터 루트로 이동
            int lastIndex = _heap.Count - 1;
            _heap[0] = _heap[lastIndex];
            _heap.RemoveAt(lastIndex);
            lastIndex--;
            //역으로 내려가는 도장깨기
            int now = 0;
            
            while (true)
            {
                int leftIndex = 2 * now + 1;
                int rightIndex = 2 * now + 2;

                int next = now;
                //왼쪽값이 현재값보다 크면 왼쪽으로 이동
                if(leftIndex<=lastIndex && _heap[next].CompareTo(_heap[leftIndex])<0) { next = leftIndex; }
                //오른쪽값이 현재값보다 크면 오른쪽으로 이동
                if(rightIndex<=lastIndex && _heap[next].CompareTo(_heap[rightIndex])<0) { next = rightIndex; }
                //왼쪽 오른쪽 모두 현재값보다 작으면 종료
                if(next == now) { break; }
                //두 값을 교체한다.
                T temp = _heap[now];
                _heap[now] = _heap[next];
                _heap[next] = temp;
                //검사위치 이동
                now = next;
                
            }

            return ret;
        }
        public int Count { get { return _heap.Count; } }
    }
}
