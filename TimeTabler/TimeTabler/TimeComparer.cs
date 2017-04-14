using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class TimeComparer : IComparer<DateTime> {

        public int Compare(DateTime x, DateTime y) {
            return x.CompareTo(y);
        }
    }
}
