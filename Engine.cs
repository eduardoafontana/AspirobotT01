using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Engine
    {
        private static Thread environmentThread = null;
        private static Thread robotThread = null;

        public static Environment environment;
        public static Robot robot;

        internal static void Init()
        {
            environment = new Environment();
            robot = new Robot();

            environment.sensor.AddRobotInEnvironment();

            environmentThread = new Thread(new ThreadStart(EnvironmentLoop));
            robotThread = new Thread(new ThreadStart(RobotLoop));
        }

        internal static void Start()
        {
            environmentThread.Start();
            robotThread.Start();
        }

        private static void EnvironmentLoop()
        {
            while(true)
            {
                environment.Execute();
            }
        }

        private static void RobotLoop()
        {
            while (true)
            {
                robot.Execute();
            }
        }

        internal static void Stop()
        {
            if (environmentThread != null)
                environmentThread.Abort();

            if (robotThread != null)
                robotThread.Abort();
        }
    }
}
