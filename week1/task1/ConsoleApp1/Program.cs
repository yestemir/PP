using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        public static bool Isprime(int a) //Method to determine the prime number
        {
            if (a == 1)
                return false;
            int cnt = 0;
            for (int i = 2; i <= a; i++)
            {
                if (a % i == 0) cnt++;
            }
            if (cnt == 1) return true;
            return false;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); //set the size of the array and declare the array 
            int[] a = new int[n];
            int x = n;
            string[] arr = Console.ReadLine().Split(); //write the values ​​of the type string into the array
            for (int i = 0; i < n; i++) //change string to integer 
            {
                a[i] = int.Parse(arr[i]);
            }
            for (int i = 0; i < n; i++)
            {
                if (!Isprime(a[i]))
                    x--;
            }
            Console.WriteLine(x);
            for (int i = 0; i < a.Length; i++)//checking all numbers in array with function isPrime
            {
                if (Isprime(a[i]))
                    Console.Write(a[i] + " ");
            }
        }
    }
}