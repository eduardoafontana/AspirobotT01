using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Intention
    {
        public List<Perception> IntentionBranch { get; set; }

        public Intention()
        {
            IntentionBranch = new List<Perception>();
        }

        public Intention(Intention oldIntention)
        {
            IntentionBranch = oldIntention.IntentionBranch.Select(o => new Perception(o)).ToList();
        }
    }
}
