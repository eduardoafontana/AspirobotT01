using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class RobotActuator
    {
        public delegate void MovingRobotActuator(Robot robot, int position);
        public event MovingRobotActuator RaiseMoveRobot;

        public delegate void AspiringRobotActuator(int position);
        public event AspiringRobotActuator RaiseAspireRobot;

        public delegate void CollectingRobotActuator(int position);
        public event CollectingRobotActuator RaiseCollectRobot;

        public RobotActuator()
        {
        }

        public void TriggerMoveRobot(Robot robot, int position)
        {
            RaiseMoveRobot(robot, position);
        }

        public void TriggerAspireRobot(int position)
        {
            RaiseAspireRobot(position);
        }

        public void TriggerCollectRobot(int position)
        {
            RaiseCollectRobot(position);
        }
    }
}
