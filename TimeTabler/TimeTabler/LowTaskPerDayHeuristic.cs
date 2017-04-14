using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    class LowTaskPerDayHeuristic : Heuristic {

        /// <summary>
        /// Returns the number of tasks in the
        /// "easiest" day
        /// </summary>
        public int Evaluate(Node B) {
            Week Week = B.Week;
            int Min = int.MaxValue;
            foreach(Day Day in Week.Days) {
                if (Day.Tasks.Count < Min)
                    Min = Day.Tasks.Count;
            }
            return Min;
        }
    }
}
