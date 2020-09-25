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
        public event AspiringRobotActuator RaiseAspireRobot;

        public delegate void CollectingRobotActuator(int position);
        public event CollectingRobotActuator RaiseCollectRobot;

        public string ImagePath { get; set; }
        public RobotDisplay robotDisplay { get; set; }

        private Place realTimeSensorPlace;
        private Place internalCurrentPlaceState;

        private int positionWhereRobotIs = 0;
        private int electricity = 0;
        private int countDirtyCollected = 0;
        private int countJewelCollected = 0;
        private int countPenitenceReceived = 0;

        private enum Actions
        {
            Explore,
            Aspire,
            Collect,
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown
        }

        private List<Actions> intentions = new List<Actions>();

        private List<List<Actions>> beliefs = new List<List<Actions>>();

        public Robot()
        {
            ImagePath = "Assets\\robot.png";
            robotDisplay = new RobotDisplay();

            Engine.environment.RaiseChangeEnvironment += new Environment.ChangingEnvironmentActuator(robotSensor_OnEnvironmentChange);
        }

        private void robotSensor_OnEnvironmentChange(List<Place> places)
        {
            realTimeSensorPlace = places[positionWhereRobotIs];
        }

        internal void Execute()
        {
            Thread.Sleep(333);

            ObserveEnvironmentWithAllMySensors();
            UpdateMyState();
            ChooseAnAction();
            JustDoIt();
        }

        private void ObserveEnvironmentWithAllMySensors()
        {
            if (intentions.Count() > 0)
                return;

            internalCurrentPlaceState = realTimeSensorPlace;
        }

        private void UpdateMyState()
        {
            if (internalCurrentPlaceState == null)
                return;

            if (intentions.Count() > 0)
                return;

            beliefs.Clear();

            if (internalCurrentPlaceState.jewel == null && internalCurrentPlaceState.dirty == null)
                beliefs.Add(new List<Actions>() { Actions.Explore });
            else if (internalCurrentPlaceState.jewel != null && internalCurrentPlaceState.dirty == null)
                beliefs.Add(new List<Actions>() { Actions.Collect });
            else if (internalCurrentPlaceState.jewel == null && internalCurrentPlaceState.dirty != null)
                beliefs.Add(new List<Actions>() { Actions.Aspire });
            else if (internalCurrentPlaceState.jewel != null && internalCurrentPlaceState.dirty != null)
                beliefs.Add(new List<Actions>() { Actions.Collect, Actions.Aspire });
        }

        private void ChooseAnAction()
        {
            if (intentions.Count() > 0)
                return;

            foreach (List<Actions> belief in beliefs)
            {
                //CleanEntireEnvironment,
                if (belief.Last() == Actions.Explore)
                    intentions = Explore();

                //CleanCurrentPlace
                if (belief.Last() == Actions.Aspire)
                    intentions = belief;

                if (belief.Last() == Actions.Collect)
                    intentions = belief;
            }
        }

        private List<Actions> Explore()
        {
            return new List<Actions>() { Actions.MoveUp };
            //TODO will change
        }

        private void JustDoIt()
        {
            if (intentions.Count() == 0)
                return;

            switch (intentions.First())
            {
                case Actions.MoveUp:
                    //TODO: this logic will be changed for exploration, after
                    positionWhereRobotIs++;

                    if (positionWhereRobotIs >= Config.environmentSize)
                        positionWhereRobotIs = 0;

                    RaiseMoveRobot(this, positionWhereRobotIs);
                    break;
                case Actions.MoveDown:
                    break;
                case Actions.MoveLeft:
                    break;
                case Actions.MoveRight:
                    break;
                case Actions.Aspire:
                    countDirtyCollected++;
                    RaiseAspireRobot(positionWhereRobotIs);
                    break;
                case Actions.Collect:
                    countPenitenceReceived++; //TODO change it
                    countJewelCollected++;
                    RaiseCollectRobot(positionWhereRobotIs);
                    break;
            }

            intentions.RemoveAt(0);

            electricity++;

            //TODO maybe after, if not used the variable here, change to display only.
            robotDisplay.UpdateDisplay(electricity, countDirtyCollected, countJewelCollected, countPenitenceReceived);
        }
    }
}
