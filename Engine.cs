using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Engine
    {
        private static Thread environment = null;
        private static Thread robot = null;

        internal static void Start()
        {
            environment = new Thread(new ThreadStart(EnvironmentLoop));
            robot = new Thread(new ThreadStart(RobotLoop));

            environment.Start();
            robot.Start();
        }

        private static void EnvironmentLoop()
        {
            while(true)
            {
                Environment.Execute();
            }
        }

        private static void RobotLoop()
        {
            while (true)
            {
                Robot.Execute();
            }
        }

        internal static void Stop()
        {
            if (environment != null)
                environment.Abort();

            if (robot != null)
                robot.Abort();
        }
    }
}
