using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibraryForArray
{
    public class IntArray
    {
        public delegate void IntArrayHandler(string message);
        private static event IntArrayHandler _notify;
        // the Notify event, which allows you to inform the application about which method has worked.
        public event IntArrayHandler Notify
        {
            // accessory for adding handlers
            add { _notify += value; }
            // accessory for removing handlers
            remove { _notify -= value; }
        }

        // private one-dimensional array
        private int[] a;
        // privatelength
        private int length;

        // property: array length
        public int Length { get; set; }
        // indexer
        public int this[int i]
        {
            get
            {
                if (i >= 0 && i < length)
                    return a[i];
                else
                    throw new IndexOutOfRangeException();
            }
            set
            {
                if (i >= 0 && i < length)
                    a[i] = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        // constructor for creating an array of a given length
        public IntArray(int length)
        {
            a = new int[length];
            this.length = length;
        }
        // constructor with a variable number of parameters
        public IntArray(params int[] arr)
        {
            length = arr.Length;
            a = new int[length];

            for (int i = 0; i < length; i++)
                a[i] = arr[i];
        }

        // creating an array of length "length" and filling it with random integers in the range from a to b
        public static IntArray RandomIntArray(int length, int a, int b)
        {
            IntArray result = new IntArray(length);
            Random rand = new Random();

            for (int i = 0; i < length; i++)
                result[i] = rand.Next(a, b);

            _notify?.Invoke($"The RandomIntArray method has worked. Created an array of long {length} in the range [{a}; {b}]");
            return result;
        }
        // entering an array from a text file named "filename"
        public static IntArray ArrayFromTextFile(string fileName)
        {
            try
            {
                StreamReader file = new StreamReader(fileName);
                string row;
                string[] buff;
                int tempLength = 0;

                while ((row = file.ReadLine()) != null)
                    tempLength += row.Split(' ').Length;


                file.BaseStream.Position = 0;
                IntArray result = new IntArray(tempLength);

                while ((row = file.ReadLine()) != null)
                {
                    buff = row.Split(' ');

                    for (int i = 0; i < buff.Length; i++)
                    {
                        result[i] = Convert.ToInt32(buff[i]);
                    }
                }

                file.Close();
                _notify?.Invoke("The ArrayFromTextFile method has worked");
                return result;
            }
            catch (FileNotFoundException e)
            {
                throw new Exception(e.Message);
                throw new Exception("ERROR. CHECK THE FILENAME");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        // output of the arr array to a text file named "filename"
        public static void ArrayToTextFile(IntArray arr, string fileName)
        {
            try
            {
                StreamWriter file = new StreamWriter(fileName);

                for (int i = 0; i < arr.length; i++)
                    file.Write(arr[i] + " ");

                file.Close();
                _notify?.Invoke("The ArrayToTextFile method has worked");
            }
            catch (FileNotFoundException e)
            {
                throw new Exception(e.Message);
                throw new Exception("ERROR. CHECK THE FILENAME");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        // calculating the sum of arr array elements
        public static int SumArray(IntArray arr)
        {
            int result = 0;

            for (int i = 0; i < arr.length; i++)
                result += arr[i];

            _notify?.Invoke($"The SumArray method has worked. The sum of the elements of the array is {result}");
            return result;
        }
        // ++: increment: increment by 1 of all array elements
        public static IntArray operator ++(IntArray arr)
        {
            for (int i = 0; i < arr.length; i++)
                arr[i]++;

            return arr;
        }
        // +: addition of an array x with a scalar y
        public static IntArray operator +(IntArray x, int y)
        {
            for (int i = 0; i < x.length; i++)
                x[i] += y;

            return x;
        }
        // +: addition of a scalar x with an array y
        public static IntArray operator +(int x, IntArray y)
        {
            IntArray result = new IntArray(y.length);

            for (int i = 0; i < y.length; i++)
                result[i] = x + y[i];

            return result;
        }
        // +: addition of two arrays x and y
        public static IntArray operator +(IntArray x, IntArray y)
        {
            if (x.length == y.length)
            {
                IntArray result = new IntArray(x.length);

                for (int i = 0; i < x.length; i++)
                    result[i] = x[i] + y[i];

                return result;
            }
            else
            {
                throw new Exception("ERROR. THE SIZES OF THE ARRAYS DO NOT MATCH: " + x.length + " != " + y.length);
            }
        }
        // --: decrement: decrease by 1 of all array elements
        public static IntArray operator --(IntArray arr)
        {
            for (int i = 0; i < arr.length; i++)
                arr[i]--;

            return arr;
        }
        // -: subtraction from the array x of the scalar y (x - y)
        public static IntArray operator -(IntArray x, int y)
        {
            for (int i = 0; i < x.length; i++)
                x[i] -= y;

            return x;
        }
        // -: subtraction from the scalar x of the array y (x - y)
        public static IntArray operator -(int x, IntArray y)
        {
            IntArray result = new IntArray(y.length);

            for (int i = 0; i < y.length; i++)
                result[i] = x - y[i];

            return result;
        }
        // -: subtraction from array x of array y(x - y)
        public static IntArray operator -(IntArray x, IntArray y)
        {
            if (x.length == y.length)
            {
                IntArray result = new IntArray(x.length);

                for (int i = 0; i < x.length; i++)
                    result[i] = x[i] - y[i];

                return result;
            }
            else
            {
                throw new Exception("ERROR. THE SIZES OF THE ARRAYS DO NOT MATCH: " + x.length + " != " + y.length);
            }
        }

        public static void print(IntArray x)
        {
            for (int i = 0; i < x.length; i++)
            {
                Console.WriteLine(x[i]);
            }
        }
    }
}
