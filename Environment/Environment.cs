using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Environment
    {
        private List<Place> places = new List<Place>();
        private Random random = new Random();
        private int currentRandomPosition;

        public EnvironmentSensor sensor;
        public EnvironmentActuator actuator = new EnvironmentActuator();

        public Environment()
        {
            for (int i = 0; i < Config.environmentSize; i++)
            {
                places.Add(new Place());
            }

            sensor = new EnvironmentSensor(this);
        }

        internal void Execute()
        {
            Thread.Sleep(Config.environmentActionDelay);

            if (ShouldThereBeANewDirtySpace())
                GenerateDirt();

            if (ShouldThereBeANewLostJewel())
                GenerateJewel();
        }

        private void GenerateJewel()
        {
            places[currentRandomPosition].jewel = new Jewel();

            actuator.TriggerChangeEnvironment(places);
        }

        private bool ShouldThereBeANewLostJewel()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].jewel == null)
                return true;

            return false;
        }

        private void GenerateDirt()
        {
            places[currentRandomPosition].dirty = new Dirty();

            actuator.TriggerChangeEnvironment(places);
        }

        private bool ShouldThereBeANewDirtySpace()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].dirty == null)
                return true;

            return false;
        }

        internal void ApplyRobotMove(Robot robot, int position)
        {
            int currentIndex = places.FindIndex(p => p.robot != null && p.robot.GetType() == robot.GetType());

            if (currentIndex >= 0)
                places[currentIndex].robot = null;

            places[position].robot = robot;

            actuator.TriggerChangeEnvironment(places);
        }

        internal void ApplyRobotAspire(int position)
        {
            places[position].dirty = null;
            places[position].jewel = null;

            actuator.TriggerChangeEnvironment(places);
        }

        internal void ApplyRobotCollect(int position)
        {
            places[position].jewel = null;

            actuator.TriggerChangeEnvironment(places);
        }
    }
}
