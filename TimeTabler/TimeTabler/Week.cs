using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTabler {

    class Week {

        public string[] WeekDays = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday",
            "Friday", "Saturday" };
        public static string CAL_CSV = "..\\..\\Schedule.csv";

        public Day[] Days { get; private set; }

        public Week() {
            Days = new Day[7];
            for(int i = 0; i < 7; i++) {
                Days[i] = new Day(WeekDays[i]);
            }
            PopulateCalander();
        }

        public Week(Week Week) {
            this.Days = new Day[7];
            for(int i = 0; i < 7; i++) {
                this.Days[i] = new Day(Week.Days[i]);
            }
        }

        public override string ToString() {
            string S = "";
            for(int i = 0; i < 7; i++) {
                S += Days[i].ToString();
            }
            return S;
        }

        private void PopulateCalander() {
            StreamReader Reader = new StreamReader(CAL_CSV);

            while(!Reader.EndOfStream) {
                string Line = Reader.ReadLine();
                string[] Splitted = Line.Split(',');

                int Index;
                int.TryParse(Splitted[0], out Index);

                DateTime StartTime = DateTime.ParseExact(Splitted[2], "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
                DateTime EndTime = DateTime.ParseExact(Splitted[3], "h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

                Days[Index].AddTask(new Task(Splitted[1], StartTime, EndTime));
            }
        }

        public int GetGapTime() {
            int Sum = 0;
            foreach(Day D in Days) {
                Sum += D.GetGapTime();
            }
            return Sum;
        }

    }
}
