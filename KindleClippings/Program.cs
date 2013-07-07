using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KindleClippings
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram();
            Console.ReadLine();
        }

        static void RunProgram()
        {
            var parser = new MyClippingsParser();
            var clippings = parser.Parse("My Clippings.txt");
        }
    }
}
