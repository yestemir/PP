using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = "C:\\Users\\Gaziza.USER\\source\\week2\\Task4\\papka1\\task4.txt";//path1 of txt file
            string path2 = "C:\\Users\\Gaziza.USER\\source\\week2\\Task4\\papka2\\task4.txt";//path2 of txt file

            StreamWriter sw = new StreamWriter(path1);//create a streamwriter 
            sw.Write("dina poluchit po labke full");//we create txt file 
            sw.Close();//close the streamwriter
            FileInfo f = new FileInfo(path1);//create a new fileinfo and send the meaning of path1  

            if (f.Exists)//if file f exists then do following things
            {
                f.CopyTo(path2, true);//we copy txt file from path1 to path2
                f.Delete();//delete the txt file in path1
            }
        }
    }
}