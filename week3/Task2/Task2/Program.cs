using System;
using System.Collections.Generic;
using System.IO;
namespace Task2
{
    enum FSIMode // listing declaration
    {
        DirectoryInfo = 1,
        File = 2
    }
    class Layer// create a class
    {
        public DirectoryInfo[] DirectoryContent//property declaration
        {
            get;
            set;
        }
        public FileInfo[] FileContent//property declaration
        {
            get;
            set;
        }
        public int SelectedIndex//property declaration
        {
            get;
            set;
        }
        void SelectedColor(int i)
        {
            if (i == SelectedIndex)
                Console.BackgroundColor = ConsoleColor.Red;
            else
                Console.BackgroundColor = ConsoleColor.Black;
        }
        public void Draw()//create a method for manipulating with colors
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            for (int i = 0; i < DirectoryContent.Length; ++i)
            {
                SelectedColor(i);
                Console.WriteLine((i + 1) + ". " + DirectoryContent[i].Name);
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < FileContent.Length; ++i)
            {
                SelectedColor(i + DirectoryContent.Length);
                Console.WriteLine((i + DirectoryContent.Length + 1) + ". " + FileContent[i].Name);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("/Users/meruyerttastandiyeva/Desktop");//make a reference to a directory

            Layer l = new Layer//call constructor with parameters
            {
                DirectoryContent = dir.GetDirectories(),
                FileContent = dir.GetFiles(),
                SelectedIndex = 0
            };

            Stack<Layer> history = new Stack<Layer>();//create a stack using the constructor
            history.Push(l);//insert an element at the top of the stack
            bool esc = false;//create a boolean variable
            FSIMode curMode = FSIMode.DirectoryInfo;
            while (!esc)//use while loop for executing a statements while a specified boolean expression is true
            {
                if (curMode == FSIMode.DirectoryInfo)
                {
                    history.Peek().Draw();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKeyInfo consolekeyInfo = Console.ReadKey();//create a variable that identifies the console key that was pressed
                switch (consolekeyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        history.Peek().SelectedIndex--;//incrementing index
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SelectedIndex++;//decrementing index
                        break;
                    case ConsoleKey.Enter:
                        int index = history.Peek().SelectedIndex;
                        if (index < history.Peek().DirectoryContent.Length)
                        {
                            DirectoryInfo d = history.Peek().DirectoryContent[index];
                            history.Push(new Layer//insert an element at the top of the stack
                            {
                                DirectoryContent = d.GetDirectories(),
                                FileContent = d.GetFiles(),
                                SelectedIndex = 0
                            });
                        }
                        else//if it is a textfile we enter to it
                        {
                            curMode = FSIMode.File;
                            using (FileStream fs = new FileStream(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName, FileMode.Open, FileAccess.Read))//open a textfile to read from it
                            {
                                using (StreamReader sr = new StreamReader(fs))//create a stream reader and link it to the file stream
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Clear();
                                    Console.WriteLine(sr.ReadToEnd());//read all the data from textfile and display it
                                 }//"using" statement closes the stream
                            }

                        }
                        break;
                    case ConsoleKey.Backspace:
                        if (curMode == FSIMode.DirectoryInfo)
                        {
                            if (history.Count > 1)
                                history.Pop();//removes and returns the object at the top of the stack
                        }
                        else
                        {
                            curMode = FSIMode.DirectoryInfo;
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case ConsoleKey.Escape:
                        esc = true;
                        break;
                    case ConsoleKey.Delete:
                        index = history.Peek().SelectedIndex;
                        int ind = index;
                        if (index < history.Peek().DirectoryContent.Length)
                            history.Peek().DirectoryContent[index].Delete(true);//delete a directory
                        else
                            history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Delete();//delete a file
                        int numofcontent = history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 2;
                        history.Pop();//removes and returns the object at the top of the stack
                        if (history.Count == 0)
                        {
                            Layer nl = new Layer
                            {
                                DirectoryContent = dir.GetDirectories(),
                                FileContent = dir.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);//insert an element at the top of the stack
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex;
                            DirectoryInfo di = history.Peek().DirectoryContent[index];
                            Layer nl = new Layer
                            {
                                DirectoryContent = di.GetDirectories(),
                                FileContent = di.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);//insert an element at the top of the stack
                        }
                        break;
                    case ConsoleKey.A:
                        index = history.Peek().SelectedIndex;
                        string name, fullname;
                        int selectedMode;
                        if (index < history.Peek().DirectoryContent.Length)
                        {
                            name = history.Peek().DirectoryContent[index].Name;
                            fullname = history.Peek().DirectoryContent[index].FullName;
                            selectedMode = 1;
                        }
                        else
                        {
                            name = history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].Name;
                            fullname = history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName;
                            selectedMode = 2;
                        }
                        fullname = fullname.Remove(fullname.Length - name.Length);
                        Console.WriteLine("Please enter the new name, to rename {0}:", name);
                        Console.WriteLine(fullname);
                        string newname = Console.ReadLine();
                        while (newname.Length == 0)
                        {
                            Console.WriteLine("This directory was created, Enter the new one");
                            newname = Console.ReadLine();
                        }
                        Console.WriteLine(selectedMode);
                        if (selectedMode == 1)
                        {
                            new DirectoryInfo(history.Peek().DirectoryContent[index].FullName).MoveTo(fullname + newname);//renames a directory
                        }
                        else
                            new FileInfo(history.Peek().FileContent[index - history.Peek().DirectoryContent.Length].FullName).MoveTo(fullname + newname);//renames a file
                        index = history.Peek().SelectedIndex;
                        ind = index;
                        numofcontent = history.Peek().DirectoryContent.Length + history.Peek().FileContent.Length - 1;
                        history.Pop();//removes and returns the object at the top of the stack
                        if (history.Count == 0)
                        {
                            Layer nl = new Layer
                            {
                                DirectoryContent = dir.GetDirectories(),
                                FileContent = dir.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        else
                        {
                            index = history.Peek().SelectedIndex;
                            DirectoryInfo di = history.Peek().DirectoryContent[index];
                            Layer nl = new Layer
                            {
                                DirectoryContent = di.GetDirectories(),
                                FileContent = di.GetFiles(),
                                SelectedIndex = Math.Min(Math.Max(numofcontent, 0), ind)
                            };
                            history.Push(nl);
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}