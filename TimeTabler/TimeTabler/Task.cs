using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {
    /// <summary>
    /// An event in the users calender
    /// </summary>
    public class Task {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Helper constructor
        /// </summary>
        /// <param name="Title">The task title</param>
        /// <param name="StartTime">The Start time</param>
        private Task(string Title, DateTime StartTime) {
            this.Title = Title;
            this.StartTime = StartTime;
        }

        public Task(string Title, DateTime StartTime, DateTime EndTime) : this(Title, StartTime) {
            if (EndTime.CompareTo(StartTime) < 0)
                throw new InvalidOperationException("End time Cannot be before Start time!");
            this.EndTime = EndTime;
            this.Duration = EndTime.Subtract(StartTime);
        }

        public Task(string Title, DateTime StartTime, TimeSpan Duration) : this(Title, StartTime) {
            this.Duration = Duration;
            this.EndTime = StartTime.Add(Duration);
        }

        public override string ToString() {
            return Title;
        }

    }
}
