using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Knowledge
    {
        public int Position { get; set; }
        public int PositionLinkedKnowledge { get; set; }
        public State State { get; set; }
        public Actions Action { get; set; }

        public int IndexI { get; set; }
        public int IndexJ { get; set; }

        public Knowledge(int position, int positionLinkedKnowledge, State state, Actions action, int indexI, int indexJ)
        {
            this.Position = position;
            this.PositionLinkedKnowledge = positionLinkedKnowledge;
            this.State = state;
            this.Action = action;

            this.IndexI = indexI;
            this.IndexJ = indexJ;
        }
    }

    public class InternalState
    {
        public List<Knowledge> Beliefs = new List<Knowledge>();

        public void CreateInitialState()
        {
            int i = 0;
            int j = 0;

            for (int c = 0; c < Config.environmentSize; c++)
            {
                int rightNode = i + 1;
                int bottomNode = j + 1;
                int leftNode = i - 1;
                int topNode = j - 1;

                if (rightNode < Config.environmentDimension)
                {
                    int linkedKnowledgePosition = (Config.environmentDimension * j) + rightNode;

                    Beliefs.Add(new Knowledge(c, linkedKnowledgePosition, State.Empty, Actions.MoveRight, i, j));
                }

                if (bottomNode < Config.environmentDimension)
                {
                    int linkedKnowledgePosition = (Config.environmentDimension * bottomNode) + i;

                    Beliefs.Add(new Knowledge(c, linkedKnowledgePosition, State.Empty, Actions.MoveDown, i, j));
                }

                if (leftNode >= 0)
                {
                    int linkedKnowledgePosition = (Config.environmentDimension * j) + leftNode;

                    Beliefs.Add(new Knowledge(c, linkedKnowledgePosition, State.Empty, Actions.MoveLeft, i, j));
                }

                if (topNode >= 0)
                {
                    int linkedKnowledgePosition = (Config.environmentDimension * topNode) + i;

                    Beliefs.Add(new Knowledge(c, linkedKnowledgePosition, State.Empty, Actions.MoveUp, i, j));
                }

                i++;

                if (i >= Config.environmentDimension)
                {
                    i = 0;
                    j++;
                }
            }
        }

        internal void UpdateInteralState(List<Place> observedEnvironmentState)
        {
            for (int i = 0; i < observedEnvironmentState.Count(); i++)
            {
                Place place = observedEnvironmentState[i];

                State newState = State.Empty;

                if (place.jewel == null && place.dirty == null)
                    newState = State.Empty;
                else if (place.jewel != null && place.dirty == null)
                    newState = State.Jewel;
                else if (place.jewel == null && place.dirty != null)
                    newState = State.Dirty;
                else if (place.jewel != null && place.dirty != null)
                    newState = State.DirtyAndJewel;

                foreach (Knowledge node in Beliefs.Where(n => n.Position == i))
                    node.State = newState;
            }
        }
    }
}
