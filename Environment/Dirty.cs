using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Dirty : IElement
    {
        public string ImagePath { get; set; }

        public Dirty()
        {
            Random random = new Random();

            int index = random.Next(1, 4);

            ImagePath = String.Format("Assets\\dirty{0}.png", index);
        }
    }
}
