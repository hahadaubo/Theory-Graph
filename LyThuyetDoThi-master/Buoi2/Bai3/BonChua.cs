using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Bai3
{
    class Graph
    {
        private int n;
        private int[,] arrGraph;

        public void Read(string fName)
        {
            StreamReader sr = new StreamReader(fName);
            n = int.Parse(sr.ReadLine());
            arrGraph = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] line = sr.ReadLine().Split();
                for (int j = 0; j < n; j++)
                {
                    arrGraph[i, j] = int.Parse(line[j]);
                }
            }
            sr.Close();
        }
        public void Write()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{arrGraph[i, j]}  ");
                }
                Console.WriteLine();
            }
        }

        public void BonChua(string fName)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                int inDegree = 0;
                int outDegree = 0;
                for (int j = 0; j < n; j++)
                {
                    outDegree += arrGraph[i, j];
                    inDegree += arrGraph[j, i];
                }
                if (outDegree == 0 && inDegree != 0)
                {
                    list.Add(i + 1);
                }
            }
            using (StreamWriter sWriter = new StreamWriter(fName))
            {
                sWriter.WriteLine(list.Count);
                foreach (int i in list)
                {
                    sWriter.WriteLine(i);
                }

            }
        }

    }
}
