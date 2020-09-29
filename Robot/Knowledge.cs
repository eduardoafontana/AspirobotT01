using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Knowledge
    {
        public List<Perception> KnowledgeBranch { get; set; }

        public Knowledge()
        {
            KnowledgeBranch = new List<Perception>();
        }

        public Knowledge(Knowledge oldKnowledge)
        {
            KnowledgeBranch = oldKnowledge.KnowledgeBranch.Select(o => new Perception(o)).ToList();
        }
    }
}
