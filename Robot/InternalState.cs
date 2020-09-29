using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Node
    {
        public int Position { get; set; }
        public int PositionLinkedNode { get; set; }
        public State State { get; set; }
        public Actions Action { get; set; }

        public int IndexI { get; set; }
        public int IndexJ { get; set; }

        public Node(int position, int positionLinkedNode, State state, Actions action, int indexI, int indexJ)
        {
            this.Position = position;
            this.PositionLinkedNode = positionLinkedNode;
            this.State = state;
            this.Action = action;

            this.IndexI = indexI;
            this.IndexJ = indexJ;
        }
    }

    public class InternalState
    {
        public List<Node> Map = new List<Node>();

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
                    int linkedNodePosition = (Config.environmentDimension * j) + rightNode;

                    Map.Add(new Node(c, linkedNodePosition, State.Empty, Actions.MoveRight, i, j));
                }

                if (bottomNode < Config.environmentDimension)
                {
                    int linkedNodePosition = (Config.environmentDimension * bottomNode) + i;

                    Map.Add(new Node(c, linkedNodePosition, State.Empty, Actions.MoveDown, i, j));
                }

                if (leftNode >= 0)
                {
                    int linkedNodePosition = (Config.environmentDimension * j) + leftNode;

                    Map.Add(new Node(c, linkedNodePosition, State.Empty, Actions.MoveLeft, i, j));
                }

                if (topNode >= 0)
                {
                    int linkedNodePosition = (Config.environmentDimension * topNode) + i;

                    Map.Add(new Node(c, linkedNodePosition, State.Empty, Actions.MoveUp, i, j));
                }

                i++;

                if (i >= Config.environmentDimension)
                {
                    i = 0;
                    j++;
                }
            }
        }
    }
}
