using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Environment
    {
        public delegate void OpeningDoor(string count);
        public event OpeningDoor RaiseDoorOpening;

        private int counter = 0;

        internal void Execute()
        {
            counter++;

            RaiseDoorOpening(counter.ToString());

            //Console.WriteLine("enviroment");
            Thread.Sleep(1000);

            //if (shouldThereBeANewDirtySpace())
            //    GenerateDirt();

            //if (shouldThereBeANewLostJewel())
            //    GenerateJewel();


        }
    }
}
