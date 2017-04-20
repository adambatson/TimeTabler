using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class Program {
        static void Main(string[] args) {
            //DateTime Start = new DateTime(0, 0, 0, 8, 30, 0);
            //Day D = new Day("Monday");
            //D.AddTask(new Task("Task 1", new DateTime(2017, 2, 2, 9, 0, 0), new DateTime(2017, 2, 2, 9, 30, 0)));
            //Console.WriteLine(D);
            //D.AddTask(new Task("Task 3", new DateTime(2017, 2, 2, 9, 15, 0), new DateTime(2017, 2, 2, 10, 40, 0)));
            /*Day D = GetKnownEvents();
            Console.WriteLine(D);
            Dictionary<string, TimeSpan> Tasks = GetTasks();
            Console.WriteLine("Tasks to schedule");
            foreach (KeyValuePair<string, TimeSpan> Pair in Tasks) {
                Console.WriteLine(Pair.Key + " " + Pair.Value.Minutes + " minutes");
            }
            Node Init = new Node(D, null, Tasks);
            //List<Node> Nodes = Init.Expand();
            Solve(Init);*/
            Week W = new Week();
            //Console.WriteLine(W);
            //Console.Read();
            Node Init = new Node(W, null, GetTasks());
            Solve(Init);
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

        static Dictionary<string, TimeSpan> GetTasks() {
            Dictionary<string, TimeSpan> Tasks = new Dictionary<string, TimeSpan>();
            while(true) {
                Console.WriteLine("Enter task name");
                string Name = Console.ReadLine();
                int Minutes;
                Console.WriteLine("Enter estimated completion time in minutes");
                int.TryParse(Console.ReadLine(), out Minutes);
                Tasks.Add(Name, new TimeSpan(0, Minutes, 0));

                Console.WriteLine("Would you like to enter more? [Y/n]");
                string Res = Console.ReadLine();
                if (Res == "y" || Res == "Y" || Res == "") continue;
                else break;
            }
            return Tasks;
        }

        static void Solve(Node Initial) {
            SearchStrategy Strat = null;
            Console.WriteLine("Which strategy would you like to use?");
            Console.WriteLine("1) Depth First Search");
            Console.WriteLine("2) Breadth First Search");
            Console.WriteLine("3) Minimize time per day heuristic");
            Console.WriteLine("4) Minimize tasks per day heuristic");
            Console.WriteLine("5) Simulated Annealing");
            int X;
            int.TryParse(Console.ReadLine(), out X);
            switch(X) {
                case 1:
                    Strat = new DepthFirstSearch();
                    break;
                case 2:
                    Strat = new BreadthFirstSearch();
                    break;
                case 3:
                    Strat = new AStarSearch(new LowTimePerDayHeuristic());
                    break;
                case 4:
                    Strat = new AStarSearch(new LowTaskPerDayHeuristic());
                    break;
                case 5:
                    SimulatedAnnealer SA = new SimulatedAnnealer(Initial);
                    Console.WriteLine(SA.Anneal());
                    return;
                default:
                    Console.WriteLine("I don't understand, exiting!");
                    return;
            }
            Console.WriteLine(Strat.Solve(Initial));
        }
    }
}
