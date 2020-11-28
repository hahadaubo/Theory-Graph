using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Graph
    {
        private int V;
        private List<List<int>> adj;

        Graph(int v)
        {
            V = v;
            adj = new List<List<int>>(v);
            for (int i = 0; i < v; i++)
                adj.Add(new List<int>());
        }

        public void AddEdge(int v, int w) { adj[v].Add(w); }

        void TopologicalSortUtil(int v, bool[] visited,
                                 Stack<int> stack)
        {

            visited[v] = true;

            foreach (var vertex in adj[v])
            {
                if (!visited[vertex])
                    TopologicalSortUtil(vertex, visited, stack);
            }

            stack.Push(v);
        }

        void TopologicalSort()
        {
            Stack<int> stack = new Stack<int>();

            var visited = new bool[V];

            for (int i = 0; i < V; i++)
            {
                if (visited[i] == false)
                    TopologicalSortUtil(i, visited, stack);
            }

            foreach (var vertex in stack)
            {
                Console.Write(vertex + " ");
            }
        }

        class Program
        {
            static void Main(string[] args)
            {

                Graph g = new Graph(8);
                g.AddEdge(1, 2);
                g.AddEdge(1, 5);
                g.AddEdge(2, 3);
                g.AddEdge(5, 6);
                g.AddEdge(5, 7);
                g.AddEdge(3, 4);
                g.AddEdge(7, 6);
                g.AddEdge(7, 4);


                g.TopologicalSort();
            }
        }
    }
}
