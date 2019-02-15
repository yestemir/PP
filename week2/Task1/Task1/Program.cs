using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1
{
    class Program
    {
        public static bool ispalindrome(string s)
        {
            for (int i = 0, j = s.Length - 1; i <= j; i++, j--)//cycle that i begins from left and j begins from bottom
            {
                if (s[i] != s[j]) return false;//check the length and if they are not equal return false
            }
            return true;//else if they equal return true
        }
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("task1.txt");//create a new input file
            string s = sr.ReadToEnd();//read from beginning till end
            if (ispalindrome(s) == true)//if string palindrome 
            {
                Console.WriteLine("Yes");//write yes
            }
            else Console.WriteLine("No");//write no
            Console.ReadKey();
        }
    }
}