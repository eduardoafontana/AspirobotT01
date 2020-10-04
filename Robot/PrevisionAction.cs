using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class PrevisionAction
    {
        public Actions Action { get; set; }
        public int ElectricityCost { get; set; }
        public int Position { get; set; }

        public PrevisionAction()
        {

        }

        public PrevisionAction(PrevisionAction previsionAction)
        {
            this.Action = previsionAction.Action;
            this.ElectricityCost = previsionAction.ElectricityCost;
            this.Position = previsionAction.Position;
        }
    }
}
