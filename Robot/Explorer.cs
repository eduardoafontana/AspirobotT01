using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Explorer
    {
        private List<Intention> intentions = new List<Intention>();
        private InternalState internalState;

        public Explorer(InternalState internalState)
        {
            this.internalState = internalState;
        }

        internal List<IntentionAction> Execute_DeepSearchLimited_Algorithme(int positionWhereRobotIs)
        {
            //Clean old intentions
            intentions.Clear();

            Knowledge nodeWhereRobotIs = internalState.Beliefs.Where(n => n.Position == positionWhereRobotIs).First();

            Intention intention = new Intention();

            if (nodeWhereRobotIs.State == State.Dirty || nodeWhereRobotIs.State == State.DirtyAndJewel)
            {
                Perception perception = new Perception();
                perception.Action = Actions.Aspire;
                perception.DesireState = State.Dirty;
                perception.ElectricityCost = 1;
                perception.Position = nodeWhereRobotIs.Position;

                intention.IntentionBranch.Add(perception);
            }

            intentions.Add(intention);

            DeepSearchLimited(positionWhereRobotIs, new List<int>(), intention);

            //If there is no plan of intent that contains dirt, then return an empty plan and continue exploration in the next cycle, realizing the environment again.
            //TODO: change after jewel logic
            if (!intentions.Any(k => k.IntentionBranch.Any(p => p.DesireState == State.Dirty || p.DesireState == State.DirtyAndJewel)))
                new List<IntentionAction>();

            //The objective is achieved here. Choose the belief with more dirt that consumes less electricity.
            Intention mostDirtyLessElectricityCost = intentions
                .OrderByDescending(k => k.IntentionBranch.Where(p => p.DesireState == State.Dirty || p.DesireState == State.DirtyAndJewel).Count())
                .ThenBy(k => k.IntentionBranch.Sum(p => p.ElectricityCost))
                .First();

            List<IntentionAction> actionPlan = new List<IntentionAction>();

            foreach (Perception perception in mostDirtyLessElectricityCost.IntentionBranch)
            {
                IntentionAction intentionAction = new IntentionAction();
                intentionAction.Action = perception.Action;
                intentionAction.Position = perception.Position;

                actionPlan.Add(intentionAction);
            }

            return actionPlan;
        }

        private void DeepSearchLimited(int positionToSearch, List<int> positionAlreadySearched, Intention currentKnowledge)
        {
            List<Knowledge> nodesToDoPrevision = internalState.Beliefs.Where(n => n.Position == positionToSearch && !positionAlreadySearched.Contains(n.PositionLinkedKnowledge)).ToList();

            foreach (Knowledge knowledge in nodesToDoPrevision)
            {
                Intention intention = new Intention(currentKnowledge);

                Knowledge nodeLinked = internalState.Beliefs.Where(n => n.Position == knowledge.PositionLinkedKnowledge).First();

                Perception perceptionMove = new Perception();
                perceptionMove.Action = knowledge.Action;
                perceptionMove.DesireState = nodeLinked.State;
                perceptionMove.ElectricityCost = 1;
                perceptionMove.Position = knowledge.PositionLinkedKnowledge;

                intention.IntentionBranch.Add(perceptionMove);

                if (nodeLinked.State == State.Dirty || nodeLinked.State == State.DirtyAndJewel)
                {
                    Perception perceptionClean = new Perception();
                    perceptionClean.Action = Actions.Aspire;
                    perceptionClean.DesireState = State.Empty;
                    perceptionClean.ElectricityCost = 1;
                    perceptionClean.Position = knowledge.PositionLinkedKnowledge;

                    intention.IntentionBranch.Add(perceptionClean);
                }

                intentions.Add(intention);

                List<int> newPositionAlreadySearched = new List<int>(positionAlreadySearched);
                newPositionAlreadySearched.Add(positionToSearch);

                DeepSearchLimited(knowledge.PositionLinkedKnowledge, newPositionAlreadySearched, intention);
            }
        }
    }
}
