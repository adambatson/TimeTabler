using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class Node {

        public Week Week { get; private set; }
        public Node Parent { get; private set; }
        public Dictionary<string, TimeSpan> TasksToSched { get; private set; }

        public Node(Week Week, Node Parent, Dictionary<string, TimeSpan> TasksToSched) {
            this.Week = Week;
            this.Parent = Parent;
            this.TasksToSched = TasksToSched;
        }

        public List<Node> Expand() {
            /*List<Node> Nodes = new List<Node>();

            DateTime Now = DateTime.Now;

            foreach(KeyValuePair<string, TimeSpan> Pair in TasksToSched) {
                DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day, 8, 30, 0);
                DateTime DayEnd = new DateTime(Now.Year, Now.Month, Now.Day, 20, 0, 0);
                while(Start < DayEnd) {
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
            return Nodes;*/
            List<Node> Nodes = new List<Node>();
            DateTime Now = DateTime.Now;

            foreach(KeyValuePair<string, TimeSpan> Pair in TasksToSched) {
                for(int i = 0; i < 7; i++) {
                    DateTime Start = new DateTime(Now.Year, Now.Month, Now.Day, 8, 30, 0);
                    DateTime DayEnd = new DateTime(Now.Year, Now.Month, Now.Day, 22, 0, 0);
                    while(Start < DayEnd) {
                        Week NewWeek = new Week(Week);
                        Task Task = new Task(Pair.Key, Start, Pair.Value);
                        bool Res = NewWeek.Days[i].AddTask(Task);
                        if(Res) {
                            Dictionary<string, TimeSpan> NewTasks = new Dictionary<string, TimeSpan>(TasksToSched);
                            NewTasks.Remove(Pair.Key);
                            Nodes.Add(new Node(NewWeek, this, NewTasks));
                        }
                        Start = Start.Add(new TimeSpan(0, 30, 0));
                    }
                }
            }
            return Nodes;
        }

        public bool IsValid() {
            return TasksToSched.Count == 0;
        }

        public override string ToString() {
            return Week.ToString();
        }

    }
}
