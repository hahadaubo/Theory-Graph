using System;
using System.Collections.Generic;
using System.IO;

namespace BT1
{
    class Program
    {
        static void Main(string[] args)
        {
            BACDOTHIVOHUONG BACDOTHIVOHUONG = new BACDOTHIVOHUONG();
            BACDOTHIVOHUONG.Read("BACDOTHIVOHUONG.INP");
            BACDOTHIVOHUONG.Write();
            Console.WriteLine("OUTPUT");
            BACDOTHIVOHUONG.DoThi("BACDOTHIVOHUONG.OUT");
        }
    }
}
