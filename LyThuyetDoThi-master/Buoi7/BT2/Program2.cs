using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_Bai2
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.SPIntermediaryVertex("NganNhatX.INP");
            Console.ReadKey();
        }
    }
}
