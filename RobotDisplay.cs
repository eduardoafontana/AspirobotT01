﻿using System;
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

        internal void UpdateDisplay(int electricity, int dirty, int jewel, int penitence)
        {
            this.Electricity = electricity;
            this.Dirty = dirty;
            this.Jewel = jewel;
            this.Penitence = penitence;

            RaiseDisplayRobot(this);
        }
    }
}
