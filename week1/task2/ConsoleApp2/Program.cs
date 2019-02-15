using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Student//create a class Student
    {
        string name;
        string id;
        int year_of_study;

        public Student() { }
        //Создали контруктор который по умолчанию. Просто если создать еще один контруктор нужно будет создать тот что по умолчанию заново
        //Created a default constructor.Just if you create another constructor, you will need to create one that defaults again.
        public Student(string name, string id)//constructor that helps to give the values
        {
            this.name = name;
            this.id = id;
        }
        public void set_name(string name)//Method which can change the value of variable "name"
        {
            this.name = name;
        }
        public void set_id(string id)
        {
            this.id = id;
        }
        public string get_name()
        {
            return name;
        }
        public string get_id()
        {
            return id;
        }
        public int add_the_year_of_study(int year)//make a method which increase value year_of_study and assigns him to change year of study 
        {
            year_of_study = year + 1;
            return year_of_study;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Student s = new Student("Maxim", "18BD101010");
            Console.WriteLine(s.get_name());
            Console.WriteLine(s.get_id());
            Console.WriteLine(s.add_the_year_of_study(2018));
        }
    }
}