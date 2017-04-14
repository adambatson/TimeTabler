using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class Program {
        static void Main(string[] args) {
            //Day D = new Day("Monday");
            //D.AddTask(new Task("Task 1", new DateTime(2017, 2, 2, 9, 0, 0), new DateTime(2017, 2, 2, 9, 30, 0)));
            //Console.WriteLine(D);
            //D.AddTask(new Task("Task 3", new DateTime(2017, 2, 2, 9, 15, 0), new DateTime(2017, 2, 2, 10, 40, 0)));
            Day D = GetKnownEvents();
            Console.WriteLine(D);
            Console.Read();
        }

        static Day GetKnownEvents() {
            Day D = new Day("Today");

            do {
                Console.WriteLine("Enter Task Name");
                string Name = Console.ReadLine();
                Console.WriteLine("Enter Start Time");
                string Start = Console.ReadLine();
                Console.WriteLine("Enter End Time");
                string End = Console.ReadLine();

                DateTime StartTime = DateTime.ParseExact(Start, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                DateTime EndTime = DateTime.ParseExact(End, "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                D.AddTask(new Task(Name, StartTime, EndTime));

                Console.WriteLine("Would you like to enter more? [Y/n]");
                string Res = Console.ReadLine();

                if (Res == "y" || Res == "Y" || Res == "") continue;
                else break;
            } while (true);
            return D;
        }
    }
}
