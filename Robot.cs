using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Robot : IElement
    {
        public delegate void MovingRobotActuator(Robot robot, int position);
        public event MovingRobotActuator RaiseMoveRobot;

        public delegate void AspiringRobotActuator(int position);
        public event AspiringRobotActuator AspireMoveRobot;

        public string ImagePath { get; set; }

        private Place place;
        private int positionWhereRobotIs = 0;
        //private Random random = new Random();

        public Robot()
        {
            ImagePath = "Assets\\robot.png";

            Engine.environment.RaiseChangeEnvironment += new Environment.ChangingEnvironmentActuator(robotSensor_OnEnvironmentChange);
        }

        private void robotSensor_OnEnvironmentChange(List<Place> places, int position)
        {
            place = places[positionWhereRobotIs];
        }

        internal void Execute()
        {
            Thread.Sleep(1000);

            //ObserveEnvironmentWithAllMySensors();
            //UpdateMyState();
            //ChooseAnAction();
            JustDoIt();
        }

        private void JustDoIt()
        {
            AspireMoveRobot(positionWhereRobotIs);
            //TODO: obviously this action will be placed in another location after.

            RaiseMoveRobot(this, positionWhereRobotIs);

            //TODO: this logic here will be inside action maybe
            positionWhereRobotIs++;

            if (positionWhereRobotIs >= Config.environmentSize)
                positionWhereRobotIs = 0;
        }
    }
}
