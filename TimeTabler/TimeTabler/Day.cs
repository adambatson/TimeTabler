using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class Day {

        public string Title { get; set; }
        public SortedDictionary<DateTime, Task> Tasks { get; private set; }

        public Day(string Title) {
            this.Title = Title;
            Tasks = new SortedDictionary<DateTime, Task>(new TimeComparer());
        }

        public Day(Day Day) {
            this.Title = Day.Title;
            this.Tasks = new SortedDictionary<DateTime, Task>(Day.Tasks);
        }

        public bool AddTask(Task Task) {
            try {
                SortedDictionary<DateTime, Task> NewTasks = new SortedDictionary<DateTime, Task>(Tasks);
                NewTasks.Add(Task.StartTime, Task);
                NewTasks.Add(Task.EndTime, Task);
                DateTime Time = Task.StartTime.Add(new TimeSpan(0, 30, 0)); //Half hour increments
                while(Time < Task.EndTime) {
                    NewTasks.Add(Time, Task);
                    Time = Time.Add(new TimeSpan(0, 30, 0));
                }
                Tasks = NewTasks;
                return true;
            } catch(ArgumentException) {
                return false;
            }
        }

        public override string ToString() {
            string s = Title + "\n";
            foreach(KeyValuePair<DateTime, Task> pair in Tasks) {
                s += (pair.Key.ToString("t") + " " + pair.Value);
                s += "\n";
            }
            return s;
        }

    }
}
