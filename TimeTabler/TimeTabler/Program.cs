using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class Program {
        static void Main(string[] args) {
            Day D = new Day("Monday");
            D.AddTask(new Task("Task 1", new DateTime(2017, 2, 2, 9, 0, 0), new DateTime(2017, 2, 2, 9, 30, 0)));
            Console.WriteLine(D);
            Console.Read();
        }
    }
}
