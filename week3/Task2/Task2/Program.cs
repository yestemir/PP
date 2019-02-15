using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp2
{
    enum FSIMode//запишем информацию отднльно 
    {
        DirectoryInfo = 1,
        File = 2
    }
    class Lawer
    {
        public DirectoryInfo[] DirCon//создаем новый массив для папок
        {
            get; //через эту операцию читаем переменные 
            set;//даем значение

        }
        public FileInfo[] FileCon
        {
            get;
            set;

        }
        public int selectedIndex
        {
            get;
            set;
        }
        public void Draw()//создаем функцию
        {
            Console.BackgroundColor = ConsoleColor.Black;//закрашивает в черный
            Console.Clear();
            for (int i = 0; i < DirCon.Length; i++)
            {
                if (i == selectedIndex)//если укажем индекс
                {
                    Console.BackgroundColor = ConsoleColor.Red;//закрашиваем в красный
                }
                else
                    Console.BackgroundColor = ConsoleColor.Black;//все остальные на черный
                Console.WriteLine(i + 1 + ". " + DirCon[i].Name);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;// файлы в желтый цвет
            for (int j = 0; j < FileCon.Length; j++)
            {
                if (j + DirCon.Length == selectedIndex)//если укажем выбранный индекс
                {
                    Console.BackgroundColor = ConsoleColor.Red;//выбранный файл закрашиваем в красный 
                }
                else
                    Console.BackgroundColor = ConsoleColor.Black;//все остальные в черный
                Console.WriteLine(j + 1 + DirCon.Length + ". " + FileCon[j].Name);
            }
            Console.ForegroundColor = ConsoleColor.White;

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo Dir = new DirectoryInfo(@"C:\Users\Gaziza.USER\source");
            Lawer l = new Lawer// создаем класс
            {
                DirCon = Dir.GetDirectories(),//для массива DirCon укажем адресс папки в директории Dir
                FileCon = Dir.GetFiles(),
                selectedIndex = 0
            };
            l.Draw();
            FSIMode Mod = FSIMode.DirectoryInfo;//создаем новый FSI(enum) для папок 
            Stack<Lawer> contral = new Stack<Lawer>();//создаем новый стэк
            {
            contral.Push(l);// в стэк добавляем class (1)
            bool work = false;
            while (!work)
                if (Mod == FSIMode.DirectoryInfo)//если FSI(enum) папка  
                {
                    contral.Peek().Draw();//в стэк указываем функцию Draw
                    Console.BackgroundColor = ConsoleColor.Blue;

                    Console.WriteLine("Deleted: Deleted | Rename: R | Back: Bakcspace | Open: Enter");

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;



                }
                ConsoleKeyInfo key = Console.ReadKey();// нажатие клавиш в консоле
                switch (key.Key)//
                {
                    case ConsoleKey.UpArrow:// если стрелка вверх

                        if (contral.Peek().selectedIndex > 0)
                        {
                            contral.Peek().selectedIndex--;// не идет выше самого верхнего элемента 
                        }
                        break;
                    case ConsoleKey.DownArrow://если стрелка вниз 
                        if (contral.Peek().selectedIndex < contral.Peek().DirCon.Length + contral.Peek().FileCon.Length - 1)
                        {
                            contral.Peek().selectedIndex++;//не опускается ниже самого нижнего элемента
                        }
                        break;
                    case ConsoleKey.Enter:// если нажмем Enter 
                        int ind = contral.Peek().selectedIndex;//
                        if (contral.Peek().selectedIndex < contral.Peek().DirCon.Length)//если папка имее индекс
                        {
                            DirectoryInfo di = contral.Peek().DirCon[contral.Peek().selectedIndex];//создаем новую папку с таким индексом
                            Lawer nl = new Lawer// создаем новый класс Lawer
                            {
                                DirCon = di.GetDirectories(),//указываем адрес папки в Диркон
                                FileCon = di.GetFiles(),//указываем адрес файла в Файлкон
                                selectedIndex = 0
                            };
                            contral.Push(nl);//добавляем в стэк н1
                        }
                        else
                        {
                            Mod = FSIMode.File;// Если FSI(enum) файл
                            FileStream fl = new FileStream(contral.Peek().FileCon[contral.Peek().selectedIndex - contral.Peek().DirCon.Length].FullName, FileMode.Open, FileAccess.Read);
                            // берем индексы файлов
                            StreamReader sr = new StreamReader(fl);
                            Console.BackgroundColor = ConsoleColor.Black;//закрашиваем файлы в черный
                            Console.ForegroundColor = ConsoleColor.White;//информацию в файле в белый
                            Console.Clear();
                            Console.WriteLine(sr.ReadToEnd());//читаем инфу в файле
                            fl.Close();
                            sr.Close();
                        }
                        break;
                    case ConsoleKey.Backspace://если нажмем Backspace
                        if (Mod == FSIMode.DirectoryInfo)//если FSI(enum) папка
                        {
                            contral.Pop();
                        }
                        else //если FSI(enum) файл
                        {
                            Mod = FSIMode.DirectoryInfo;//вернет первую папку 
                        }
                        break;

                    case ConsoleKey.Escape://если нажмем Escape-
                        work = true;
                        break;
                    case ConsoleKey.Delete://если нажмем Delete
                        int index = contral.Peek().selectedIndex;
                        int a = contral.Peek().DirCon.Length;
                        int b = contral.Peek().FileCon.Length;
                        if (index < a)
                        {
                            Directory.Delete(contral.Peek().DirCon[index].FullName);//удаляем папку с таким индексом
                        }
                        else
                        {
                            File.Delete(contral.Peek().FileCon[index - a].FullName);//удаляем файл с таким индексом
                        }
                        contral.Pop();
                        if (contral.Count == 0)//Если в последней части
                        {
                            Lawer nl = new Lawer//создаем новый класс 
                            {
                                DirCon = Dir.GetDirectories(),//
                                FileCon = Dir.GetFiles(),//
                                selectedIndex = 0
                            };
                            contral.Push(nl);//в стэк н1
                        }
                        else//если не в начальной части
                        {
                            DirectoryInfo dif = contral.Peek().DirCon[index];
                            Lawer nl = new Lawer
                            {
                                DirCon = dif.GetDirectories(),
                                FileCon = dif.GetFiles(),
                                selectedIndex = 0
                            };
                            contral.Push(nl);//добавляем в стэк
                        }
                        break;
                    case ConsoleKey.R://если нажмем Р
                        index = contral.Peek().selectedIndex;
                        a = contral.Peek().DirCon.Length;
                        b = contral.Peek().FileCon.Length;
                        string name, fullname;
                        int IndexMode;
                        if (index < a)// если индекс указывает на папку
                        {
                            name = contral.Peek().DirCon[index].Name;//читаем название папки как строка
                            fullname = contral.Peek().DirCon[index].FullName;
                            IndexMode = 1;
                        }
                        else
                        {
                            name = contral.Peek().FileCon[index - a].Name;//читаем название файла
                            fullname = contral.Peek().FileCon[index - a].FullName;//фулл нейм
                            IndexMode = 2;
                        }
                        fullname = fullname.Remove(fullname.Length - name.Length);//создаем адрес
                        Console.WriteLine("ename: Please to write a new name:");
                        string newname = Console.ReadLine();//название папки или файла
                        if (IndexMode == 1)//если папка
                        {
                            new DirectoryInfo(contral.Peek().DirCon[index].FullName).MoveTo(fullname + newname);//меняем название папки
                        }
                        else
                        {
                            new FileInfo(contral.Peek().FileCon[index - a].FullName).MoveTo(fullname + newname);//меняем название файла
                        }
                        contral.Pop();//показываем последний элемент стэка
                        if (contral.Count == 0)//если стэк пустой
                        {
                            Lawer nl = new Lawer//создаем новый класс
                            {
                                DirCon = Dir.GetDirectories(),
                                FileCon = Dir.GetFiles(),
                                selectedIndex = 0
                            };
                            contral.Push(nl);//добавляем в класс
                        }
                        else//если стэк не пустой
                        {
                            DirectoryInfo dif = contral.Peek().DirCon[index];
                            Lawer nl = new Lawer
                            {
                                DirCon = dif.GetDirectories(),
                                FileCon = dif.GetFiles(),
                                selectedIndex = 0
                            };
                            contral.Push(nl);
                        }
                        break;


                }

            }
        }
    }
}
