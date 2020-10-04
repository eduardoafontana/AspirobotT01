using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Config
    {
        public static int environmentSize = 25;
        public static int environmentDimension = 5;
        public static int environmentPlaceSize = 100;
        public static int elementSize = 40;

        public static int stepsToReplan = 7;
        public static int countStepsToReplan = 4;
        public static int penitenceShed = 5;

        public static int robotActionDelay = 50;
        public static int environmentActionDelay = 300;
    }
}
