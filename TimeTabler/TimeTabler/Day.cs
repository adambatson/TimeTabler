﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class Day {

        public string Title { get; set; }
        public SortedDictionary<DateTime, Task> Tasks { get; private set; }

        private DateTime DayEnd;

        public Day(string Title) {
            this.Title = Title;
            Tasks = new SortedDictionary<DateTime, Task>(new TimeComparer());
            DateTime Now = DateTime.Now;
            DayEnd = new DateTime(Now.Year, Now.Month, Now.Day, 22, 0, 0);
        }

        public Day(Day Day) {
            this.Title = Day.Title;
            this.Tasks = new SortedDictionary<DateTime, Task>(Day.Tasks);
            this.DayEnd = Day.DayEnd;
        }

        public bool AddTask(Task Task) {
            if (Task.EndTime > DayEnd)
                return false;
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
            List<string> Printed = new List<string>();
            foreach(KeyValuePair<DateTime, Task> Pair in Tasks) {
                if (Printed.Contains(Pair.Value.Title))
                    continue;
                else {
                    s += (Pair.Value + " " + Pair.Value.StartTime.ToString("t") + " - " +
                        Pair.Value.EndTime.ToString("t"));
                    s += "\n";
                    Printed.Add(Pair.Value.Title);
                }
            }
            return s;
        }

        /// <summary>
        /// Gets "free" time between tasks
        /// </summary>
        /// <returns></returns>
        public int GetGapTime() {
            Task[] T = Tasks.Values.Distinct().ToArray();
            DateTime Now = DateTime.Now;
            DateTime Elapsed = new DateTime(Now.Year, Now.Month, Now.Day, 0, 0, 0);
            int TimeElapsed = 0;
            for(int i = 0; i < T.Count() - 1; i++) {
                TimeSpan Diff = T[i + 1].StartTime.Subtract(T[i].EndTime);
                TimeElapsed += (Diff.Hours * 60) + Diff.Minutes;
            }
            return TimeElapsed;
        }

    }
}
