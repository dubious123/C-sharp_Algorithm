using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise
{
    //그래프는 보통 정적인 상황에서 많이 사용
    //트리는 동적인 상황에서 많이 사용 
    //노드를 만들어서 관리

    class TreeNode<T>
    {
        public T _Data { get; set; }
        public List<TreeNode<T>> _Children { get; set; } = new List<TreeNode<T>>();

        public TreeNode(T Data, params TreeNode<T>[] ChildList)
        {
            _Data = Data;
            foreach(TreeNode<T> child in ChildList) { _Children.Add(child); }
        }
        public TreeNode(T Data) : this(Data,new TreeNode<T>[0]) { }
    }
    class Tree<T>
    {
        public TreeNode<T> _Root { get; private set; }
        public int _TreeHight;
        public bool IsEmpty;
        public int NumOfNodes;
        public void Initialize(TreeNode<T> Root)
        {
           _Root = Root;
            _TreeHight = GetHeight(_Root);
        }
        public static int GetHeight(TreeNode<T> Root)
        {
            int height = 0;
            foreach(TreeNode<T> child in Root._Children)
            {
                int newheight = GetHeight(child) + 1;
                height = Math.Max(height, newheight);
            }


            return height;
        }
        public static void PrintTree(TreeNode<T> Root)
        {
            Console.WriteLine(Root._Data);
            foreach(TreeNode<T> child in Root._Children) { PrintTree(child); }
        }
    }
}
