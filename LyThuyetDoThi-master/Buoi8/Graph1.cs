using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    class Graph
    {
        public const int Inf = 1000;
        int nRow, nCol, numVertices, sVertex, eVertex;
        int[,] arrGraph;
        int[] path, distance;
        bool[] status;
        Tuple<int, int> sNode;

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


        protected void InitIntArray(int value, int type = 1)
        {

            if (type == 1)
            {
                path = new int[numVertices];
                // if (isGrid)
                //     path = new int[nCol * nRow];

                for (int i = 0; i < path.Length; i++)
                    path[i] = value;

                return;
            }

            if (type == 2)
            {
                distance = new int[numVertices + 1];
                // if (isGrid)
                //     distance = new int[(nCol * nRow) + 1];

                for (int i = 0; i < distance.Length; i++)
                    distance[i] = value;

                return;
            }
        }

        protected void InitBoolArray(bool value = false)
        {
            status = new bool[numVertices];
            // if (isGrid)
            //     status = new bool[nCol * nRow];
            for (int i = 0; i < status.Length; i++)
                status[i] = value;
        }

        public void GoMargin(string fname)
        {
            ReadGoMargin(fname);
            GoMargin();
            //int size =0;
            //string str = "";
            //TracePath(ref str, ref size, sVertex, eVertex);
            //Console.WriteLine(str);
            WriteValue(fname.Substring(0, fname.Length - 3) + "out", distance[eVertex]);
        }

        protected void ReadGoMargin(string fname)
        {
            //int[,] graph;Text
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');

            nRow = Int32.Parse(line[0].Trim());
            nCol = Int32.Parse(line[1].Trim());

            numVertices = nRow * nCol;

            sNode = new Tuple<int, int>(Int32.Parse(line[2].Trim()) - 1, Int32.Parse(line[3].Trim()) - 1);
            sVertex = Point2Num(sNode.Item1, sNode.Item2);

            arrGraph = new int[nRow, nCol];
            eVertex = nRow * nCol;

            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    SetNode(i - 1, j, Int32.Parse(line[j].Trim()));
                }

            }
        }
        private int Point2Num(int i, int j)
        {
            return i * nCol + j;
        }
        private void Num2Point(int num, ref int i, ref int j)
        {
            i = num / nCol;
            j = num % nCol;
        }
        private bool ISMargin(int g)
        {
            int i = 0;
            int j = 0;
            Num2Point(g, ref i, ref j);

            return (i == 0) || (i == (nRow - 1)) || (j == 0) || (j == nCol - 1);
        }
        protected void GoMargin()
        {
            InitBoolArray(false);
            InitIntArray(-1, 1);
            InitIntArray(Inf, 2);
            int[] dx = { -1, 0, 0, 1 };
            int[] dy = { 0, -1, 1, 0 };
            int g = Point2Num(sNode.Item1, sNode.Item2);

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

                if (ISMargin(g))
                {
                    eVertex = g;
                    break;
                }

                status[g] = true;

                int xcur = 0;
                int ycur = 0;
                Num2Point(g, ref xcur, ref ycur);

                for (int i = 0; i < 4; i++)
                {
                    int x = xcur + dx[i];
                    int y = ycur + dy[i];
                    if (!InBoard(x, y))
                        continue;

                    int v = Point2Num(x, y);
                    if (!status[v])
                    {
                        int d = distance[g] + arrGraph[x, y];
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
        private void printArray(int type = 1)
        {
            if (type == 1)
            {
                foreach (int d in distance)
                    Console.Write(String.Format("{0,-5}", d));
                Console.WriteLine();
                return;

            }
            if (type == 2)
            {
                foreach (int d in path)
                    Console.Write(String.Format("{0,-5}", d));
                Console.WriteLine();
                return;

            }
        }
        protected void WriteValue(string fname, int value)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.Write(value);
            }
        }

    }
}
