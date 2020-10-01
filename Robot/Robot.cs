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

        private List<Place> realTimeSensorPlace;
        private List<Place> observedEnvironmentState;

        private InternalState internalState = new InternalState();

        private List<IntentionAction> actionPlan = new List<IntentionAction>();

        public Robot()
        {
            ImagePath = "Assets\\robot.png";
            robotDisplay = new RobotDisplay();

            internalState.CreateInitialState();

            Engine.environment.RaiseChangeEnvironment += new Environment.ChangingEnvironmentActuator(robotSensor_OnEnvironmentChange);
        }

        private void robotSensor_OnEnvironmentChange(List<Place> places)
        {
            realTimeSensorPlace = places;
        }

        internal void Execute()
        {
            ObserveEnvironmentWithAllMySensors();
            UpdateMyState();
            ChooseAnAction();
            JustDoIt();
        }

        private void ObserveEnvironmentWithAllMySensors()
        {
            if (actionPlan.Count() > 0)
                return;

            if (realTimeSensorPlace == null)
                return;

            observedEnvironmentState = realTimeSensorPlace;
        }

        private void UpdateMyState()
        {
            if (actionPlan.Count() > 0)
                return;

            if (observedEnvironmentState == null)
                return;

            internalState.UpdateInteralState(observedEnvironmentState);
        }

        private void ChooseAnAction()
        {
            if (actionPlan.Count() > 0)
                return;

            Explorer explorer = new Explorer();

            actionPlan = explorer.Execute_DeepSearchLimited_Algorithme(internalState);
        }

        private void JustDoIt()
        {
            if (actionPlan.Count() == 0)
                return;

            internalState.PositionWhereRobotIs = actionPlan.First().Position;

            switch (actionPlan.First().Action)
            {
                case Actions.MoveUp:
                    RaiseMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.MoveDown:
                    RaiseMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.MoveLeft:
                    RaiseMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.MoveRight:
                    RaiseMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.Aspire:
                    robotDisplay.Dirty++;

                    RaiseAspireRobot(internalState.PositionWhereRobotIs);
                    break;
                case Actions.Collect:
                    robotDisplay.Penitence++;
                    robotDisplay.Jewel++;

                    RaiseCollectRobot(internalState.PositionWhereRobotIs);
                    break;
            }

            robotDisplay.Electricity++;

            robotDisplay.UpdateDisplay();

            actionPlan.RemoveAt(0);

            Thread.Sleep(333);
        }
    }
}
