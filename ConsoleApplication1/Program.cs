using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = Convert.ToInt32(Console.ReadLine());
            List<Person> persons = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                var line = Console.ReadLine().Split(' ');
                int nameIndx = FindLastNameIndx(persons, line[2]);
                if (nameIndx == -1 || persons[nameIndx].timeOut > 0 && persons[nameIndx].timeIn >= 0) //нет имени в списке или посетитель уже был
                    persons.Add(new Person(line[2], Convert.ToInt32(line[1])));            
                else
                {
                    persons[nameIndx].timeOut = Convert.ToInt32(line[1]);
                }
            }
            Console.WriteLine(GetSumTime(persons));
            Console.WriteLine(GetNumPers(persons));
        }

        class Person
        {
            public string name; 
            public int timeIn; 
            public int timeOut; 

            public Person(string name, int timeIn)
            {
                this.name = name;
                this.timeIn = timeIn;
                timeOut = 0;
            }
        }

        static int FindLastNameIndx(List<Person> person, string name) //поиск последнего имени
        {
            if (person == null)
                return -1;
            int indx = -1;
            for (int i = 0; i < person.Count; i++)
            {
                if (person[i].name == name)
                    indx = i;
            }
            return indx;
        }
        static int GetSumTime(List<Person> person)
        {
            if (person == null)
                return -1;
            int summ = 0;
            int max = person[0].timeOut;
            int min = person[0].timeIn;
            for (int i = 1; i < person.Count; i++)
            {
                if (person[i].timeIn <= max && person[i].timeOut >= max)
                    max = person[i].timeOut;
                else if (person[i].timeIn >= max)
                {
                    summ += max - min;
                    min = person[i].timeIn;
                    max = person[i].timeOut;
                }
                 if (i == person.Count - 1)
                    summ += max - min;
            }
            return summ;
        }

        static int GetNumPers(List<Person> person)
        {
            if (person == null)
                return -1;
            int [] temp = new int[person.Count];
            for (int i = 0; i < person.Count; i++)
            {
                int min = person[i].timeIn;
                int max = person[i].timeOut;
                for (int j = i; j < person.Count; j++)
                {
                    if (person[j].timeIn < max)
                        temp[i] += 1;
                }

            }
            return temp.Max();
        }
    }
}
