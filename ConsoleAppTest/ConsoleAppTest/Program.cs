using System;
using ClassLibraryForArray;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IntArray temp = new IntArray(2, 6, 0, 0, 0);
            temp.Notify += DisplayMessage;

            IntArray result = IntArray.FindСlosestToAvg(temp);

            Console.WriteLine(result.Length);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(i + " index: " + result[i]);
            }
            Console.ReadKey();
        }
        private static void DisplayMessage(string message) => Console.WriteLine(message);
    }
}
