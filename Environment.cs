using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Environment
    {
        public delegate void OpeningDoor(string count);
        public event OpeningDoor RaiseDoorOpening;

        private List<Place> places = new List<Place>();
        private Random random = new Random();
        private int currentRandomPosition;

        public Environment()
        {
            for (int i = 0; i < Config.environmentSize; i++)
            {
                places.Add(new Place());
            }
        }

        internal void Execute()
        {
            //Console.WriteLine("enviroment");
            Thread.Sleep(1000);

            if (shouldThereBeANewDirtySpace())
                GenerateDirt();

            if (shouldThereBeANewLostJewel())
                GenerateJewel();
        }

        private void GenerateJewel()
        {
            places[currentRandomPosition].element = new Jewel();

            RaiseDoorOpening(currentRandomPosition.ToString());
        }

        private bool shouldThereBeANewLostJewel()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].element == null)
                return true;

            return false;

            //TODO: Does a position have just one element as dirty or jewel or could have these two elements in the same time?

            //TODO: Add a logic for generate 1 jewel for x dirty.
        }

        private void GenerateDirt()
        {
            places[currentRandomPosition].element = new Dirty();

            RaiseDoorOpening(currentRandomPosition.ToString());
        }

        private bool shouldThereBeANewDirtySpace()
        {
            currentRandomPosition = random.Next(0, Config.environmentSize);

            if (places[currentRandomPosition].element == null)
                return true;

            return false;

            //TODO: Does a position have just one element as dirty or jewel or could have these two elements in the same time?
        }

        public class Place
        {
            public Object element { get; set; }
        }
    }
}
