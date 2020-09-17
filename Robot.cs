using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Robot
    {
        public delegate void OpeningDoor(string count);
        public event OpeningDoor RaiseDoorOpening;

        private int counter = 0;

        internal void Execute()
        {
            counter++;

            RaiseDoorOpening(counter.ToString());

            Thread.Sleep(200);
        }
    }
}
