using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Perception
    {
        public Actions Action { get; set; }
        public State DesireState { get; set; }
        public int ElectricityCost { get; set; }
        public int Position { get; set; }

        public Perception()
        {

        }

        public Perception(Perception perception)
        {
            this.Action = perception.Action;
            this.DesireState = perception.DesireState;
            this.ElectricityCost = perception.ElectricityCost;
            this.Position = perception.Position;
        }
    }
}
