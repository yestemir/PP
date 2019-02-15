using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task3
{
    class Program
    {
        public static void Space(int lvl)
        {
            for (int i = 0; i < lvl; i++)//create an arr to make a spaces between directories and files
            {
                Console.Write("     ");// just write spaces
            }
        }
        public static void Ex(DirectoryInfo dir, int lvl)
        {
            foreach (FileInfo f in dir.GetFiles())//cycle that shows files name
            {
                Space(lvl);
                Console.WriteLine(f.Name);
            }
            foreach (DirectoryInfo d in dir.GetDirectories())//cycle that shows directories(folders) name
            {
                Space(lvl);
                Console.WriteLine(d.Name);
                Ex(d, lvl + 1);
            }
        }
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("C:\\Users\\Gaziza.USER\\source\\week2\\Task3");
            Ex(dir, 0);
            Console.ReadKey();
        }
    }
}