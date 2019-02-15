using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp2
{
    enum FSIMode//запимешм инофрмация раздельно как справочник
    {
        DirectoryInfo = 1,
        File = 2
    }
    class Lawer
    {
        public DirectoryInfo[] DirCon//создаем массив для папок
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
            Console.BackgroundColor = ConsoleColor.Black;//фон командной строки
            Console.Clear();//очищаем всю информацию
            for (int i = 0; i < DirCon.Length; i++)
            {
                if (i == selectedIndex)//выбранная пака/файл (строка)
                {
                    Console.BackgroundColor = ConsoleColor.Red;//то этот индекс будет закрашен на красный цвет
                }
                else
                    Console.BackgroundColor = ConsoleColor.Black;//остальные папки закрашены на черный
                Console.WriteLine(i + 1 + ". " + DirCon[i].Name);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;// файлы отмечаем желтым цветом
            for (int j = 0; j < FileCon.Length; j++)
            {
                if (j + DirCon.Length == selectedIndex)//если показываем выбранный индекс
                {
                    Console.BackgroundColor = ConsoleColor.Red;//выбранный файл закрашиваем на красный
                }
                else
                    Console.BackgroundColor = ConsoleColor.Black;//все остальные черным цветом
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
            Lawer l = new Lawer// создаем класс,который показывает папки, файлы и индексы
            {
                DirCon = Dir.GetDirectories(),//для массива DirCon в дирекории Dir показываем адрес папки
                FileCon = Dir.GetFiles(),
                selectedIndex = 0
            };
            l.Draw();
            FSIMode Mod = FSIMode.DirectoryInfo;//создаем новый FSI(enum) для папок
            Stack<Lawer> contral = new Stack<Lawer>();//создаем стэк 
            contral.Push(l);// в стэк добавляем class(l)
            bool work = false;
            while (!work)
            {
                if (Mod == FSIMode.DirectoryInfo)//если FSI(enum) папка   
                {
                    contral.Peek().Draw();//в стэке показываем функцию Draw
                }
                ConsoleKeyInfo key = Console.ReadKey();// описывает нажатие клавиш в консоле
                switch (key.Key)//
                {
                    case ConsoleKey.UpArrow://если нажимаем стрелку вверх

                        if (contral.Peek().selectedIndex > 0)
                        {
                            contral.Peek().selectedIndex--;// красный курсор не идет выше самого первого элемента в консоле 
                        }
                        break;
                    case ConsoleKey.DownArrow://если нажимаем стрелку вниз
                        if (contral.Peek().selectedIndex < contral.Peek().DirCon.Length + contral.Peek().FileCon.Length - 1)
                        {
                            contral.Peek().selectedIndex++;//красный курсор не идет ниже самого нижнего элемента в консоле
                        }
                        break;
                    case ConsoleKey.Enter:// если нажмем ентер
                        int ind = contral.Peek().selectedIndex;//
                        if (contral.Peek().selectedIndex < contral.Peek().DirCon.Length)//если папка имеет индекс 
                        {
                            DirectoryInfo di = contral.Peek().DirCon[contral.Peek().selectedIndex];//создаем новую папку с таким же индексом
                            Lawer nl = new Lawer// создаем новый класс
                            {
                                DirCon = di.GetDirectories(),//в массиве Диркон показываем адрес папки ди
                                FileCon = di.GetFiles(),//в массиве Файлкон показываем адрес папки ди
                                selectedIndex = 0
                            };
                            contral.Push(nl);// в стэк добавляем класс nl  
                        }
                        else
                        {
                            Mod = FSIMode.File;// Если FSI(enum) файл
                            FileStream fl = new FileStream(contral.Peek().FileCon[contral.Peek().selectedIndex - contral.Peek().DirCon.Length].FullName, FileMode.Open, FileAccess.Read);
                            // берем индексы файла
                            StreamReader sr = new StreamReader(fl);
                            Console.BackgroundColor = ConsoleColor.Black;//закрашивает файл в черный цвет
                            Console.ForegroundColor = ConsoleColor.White;//информацию в файле закрашивает в белы
                            Console.Clear();//очищаем 
                            Console.WriteLine(sr.ReadToEnd());//читаем содержимое в файле 
                            fl.Close();
                            sr.Close();
                        }
                        break;
                    case ConsoleKey.Backspace://если нажмем Backspacе
                        if (Mod == FSIMode.DirectoryInfo)//если FSI(enum) папка
                        {
                            contral.Pop();
                        }
                        else //если FSI(enum) файл
                        {
                            Mod = FSIMode.DirectoryInfo;
                        }
                        break;

                    case ConsoleKey.Escape://если нажмем Escapе
                        work = true;//консоль останавливает работу
                        break;
                }

            }
        }
    }
}