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

        private Place place;
        private int positionWhereRobotIs = 0;
        private int electricity = 0;
        private int countDirtyCollected = 0;
        private int countJewelCollected = 0;
        private int countPenitenceReceived = 0;
        private Actions chosedAction;

        private enum Actions
        {
            Move,
            Aspire,
            Collect
        }

        //private Random random = new Random();

        public Robot()
        {
            ImagePath = "Assets\\robot.png";
            robotDisplay = new RobotDisplay();

            Engine.environment.RaiseChangeEnvironment += new Environment.ChangingEnvironmentActuator(robotSensor_OnEnvironmentChange);
        }

        private void robotSensor_OnEnvironmentChange(List<Place> places)
        {
            place = places[positionWhereRobotIs];
        }

        internal void Execute()
        {
            Thread.Sleep(333);

            //ObserveEnvironmentWithAllMySensors();
            //UpdateMyState();
            ChooseAnAction();
            JustDoIt();
        }

        private void ChooseAnAction()
        {
            switch (chosedAction)
            {
                case Actions.Move:
                    chosedAction = Actions.Aspire;
                    break;
                case Actions.Aspire:
                    chosedAction = Actions.Collect;
                    break;
                case Actions.Collect:
                    chosedAction = Actions.Move;
                    break;
            }
        }

        private void JustDoIt()
        {
            switch (chosedAction)
            {
                case Actions.Move:
                    //TODO: this logic will be changed for exploration, after
                    positionWhereRobotIs++;

                    if (positionWhereRobotIs >= Config.environmentSize)
                        positionWhereRobotIs = 0;

                    RaiseMoveRobot(this, positionWhereRobotIs);
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

            electricity++;

            //TODO maybe after, if not used the variable here, change to display only.
            robotDisplay.UpdateDisplay(electricity, countDirtyCollected, countJewelCollected, countPenitenceReceived);
        }
    }
}
