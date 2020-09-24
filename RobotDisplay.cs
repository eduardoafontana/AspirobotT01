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

        internal void UpdateDisplay(int electricity)
        {
            this.Electricity = electricity;

            RaiseDisplayRobot(this);
        }
    }
}
