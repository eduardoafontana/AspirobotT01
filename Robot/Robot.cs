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
        private InternalState internalState = new InternalState();

        private int positionWhereRobotIs = 0;

        private List<Intention> intentions = new List<Intention>();

        private List<Knowledge> beliefs = new List<Knowledge>();

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
            if (intentions.Count() > 0)
                return;

            if (realTimeSensorPlace == null)
                return;

            for (int i = 0; i < realTimeSensorPlace.Count(); i++)
            {
                Place place = realTimeSensorPlace[i];

                State newState = State.Empty;

                if (place.jewel == null && place.dirty == null)
                    newState = State.Empty;
                else if (place.jewel != null && place.dirty == null)
                    newState = State.Jewel;
                else if (place.jewel == null && place.dirty != null)
                    newState = State.Dirty;
                else if (place.jewel != null && place.dirty != null)
                    newState = State.DirtyAndJewel;

                foreach (Node node in internalState.Map.Where(n => n.Position == i))
                    node.State = newState;
            }
        }

        private void UpdateMyState()
        {
            if (intentions.Count() > 0)
                return;

            beliefs.Clear();

            Explorer explorer = new Explorer(internalState);

            beliefs = explorer.Execute_DeepSearchLimited_Algorithme(positionWhereRobotIs);
        }

        private void ChooseAnAction()
        {
            if (intentions.Count() > 0)
                return;

            if (!beliefs.Any(k => k.KnowledgeBranch.Any(p => p.DesireState == State.Dirty || p.DesireState == State.DirtyAndJewel)))
                return;

            //The objective is achieved here. Choose the belief with more dirt that consumes less electricity.
            Knowledge mostDirtyLessElectricityCost = beliefs
                .OrderByDescending(k => k.KnowledgeBranch.Where(p => p.DesireState == State.Dirty || p.DesireState == State.DirtyAndJewel).Count())
                .ThenBy(k => k.KnowledgeBranch.Sum(p => p.ElectricityCost))
                .First();

            foreach (Perception perception in mostDirtyLessElectricityCost.KnowledgeBranch)
            {
                Intention intention = new Intention();
                intention.Action = perception.Action;
                intention.Position = perception.Position;

                intentions.Add(intention);
            }
        }

        private void JustDoIt()
        {
            if (intentions.Count() == 0)
                return;

            positionWhereRobotIs = intentions.First().Position;

            switch (intentions.First().Action)
            {
                case Actions.MoveUp:
                    RaiseMoveRobot(this, positionWhereRobotIs);
                    break;
                case Actions.MoveDown:
                    RaiseMoveRobot(this, positionWhereRobotIs);
                    break;
                case Actions.MoveLeft:
                    RaiseMoveRobot(this, positionWhereRobotIs);
                    break;
                case Actions.MoveRight:
                    RaiseMoveRobot(this, positionWhereRobotIs);
                    break;
                case Actions.Aspire:
                    robotDisplay.Dirty++;

                    RaiseAspireRobot(positionWhereRobotIs);
                    break;
                case Actions.Collect:
                    robotDisplay.Penitence++;
                    robotDisplay.Jewel++;

                    RaiseCollectRobot(positionWhereRobotIs);
                    break;
            }

            robotDisplay.Electricity++;

            robotDisplay.UpdateDisplay();

            intentions.RemoveAt(0);

            Thread.Sleep(333);
        }
    }
}
