using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LTDT_Bai2
{
    class Graph
    {
        public const int Inf = 1000;
        int[,] arrGraph;
        List<List<Tuple<int, int>>> adjWList;
        int numVertices;
        int numEdges;
        int nRow;
        int nCol;
        bool[] status;
        int sVertex;
        int eVertex;
        int iVertex;
        int[] path;
        int[] distance;

        protected void ReadAdjListSP(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            numEdges = Int32.Parse(line[1].Trim());
            sVertex = Int32.Parse(line[2].Trim()) - 1;
            eVertex = Int32.Parse(line[3].Trim()) - 1;
            if (line.Length > 4)
                iVertex = Int32.Parse(line[4].Trim()) - 1;

            Console.WriteLine("\t Number of vertices: " + numVertices);

            adjWList = new List<List<Tuple<int, int>>>();
            for (int i = 0; i < numVertices; i++)
                adjWList.Add(new List<Tuple<int, int>>());

            Console.WriteLine(adjWList.Count);
            for (int i = 0; i < numEdges; i++)
            {
                line = lines[i + 1].Split(' ');

                Console.WriteLine(lines[i + 1]);
                int v1 = Int32.Parse(line[0].Trim()) - 1;
                int v2 = Int32.Parse(line[1].Trim()) - 1;
                int w = Int32.Parse(line[2].Trim());
                adjWList[v1].Add(new Tuple<int, int>(v2, w));
                adjWList[v2].Add(new Tuple<int, int>(v1, w));
            }

        }

        public void SPIntermediaryVertex(string fname)
        {
            ReadAdjListSP(fname);
            //ReadMatrixSPIV(fname);
            ShortestPathIV(fname.Substring(0, fname.Length - 3) + "out");

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
 

        protected void ReadMatrixSPIV(string fname)
        {

            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            numVertices = Int32.Parse(line[0].Trim());
            numEdges = Int32.Parse(line[1].Trim());
            sVertex = Int32.Parse(line[2].Trim());
            eVertex = Int32.Parse(line[3].Trim());
            if (line.Length > 4)
                iVertex = Int32.Parse(line[4].Trim());
            Console.WriteLine("\t Number of vertices: " + numVertices);
            arrGraph = new int[numVertices, numVertices];

            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    SetNode(i - 1, j, Int32.Parse(line[j].Trim()));
                }
                Console.WriteLine();

            }
        }

        protected List<int> TracePath(int x, int y)
        {
            List<int> list = new List<int>();
            int v = path[y];
            Console.Write(String.Format("{0,-3}", v));
            if (v != -2)
            {

                list.Add(y + 1);

                while (v != sVertex)
                {
                    list.Insert(0, v);

                    v = path[v];


                }
                list.Insert(0, x + 1);

            }
            else
            {
                list.Add(-1);

            }
            return list;
        }
        private void SubSP()
        {
            int g = sVertex;
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
                status[g] = true;
                if ((distance[g] == Inf) || g == eVertex)
                    break;
                foreach (Tuple<int, int> v in adjWList[g])
                {
                    if (!status[v.Item1])
                    {
                        int d = distance[g] + v.Item2;
                        if (distance[v.Item1] > d)
                        {
                            distance[v.Item1] = d;
                            path[v.Item1] = g;
                        }
                    }
                }

            }
            while (true);

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
                distance = new int[numVertices];

                for (int i = 0; i < distance.Length; i++)
                    distance[i] = value;

                return;
            }

        }
        protected void InitBoolArray(bool value, bool isGrid = false)
        {
            status = new bool[numVertices];
            if (isGrid)
                status = new bool[nCol * nRow];
            for (int i = 0; i < status.Length; i++)
                status[i] = value;
        }

        protected void ShortestPath()
        {
            InitIntArray(-1);
            InitIntArray(Inf, 2);
            InitBoolArray(false);
            int g = sVertex;
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
                status[g] = true;
                if ((distance[g] == Inf) || g == eVertex)
                    break;
                foreach (Tuple<int, int> v in adjWList[g])
                {
                    if (!status[v.Item1])
                    {
                        int d = distance[g] + v.Item2;
                        if (distance[v.Item1] > d)
                        {
                            distance[v.Item1] = d;
                            path[v.Item1] = g;
                        }
                    }
                }

            }
            while (true);
        }

        protected List<int> ShortestPathIV(string fname)
        {
            int temp = eVertex;
            eVertex = iVertex;
            ShortestPath();

            int dist = distance[eVertex];

            List<int> phase1 = TracePath(sVertex, eVertex);
            eVertex = temp;
            temp = sVertex;
            sVertex = iVertex;
            for (int i = 0; i < numVertices; i++)
                if (!phase1.Contains(i + 1))
                {
                    status[i] = false;
                    path[i] = -1;
                    distance[i] = Inf;
                }


            SubSP();
            List<int> phase2 = TracePath(sVertex, eVertex);
            dist += distance[eVertex];
            phase1.RemoveAt(phase1.Count - 1);
            phase1.AddRange(phase2);
            return phase1;

        }

    }
}
