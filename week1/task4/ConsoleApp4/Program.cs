using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[,] s = new string[100, 100];//make 2d array

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    s[i, j] = "[*]";//fill the array
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    Console.Write(s[i, j]);//Print the values ​​of the array
                }
                Console.WriteLine();//After each execution of the inner loop, we start from a kinew line.
            }
            Console.ReadKey();
        }
    }
}