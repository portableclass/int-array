using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ClassLibraryForArray
{
    public class IntArray
    {
        public delegate void IntArrayHandler(string message);
        private static event IntArrayHandler _notify;
        // the Notify event, which allows you to inform the application about which method has worked.
        public static event IntArrayHandler Notify
        {
            // accessory for adding handlers
            add { _notify += value; }
            // accessory for removing handlers
            remove { _notify -= value; }
        }

        // private one-dimensional array
        private int[] a;
        // private  array length
        private int length;

        // public array length
        public int Length
        {
            get { return length; }
        }
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
        /// <summary>
        /// creating an array of length "length" and filling it with random integers in the range from a to b
        /// </summary>
        /// <param name="length">array length</param>
        /// <param name="a">beginning of the range</param>
        /// <param name="b">end of range</param>
        /// <returns></returns>
        public static IntArray RandomIntArray(int length, int a, int b)
        {
            IntArray result = new IntArray(length);
            Random rand = new Random();

            for (int i = 0; i < length; i++)
                result[i] = rand.Next(a, b+1);

            _notify?.Invoke($"The RandomIntArray method has worked. Created an array of length {length} in the range [{a}; {b}]");
            return result;
        }
        /// <summary>
        /// entering an array from a text file named "filename"
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns></returns>
        public static IntArray ArrayFromTextFile(string fileName)
        {
            StreamReader file = new StreamReader(fileName);
            string row;
            string[] buff;
            int tempLength = 0;
            int length = 0;
            int j;

            while ((row = file.ReadLine()) != null)
                length += Regex.Replace(row.Trim(' '), "\\s+", " ").Split(' ').Length;

            file.BaseStream.Position = 0;
            IntArray result = new IntArray(length);

            while ((row = file.ReadLine()) != null)
            {
                row = Regex.Replace(row.Trim(' '), "\\s+", " ");
                buff = row.Split(' ');
                tempLength += buff.Length;
                j = 0;

                for (int i = tempLength - buff.Length; i < tempLength; i++)
                {
                    result[i] = Convert.ToInt32(buff[j]);
                    j++;
                }
            }

            file.Close();
            _notify?.Invoke("The ArrayFromTextFile method has worked");
            return result;
        }
        /// <summary>
        /// output of the arr array to a text file named "filename"
        /// </summary>
        /// <param name="arr">array to write</param>
        /// <param name="fileName">file name</param>
        public static void ArrayToTextFile(IntArray arr, string fileName)
        {
            StreamWriter file = new StreamWriter(fileName);

            for (int i = 0; i < arr.length; i++)
                file.Write(arr[i] + " ");

            file.Close();
            _notify?.Invoke("The ArrayToTextFile method has worked");
        }
        /// <summary>
        /// calculating the sum of arr array elements
        /// </summary>
        /// <param name="arr">array to calculate</param>
        /// <returns></returns>
        public static int SumArray(IntArray arr)
        {
            int result = 0;

            for (int i = 0; i < arr.length; i++)
                result += arr[i];

            _notify?.Invoke($"The SumArray method has worked. The sum of the elements of the array is {result}");
            return result;
        }
        /// <summary>
        /// search for the number of elements in an array that are multiples of a given number
        /// </summary>
        /// <param name="arr">array</param>
        /// <param name="x">a number to search for multiples of it</param>
        /// <returns></returns>
        public static int CountMultiples(IntArray arr, int x)
        {
            int count = 0;
            for (int i = 0; i < arr.length; i++)
            {
                if (arr[i] % x == 0)
                {
                    count++;
                }
            }
            _notify?.Invoke($"The CountMultiples method has worked, the number of multiples of elements: {count}");
            return count;
        }
        /// <summary>
        /// a method for searching an array for the index
        /// of the element whose value is closest to the arithmetic mean 
        /// of the array elements. 
        /// if there are several such elements, 
        /// the method will return the indexes 
        /// of all those close to the arithmetic mean.
        /// </summary>
        /// <param name="arr">array</param>
        /// <returns></returns>
        public static IntArray FindСlosestToAvg(double[] arr)
        {
            string message = "";
            double sumArray = 0;

            for (int i = 0; i < arr.Length; i++)
                sumArray += arr[i];

            double avg = sumArray / arr.Length;
            double minDifference = Math.Abs(arr[0] - avg);
            List<int> index = new List<int>();

            for (int i = 0; i < arr.Length; i++)
                if (Math.Abs(arr[i] - avg) < minDifference)
                    minDifference = Math.Abs(arr[i] - avg);

            for (int i = 0; i < arr.Length; i++)
                if (Math.Abs(arr[i] - avg) == minDifference)
                    index.Add(i);

            IntArray result = new IntArray(index.Count);

            for (int i = 0; i < result.length; i++)
            {
                result[i] = index[i];
                message += $" {result[i]};";
            }

            _notify?.Invoke($"The FindСlosestToAvg method has worked. The elements closest to the arithmetic mean have indexes:{message}");
            return result;
        }
        // ++: increment: increment by 1 of all array elements
        public static IntArray operator ++(IntArray arr)
        {
            for (int i = 0; i < arr.length; i++)
                arr[i]++;

            _notify?.Invoke($"Increment by 1 of all array elements");
            return arr;
        }
        // +: addition of an array x with a scalar y
        public static IntArray operator +(IntArray x, int y)
        {
            for (int i = 0; i < x.length; i++)
                x[i] += y;

            _notify?.Invoke($"Addition of an array x with a scalar y");
            return x;
        }
        // +: addition of a scalar x with an array y
        public static IntArray operator +(int x, IntArray y)
        {
            IntArray result = new IntArray(y.length);

            for (int i = 0; i < y.length; i++)
                result[i] = x + y[i];

            _notify?.Invoke($"Addition of a scalar x with an array y");
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

                _notify?.Invoke($"Addition of two arrays x and y");
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

            _notify?.Invoke($"Decrease by 1 of all array elements");
            return arr;
        }
        // -: subtraction from the array x of the scalar y (x - y)
        public static IntArray operator -(IntArray x, int y)
        {
            for (int i = 0; i < x.length; i++)
                x[i] -= y;

            _notify?.Invoke($"Subtraction from the array x of the scalar y (x - y)");
            return x;
        }
        // -: subtraction from the scalar x of the array y (x - y)
        public static IntArray operator -(int x, IntArray y)
        {
            IntArray result = new IntArray(y.length);

            for (int i = 0; i < y.length; i++)
                result[i] = x - y[i];

            _notify?.Invoke($"Subtraction from the scalar x of the array y (x - y)");
            return result;
        }
        // -: subtraction from array x of array y (x - y)
        public static IntArray operator -(IntArray x, IntArray y)
        {
            if (x.length == y.length)
            {
                IntArray result = new IntArray(x.length);

                for (int i = 0; i < x.length; i++)
                    result[i] = x[i] - y[i];

                _notify?.Invoke($"Subtraction from array x of array y (x - y)");
                return result;
            }
            else
            {
                throw new Exception("ERROR. THE SIZES OF THE ARRAYS DO NOT MATCH: " + x.length + " != " + y.length);
            }
        }
    }
}
