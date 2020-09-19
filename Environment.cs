﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Environment
    {
        public delegate void ChangingEnvironmentActuator(List<Place> places, int position);
        public event ChangingEnvironmentActuator RaiseChangeEnvironment;

        private List<Place> places = new List<Place>();
        private Random random = new Random();
        private int currentRandomPosition;

        public Environment()
        {
            for (int i = 0; i < Config.environmentSize; i++)
            {
                places.Add(new Place());
            }
        }

        internal void AddRobotInEnvironment()
        {
            Engine.robot.RaiseMoveRobot += new Robot.MovingRobotActuator(environmentSensor_OnRobotMove);
            Engine.robot.AspireMoveRobot += new Robot.AspiringRobotActuator(environmentSensor_OnRobotAspire);
        }

        internal void Execute()
        {
            Thread.Sleep(3000);

            if (ShouldThereBeANewDirtySpace())
                GenerateDirt();

            if (ShouldThereBeANewLostJewel())
                GenerateJewel();
        }

        private void GenerateJewel()
        {
            places[currentRandomPosition].jewel = new Jewel();

            RaiseChangeEnvironment(places, currentRandomPosition);
        }

        private bool ShouldThereBeANewLostJewel()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].jewel == null)
                return true;

            return false;

            //TODO: Add a logic for generate 1 jewel for x dirty.
        }

        private void GenerateDirt()
        {
            places[currentRandomPosition].dirty = new Dirty();

            RaiseChangeEnvironment(places, currentRandomPosition);
        }

        private bool ShouldThereBeANewDirtySpace()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].dirty == null)
                return true;

            return false;
        }

        private void environmentSensor_OnRobotMove(Robot robot, int position)
        {
            int currentIndex = places.FindIndex(p => p.robot != null && p.robot.GetType() == robot.GetType());

            if (currentIndex >= 0)
                places[currentIndex].robot = null;

            places[position].robot = robot;

            RaiseChangeEnvironment(places, currentRandomPosition);
        }

        private void environmentSensor_OnRobotAspire(int position)
        {
            places[position].dirty = null;

            RaiseChangeEnvironment(places, currentRandomPosition);
        }
    }
}
