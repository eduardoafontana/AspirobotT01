using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class EnvironmentSensor
    {
        private Environment environment;

        public EnvironmentSensor(Environment environment)
        {
            this.environment = environment;
        }

        internal void AddRobotInEnvironment()
        {
            Engine.robot.actuator.RaiseMoveRobot += new RobotActuator.MovingRobotActuator(environmentSensor_OnRobotMove);
            Engine.robot.actuator.RaiseAspireRobot += new RobotActuator.AspiringRobotActuator(environmentSensor_OnRobotAspire);
            Engine.robot.actuator.RaiseCollectRobot += new RobotActuator.CollectingRobotActuator(environmentSensor_OnRobotCollect);
        }

        private void environmentSensor_OnRobotMove(Robot robot, int position)
        {
            environment.ApplyRobotMove(robot, position);
        }

        private void environmentSensor_OnRobotAspire(int position)
        {
            environment.ApplyRobotAspire(position);
        }

        private void environmentSensor_OnRobotCollect(int position)
        {
            environment.ApplyRobotCollect(position);
        }
    }
}
