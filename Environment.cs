using System;
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
            places[currentRandomPosition].element = new Jewel();

            RaiseChangeEnvironment(places, currentRandomPosition);
        }

        private bool ShouldThereBeANewLostJewel()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].element == null)
                return true;

            return false;

            //TODO: Does a position have just one element as dirty or jewel or could have these two elements in the same time?

            //TODO: Add a logic for generate 1 jewel for x dirty.
        }

        private void GenerateDirt()
        {
            places[currentRandomPosition].element = new Dirty();

            RaiseChangeEnvironment(places, currentRandomPosition);
        }

        private bool ShouldThereBeANewDirtySpace()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].element == null)
                return true;

            return false;

            //TODO: Does a position have just one element as dirty or jewel or could have these two elements in the same time?
        }

        private void environmentSensor_OnRobotMove(Robot robot, int position)
        {
            int currentIndex = places.FindIndex(p => p.element != null && p.element.GetType() == robot.GetType());

            if (currentIndex >= 0)
                places[currentIndex].element = null;

            places[position].element = robot;

            RaiseChangeEnvironment(places, currentRandomPosition);
        }
    }
}
