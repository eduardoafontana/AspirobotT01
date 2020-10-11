using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Robot : IElement
    {
        public string ImagePath { get; set; }
        public RobotDisplay robotDisplay { get; set; }
        public RobotActuator actuator = new RobotActuator();

        private RobotSensor sensor;

        private List<Place> observedEnvironmentState;

        private InternalState internalState = new InternalState();

        private List<Intention> actionPlan = new List<Intention>();

        private Learning learning = new Learning();

        public Robot()
        {
            ImagePath = "Assets\\robot.png";
            robotDisplay = new RobotDisplay();

            internalState.CreateInitialState();

            sensor = new RobotSensor();
        }

        internal void Execute()
        {
            robotDisplay.UpdatePerformanceData(learning);
            robotDisplay.UpdateDisplay();

            ObserveEnvironmentWithAllMySensors();
            UpdateMyState();
            ChooseAnAction();
            JustDoIt();
        }

        private void ObserveEnvironmentWithAllMySensors()
        {
            if (actionPlan.Count() > 0)
                return;

            learning.MeasureWaitFrequency();

            if (learning.HasToWait())
                return;

            if (sensor.RealTimeSensorPlace == null)
                return;

            observedEnvironmentState = sensor.RealTimeSensorPlace;
        }

        private void UpdateMyState()
        {
            if (actionPlan.Count() > 0)
                return;

            if (learning.HasToWait())
                return;

            if (observedEnvironmentState == null)
                return;

            internalState.UpdateInteralState(observedEnvironmentState);
        }

        private void ChooseAnAction()
        {
            if (actionPlan.Count() > 0)
                return;

            if (learning.HasToWait())
                return;

            Explorer explorer = new Explorer();

            if (this.robotDisplay.Penitence < Config.penitenceShed)
                actionPlan = explorer.Execute_DeepSearchLimited_Algorithm(internalState);
            else
                actionPlan = explorer.Execute_BestFirstSearch_Algorithm(internalState);
        }

        private void JustDoIt()
        {
            if (actionPlan.Count() == 0)
                return;

            internalState.PositionWhereRobotIs = actionPlan.First().Position;

            switch (actionPlan.First().Action)
            {
                case Actions.MoveUp:
                    actuator.TriggerMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.MoveDown:
                    actuator.TriggerMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.MoveLeft:
                    actuator.TriggerMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.MoveRight:
                    actuator.TriggerMoveRobot(this, internalState.PositionWhereRobotIs);
                    break;
                case Actions.Aspire:
                    robotDisplay.Dirty++;
                    learning.CountDirt++;

                    if (observedEnvironmentState[internalState.PositionWhereRobotIs].jewel != null)
                    {
                        robotDisplay.Penitence++;
                        learning.CountPenitence++;
                    }

                    actuator.TriggerAspireRobot(internalState.PositionWhereRobotIs);
                    break;
                case Actions.Collect:
                    robotDisplay.Jewel++;
                    learning.CountJewel++;

                    actuator.TriggerCollectRobot(internalState.PositionWhereRobotIs);
                    break;
            }

            robotDisplay.Electricity++;

            robotDisplay.UpdateDisplay();

            learning.CountElectricity++;
            learning.MeasurePerformance();

            actionPlan.RemoveAt(0);

            Thread.Sleep(Config.robotActionDelay);
        }
    }
}
