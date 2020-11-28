using System;
using System.Collections.Generic;
using System.IO;

namespace BT2
{
    class Program
    {
        static void Main(string[] args)
        {
            BACVAOBACRA BACVAOBACRA = new BACVAOBACRA();
            BACVAOBACRA.Read("BACVAOBACRA.INP");
            BACVAOBACRA.Write();
            Console.WriteLine("OUTPUT");
            BACVAOBACRA.DoThi("BACVAOBACRA.OUT");
        }
    }
}
