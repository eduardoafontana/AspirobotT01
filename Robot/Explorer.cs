﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Explorer
    {
        private List<Prevision> previsionCollection = new List<Prevision>();
        private InternalState internalState;

        internal List<Intention> Execute_DeepSearchLimited_Algorithm(InternalState internalState)
        {
            this.internalState = internalState;

            previsionCollection.Clear();

            Knowledge nodeWhereRobotIs = internalState.Beliefs.Where(n => n.Position == internalState.PositionWhereRobotIs).First();

            Prevision prevision = new Prevision();

            if (nodeWhereRobotIs.State == State.Jewel)
            {
                PrevisionAction previsionCollectJewel = new PrevisionAction();
                previsionCollectJewel.Action = Actions.Collect;
                previsionCollectJewel.ElectricityCost = 1;
                previsionCollectJewel.Position = nodeWhereRobotIs.Position;

                prevision.PrevisionBranch.Add(previsionCollectJewel);
            }
            else if (nodeWhereRobotIs.State == State.Dirty || nodeWhereRobotIs.State == State.DirtyAndJewel)
            {
                PrevisionAction previsionCleanDirty = new PrevisionAction();
                previsionCleanDirty.Action = Actions.Aspire;
                previsionCleanDirty.ElectricityCost = 1;
                previsionCleanDirty.Position = nodeWhereRobotIs.Position;

                prevision.PrevisionBranch.Add(previsionCleanDirty);
            }

            previsionCollection.Add(prevision);

            DeepSearchLimited(internalState.PositionWhereRobotIs, new List<int>(), prevision);

            //If there is no action plan that contains dirt or jewel, then return an empty plan and continue exploration in the next cycle, realizing the environment again.
            if (!previsionCollection.Any(k => k.PrevisionBranch.Any(p => p.Action == Actions.Aspire || p.Action == Actions.Collect)))
                new List<Intention>();

            //The objective is achieved here. Choose the plan with more dirt and jewel that consumes less electricity.
            Prevision mostDirtyLessElectricityCost = previsionCollection
                .OrderByDescending(k => k.PrevisionBranch.Where(p => p.Action == Actions.Aspire || p.Action == Actions.Collect).Count())
                .ThenBy(k => k.PrevisionBranch.Sum(p => p.ElectricityCost))
                .First();

            List<Intention> actionPlan = new List<Intention>();

            foreach (PrevisionAction previsionAction in mostDirtyLessElectricityCost.PrevisionBranch)
            {
                Intention intentionAction = new Intention();
                intentionAction.Action = previsionAction.Action;
                intentionAction.Position = previsionAction.Position;

                actionPlan.Add(intentionAction);
            }

            return actionPlan;
        }

        private void DeepSearchLimited(int positionToSearch, List<int> positionAlreadySearched, Prevision currentKnowledge)
        {
            List<Knowledge> nodesToDoPrevision = internalState.Beliefs.Where(n => n.Position == positionToSearch && !positionAlreadySearched.Contains(n.PositionLinkedKnowledge)).ToList();

            foreach (Knowledge knowledge in nodesToDoPrevision)
            {
                Prevision prevision = new Prevision(currentKnowledge);

                Knowledge nodeLinked = internalState.Beliefs.Where(n => n.Position == knowledge.PositionLinkedKnowledge).First();

                PrevisionAction previsionActionMove = new PrevisionAction();
                previsionActionMove.Action = (Actions)knowledge.Movement;
                previsionActionMove.ElectricityCost = 1;
                previsionActionMove.Position = knowledge.PositionLinkedKnowledge;

                prevision.PrevisionBranch.Add(previsionActionMove);

                if (nodeLinked.State == State.Jewel)
                {
                    PrevisionAction previsionCollectJewel = new PrevisionAction();
                    previsionCollectJewel.Action = Actions.Collect;
                    previsionCollectJewel.ElectricityCost = 1;
                    previsionCollectJewel.Position = knowledge.PositionLinkedKnowledge;

                    prevision.PrevisionBranch.Add(previsionCollectJewel);
                }
                else if (nodeLinked.State == State.Dirty || nodeLinked.State == State.DirtyAndJewel)
                {
                    PrevisionAction previsionActionClean = new PrevisionAction();
                    previsionActionClean.Action = Actions.Aspire;
                    previsionActionClean.ElectricityCost = 1;
                    previsionActionClean.Position = knowledge.PositionLinkedKnowledge;

                    prevision.PrevisionBranch.Add(previsionActionClean);
                }

                previsionCollection.Add(prevision);

                List<int> newPositionAlreadySearched = new List<int>(positionAlreadySearched);
                newPositionAlreadySearched.Add(positionToSearch);

                DeepSearchLimited(knowledge.PositionLinkedKnowledge, newPositionAlreadySearched, prevision);
            }
        }

        internal List<Intention> Execute_BestFirstSearch_Algorithm(InternalState internalState)
        {
            this.internalState = internalState;

            previsionCollection.Clear();

            Knowledge nodeWhereRobotIs = internalState.Beliefs.Where(n => n.Position == internalState.PositionWhereRobotIs).First();

            Prevision prevision = new Prevision();

            if (nodeWhereRobotIs.State == State.Jewel)
            {
                PrevisionAction previsionCollectJewel = new PrevisionAction();
                previsionCollectJewel.Action = Actions.Collect;
                previsionCollectJewel.ElectricityCost = 1;
                previsionCollectJewel.Position = nodeWhereRobotIs.Position;

                prevision.PrevisionBranch.Add(previsionCollectJewel);
            }
            else if (nodeWhereRobotIs.State == State.Dirty)
            {
                PrevisionAction previsionCleanDirty = new PrevisionAction();
                previsionCleanDirty.Action = Actions.Aspire;
                previsionCleanDirty.ElectricityCost = 1;
                previsionCleanDirty.Position = nodeWhereRobotIs.Position;

                prevision.PrevisionBranch.Add(previsionCleanDirty);
            }
            else if(nodeWhereRobotIs.State == State.DirtyAndJewel)
            {
                PrevisionAction previsionCollectJewel = new PrevisionAction();
                previsionCollectJewel.Action = Actions.Collect;
                previsionCollectJewel.ElectricityCost = 1;
                previsionCollectJewel.Position = nodeWhereRobotIs.Position;

                prevision.PrevisionBranch.Add(previsionCollectJewel);

                PrevisionAction previsionCleanDirty = new PrevisionAction();
                previsionCleanDirty.Action = Actions.Aspire;
                previsionCleanDirty.ElectricityCost = 1;
                previsionCleanDirty.Position = nodeWhereRobotIs.Position;

                prevision.PrevisionBranch.Add(previsionCleanDirty);
            }

            previsionCollection.Add(prevision);

            BestFirstSearch(internalState.PositionWhereRobotIs, new List<int>(), prevision);

            //If there is no action plan that contains dirt or jewel, then return an empty plan and continue exploration in the next cycle, realizing the environment again.
            if (!previsionCollection.Any(k => k.PrevisionBranch.Any(p => p.Action == Actions.Aspire || p.Action == Actions.Collect)))
                new List<Intention>();

            //The objective is achieved here. Choose the plan with more dirt and jewel that consumes less electricity.
            Prevision mostDirtyLessElectricityCost = previsionCollection
                .OrderByDescending(k => k.PrevisionBranch.Where(p => p.Action == Actions.Aspire || p.Action == Actions.Collect).Count())
                .ThenBy(k => k.PrevisionBranch.Sum(p => p.ElectricityCost))
                .First();

            List<Intention> actionPlan = new List<Intention>();

            foreach (PrevisionAction previsionAction in mostDirtyLessElectricityCost.PrevisionBranch)
            {
                Intention intentionAction = new Intention();
                intentionAction.Action = previsionAction.Action;
                intentionAction.Position = previsionAction.Position;

                actionPlan.Add(intentionAction);
            }

            return actionPlan;
        }

        private void BestFirstSearch(int positionToSearch, List<int> positionAlreadySearched, Prevision currentKnowledge)
        {
            List<Knowledge> nodesToDoPrevision = internalState.Beliefs.Where(n => n.Position == positionToSearch && !positionAlreadySearched.Contains(n.PositionLinkedKnowledge)).ToList();

            foreach (Knowledge knowledge in nodesToDoPrevision)
            {
                Prevision prevision = new Prevision(currentKnowledge);
                bool aspireOrCollect = false;

                Knowledge nodeLinked = internalState.Beliefs.Where(n => n.Position == knowledge.PositionLinkedKnowledge).First();

                PrevisionAction previsionActionMove = new PrevisionAction();
                previsionActionMove.Action = (Actions)knowledge.Movement;
                previsionActionMove.ElectricityCost = 1;
                previsionActionMove.Position = knowledge.PositionLinkedKnowledge;

                prevision.PrevisionBranch.Add(previsionActionMove);

                if (nodeLinked.State == State.Jewel)
                {
                    PrevisionAction previsionCollectJewel = new PrevisionAction();
                    previsionCollectJewel.Action = Actions.Collect;
                    previsionCollectJewel.ElectricityCost = 1;
                    previsionCollectJewel.Position = knowledge.PositionLinkedKnowledge;

                    prevision.PrevisionBranch.Add(previsionCollectJewel);
                    aspireOrCollect = true;
                }
                else if (nodeLinked.State == State.Dirty)
                {
                    PrevisionAction previsionActionClean = new PrevisionAction();
                    previsionActionClean.Action = Actions.Aspire;
                    previsionActionClean.ElectricityCost = 1;
                    previsionActionClean.Position = knowledge.PositionLinkedKnowledge;

                    prevision.PrevisionBranch.Add(previsionActionClean);
                    aspireOrCollect = true;
                }
                else if (nodeLinked.State == State.DirtyAndJewel)
                {
                    PrevisionAction previsionCollectJewel = new PrevisionAction();
                    previsionCollectJewel.Action = Actions.Collect;
                    previsionCollectJewel.ElectricityCost = 1;
                    previsionCollectJewel.Position = knowledge.PositionLinkedKnowledge;

                    prevision.PrevisionBranch.Add(previsionCollectJewel);

                    PrevisionAction previsionCleanDirty = new PrevisionAction();
                    previsionCleanDirty.Action = Actions.Aspire;
                    previsionCleanDirty.ElectricityCost = 1;
                    previsionCleanDirty.Position = knowledge.PositionLinkedKnowledge;

                    prevision.PrevisionBranch.Add(previsionCleanDirty);

                    aspireOrCollect = true;
                }

                previsionCollection.Add(prevision);

                List<int> newPositionAlreadySearched = new List<int>(positionAlreadySearched);
                newPositionAlreadySearched.Add(positionToSearch);

                if (aspireOrCollect)
                    return;

                BestFirstSearch(knowledge.PositionLinkedKnowledge, newPositionAlreadySearched, prevision);
            }
        }
    }
}
