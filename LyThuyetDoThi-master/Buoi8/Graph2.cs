using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Graph
    {
        public const int Inf = 1000;
        int nRow, nCol, numVertices, eVertex;
        int[,] arrGraph;
        int[] path, distance;
        bool[] status;
        protected int maxWeight = 0;

        private bool InBoard(int x, int y)
        {
            return (x > -1) && (x < nRow) && (y > -1) && (y < nCol);
        }

        public void SetNode(int i, int j, int value)
        {
            if (i < 0 && i < numVertices && j > -1 && j < numVertices)
            {
                Console.WriteLine(String.Format("Out of range ({0}, {1})", i, j));
                return;
            }
            arrGraph[i, j] = value;
        }
        public int GetNode(int i, int j)
        {
            if (i < 0 && i < numVertices && j > -1 && j < numVertices)
            {
                Console.WriteLine(String.Format("Out of range ({0}, {1})", i, j));
                return Int32.MinValue;
            }
            return arrGraph[i, j];
        }

        protected void InitIntArray(int value, int type = 1)
        {

            if (type == 1)
            {
                path = new int[numVertices];

                for (int i = 0; i < path.Length; i++)
                    path[i] = value;

                return;
            }

            if (type == 2)
            {
                distance = new int[numVertices + 1];

                for (int i = 0; i < distance.Length; i++)
                    distance[i] = value;

                return;
            }
        }

        protected void InitBoolArray(bool value = false)
        {
            status = new bool[numVertices];
            for (int i = 0; i < status.Length; i++)
                status[i] = value;
        }
        public void ReadMatrix2Matrix(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            numVertices = Int32.Parse(lines[0].Trim());
            Console.WriteLine("\t Number of vertices: " + numVertices);
            arrGraph = new int[numVertices, numVertices];

            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int value = Int32.Parse(line[j].Trim());
                    if (value > maxWeight)
                        maxWeight = value;
                    SetNode(i - 1, j, value);
                    Console.Write(String.Format("{0,-3}", GetNode(i - 1, j)));
                }
                Console.WriteLine();

            }

        }
        public void ChooseCity(string fname)
        {
            ReadMatrix2Matrix(fname);
            eVertex = numVertices;
            Tuple<int, int> city = ChooseCity();
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname.Substring(0, fname.Length - 3) + "OUT"))
            {
                file.WriteLine(city.Item1);
                file.WriteLine(city.Item2);
            }
        }

        protected void AdjMatrixShortestPath(int g)
        {
            InitIntArray(-1);
            InitIntArray(Inf, 2);
            InitBoolArray(false);
            distance[g] = 0;

            do
            {
                g = eVertex;
                for (int i = 0; i < numVertices; i++)
                {
                    if (!status[i] && distance[g] > distance[i])
                    {
                        g = i;
                    }
                }

                if ((distance[g] == Inf) || g == eVertex)
                    break;

                status[g] = true;

                for (int v = 0; v < numVertices; v++)
                {
                    if (!status[v])
                    {
                        int d = distance[g] + arrGraph[g, v];
                        if (distance[v] > d)
                        {
                            distance[v] = d;
                            path[v] = g;
                        }
                    }
                }
            }
            while (true);

        }

        private int Max()
        {
            int pos = 0;

            for (int i = 1; i < numVertices; i++)
                if (distance[pos] < distance[i])
                {
                    pos = i;

                }
            return pos;
        }

        protected Tuple<int, int> ChooseCity()
        {
            Tuple<int, int> res = new Tuple<int, int>(-1, Inf);
            for (int v = 0; v < numVertices; v++)
            {
                AdjMatrixShortestPath(v);
                int dist = distance[Max()];
                if (res.Item2 > dist)
                    res = new Tuple<int, int>(v, dist);

            }
            return res;
        }

    }
}
