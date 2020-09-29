using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Explorer
    {
        private List<Knowledge> beliefs = new List<Knowledge>();
        private InternalState internalState;

        public Explorer(InternalState internalState)
        {
            this.internalState = internalState;
        }

        internal List<Knowledge> Execute_DeepSearchLimited_Algorithme(int positionWhereRobotIs)
        {
            Node nodeWhereRobotIs = internalState.Map.Where(n => n.Position == positionWhereRobotIs).First();

            Knowledge knowledge = new Knowledge();

            if (nodeWhereRobotIs.State == State.Dirty || nodeWhereRobotIs.State == State.DirtyAndJewel)
            {
                Perception perception = new Perception();
                perception.Action = Actions.Aspire;
                perception.DesireState = State.Dirty;
                perception.ElectricityCost = 1;
                perception.Position = nodeWhereRobotIs.Position;

                knowledge.KnowledgeBranch.Add(perception);
            }

            beliefs.Add(knowledge);

            DeepSearchLimited(positionWhereRobotIs, new List<int>(), knowledge);

            return beliefs;
        }

        private void DeepSearchLimited(int positionToSearch, List<int> positionAlreadySearched, Knowledge currentKnowledge)
        {
            List<Node> nodesToDoPrevision = internalState.Map.Where(n => n.Position == positionToSearch && !positionAlreadySearched.Contains(n.PositionLinkedNode)).ToList();

            foreach (Node node in nodesToDoPrevision)
            {
                Knowledge knowledge = new Knowledge(currentKnowledge);

                Node nodeLinked = internalState.Map.Where(n => n.Position == node.PositionLinkedNode).First();

                Perception perceptionMove = new Perception();
                perceptionMove.Action = node.Action;
                perceptionMove.DesireState = nodeLinked.State;
                perceptionMove.ElectricityCost = 1;
                perceptionMove.Position = node.PositionLinkedNode;

                knowledge.KnowledgeBranch.Add(perceptionMove);

                if (nodeLinked.State == State.Dirty || nodeLinked.State == State.DirtyAndJewel)
                {
                    Perception perceptionClean = new Perception();
                    perceptionClean.Action = Actions.Aspire;
                    perceptionClean.DesireState = State.Empty;
                    perceptionClean.ElectricityCost = 1;
                    perceptionClean.Position = node.PositionLinkedNode;

                    knowledge.KnowledgeBranch.Add(perceptionClean);
                }

                beliefs.Add(knowledge);

                List<int> newPositionAlreadySearched = new List<int>(positionAlreadySearched);
                newPositionAlreadySearched.Add(positionToSearch);

                DeepSearchLimited(node.PositionLinkedNode, newPositionAlreadySearched, knowledge);
            }
        }
    }
}
