using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class LowTimePerDayHeuristic : Heuristic {

        /// <summary>
        /// Returns the total time spent working
        /// (in minutes) during the "easiest" day
        /// </summary>
        public int Evaluate(Node B) {
            Week Week = B.Week;
            DateTime Now = DateTime.Now;
            DateTime Elapsed = new DateTime(Now.Year, Now.Month, Now.Day, 0, 0, 0);
            foreach(Day Day in Week.Days) {
                List<string> Visited = new List<string>();
                foreach(KeyValuePair<DateTime, Task> Pair in Day.Tasks) {
                    if (Visited.Contains(Pair.Value.Title))
                        continue;
                    else {
                        Visited.Add(Pair.Value.Title);
                        Elapsed = Elapsed.Add(Pair.Value.EndTime.Subtract(Pair.Value.StartTime));
                    }
                }
            }
            //Convert final value to minutes
            return (Elapsed.Hour * 60) + Elapsed.Minute;
        }
    }
}
