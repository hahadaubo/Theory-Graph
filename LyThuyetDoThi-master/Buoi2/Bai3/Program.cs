using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_Bai3
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.Read("BONCHUA.INP");
            graph.Write();
            graph.BonChua("BONCHUA.OUT");
        }
    }
}
