using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Prevision
    {
        public List<PrevisionAction> PrevisionBranch { get; set; }

        public Prevision()
        {
            PrevisionBranch = new List<PrevisionAction>();
        }

        public Prevision(Prevision oldPrevision)
        {
            PrevisionBranch = oldPrevision.PrevisionBranch.Select(o => new PrevisionAction(o)).ToList();
        }
    }
}
