using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class Node {

        public Day Day { get; private set; }
        public Node Parent { get; private set; }
        public Dictionary<string, TimeSpan> TasksToSched { get; private set; }

        public Node(Day Day, Node Parent, Dictionary<string, TimeSpan> TasksToSched) {
            this.Day = Day;
            this.Parent = Parent;
            this.TasksToSched = TasksToSched;
        }

        public List<Node> Expand() {
            List<Node> Nodes = new List<Node>();

            DateTime Now = DateTime.Now;

            foreach(KeyValuePair<string, TimeSpan> Pair in TasksToSched) {
                DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day, 8, 30, 0);
                DateTime DayEnd = new DateTime(Now.Year, Now.Month, Now.Day, 20, 0, 0);
                while(Start < DayEnd) {
                    Day NewDay = new Day(Day);
                    Task Task = new Task(Pair.Key, Start, Pair.Value);
                    bool Res = NewDay.AddTask(Task);
                    if (Res) {
                        Dictionary<string, TimeSpan> NewTasks = new Dictionary<string, TimeSpan>(TasksToSched);
                        NewTasks.Remove(Pair.Key);
                        Nodes.Add(new Node(NewDay, this, NewTasks));
                    }
                    Start = Start.Add(new TimeSpan(0, 30, 0));
                }
            }
            return Nodes;
        }

    }
}
