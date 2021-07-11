using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    //Graph생성
    //1. 행렬
    //2. 리스트
    //그래프의 순회
    //1. DFS : 깊이 우선 탑색
    //2. BFS : 너비 우선 탑색
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            { 0,1,0,1,0,0 },
            { 1,0,1,1,0,0 },
            { 0,1,0,0,0,0 },
            { 1,1,0,0,0,0 },
            { 0,0,0,0,0,1 },
            { 0,0,0,0,1,0 },
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int>() { 1, 3 },
            new List<int>() { 0, 2, 3 },
            new List<int>() { 1 },
            new List<int>() { 0, 1 },
            new List<int>() { 5 },
            new List<int>() { 4 }
        };

        bool[] visited = new bool[6];
        //1. now부터 방문하고
        //2. now와 연결된 정점들을 하나씩 확인해서 아직 미방문 상태라면 방문한다.
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;  //1.

            //행렬버전
            for (int next = 0; next < adj.GetLength(0); next++)
            {
                if (adj[now, next] == 0 || visited[next]) { continue; }
                //미방문, 연결됨

                DFS(next);
            }

        }
        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;

            //리스트 버전
            foreach (int next in adj2[now])
            {
                if (visited[next]) { continue; }
                DFS2(next);

            }
        }

        //그런데 그래프가 모두 연결된것이라는 보장이 없다.

        public void SearchAll()
        {
            visited = new bool[6];
            for (int now = 0; now < adj.GetLength(0); now++) { if (visited[now] == false) { DFS(now); } }
        }
    }
}
