using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryForArray;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntArray temp = new IntArray(1, 2, 3, 4);
            temp.Notify += DisplayMessage;

            IntArray.SumArray(temp);
            IntArray temp2 = IntArray.ArrayFromTextFile("D:\\input.txt");
            temp2.Notify += DisplayMessage;
            IntArray.print(temp2);
        }
        private static void DisplayMessage(string message) => Console.WriteLine(message);
    }
}
