﻿using System;
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

        //TODO: actuator spire dirty

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
            //TODO: Here will be the spire dirty action too when it is necessary.

            RaiseMoveRobot(this, positionWhereRobotIs);


            //TODO: this logic here will be inside action maybe
            positionWhereRobotIs++;

            if (positionWhereRobotIs >= Config.environmentSize)
                positionWhereRobotIs = 0;
        }
    }
}
