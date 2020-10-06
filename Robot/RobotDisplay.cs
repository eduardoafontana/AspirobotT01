using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class RobotDisplay
    {
        public delegate void DisplayingRobotActuator(RobotDisplay display);
        public event DisplayingRobotActuator RaiseDisplayRobot;

        public int Electricity { get; set; }
        public int Dirty { get; set; }
        public int Jewel { get; set; }
        public int Penitence { get; set; }

        public String Status { get; set; }
        public int ActionCount { get; set; }
        public int EpisodicAverage { get; set; }
        public String Performance { get; set; }

        internal void UpdateDisplay()
        {
            RaiseDisplayRobot(this);
        }

        internal void UpdatePerformanceData(Learning learning)
        {
            Status = learning.HasToWait() ? "Wating" : "Executing";
            ActionCount = learning.CountAction;
            EpisodicAverage = learning.ExplorationFrequency;

            Performance = "Act. Num. | Perform." + System.Environment.NewLine;

            foreach (var item in learning.Episode)
            {
                Performance = Performance + item.MaxNumberAction + "          |           " + item.PerformanceNumber + System.Environment.NewLine;
            }
        }
    }
}
