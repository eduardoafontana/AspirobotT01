using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspirobotT01
{
    public class Performance
    {
        public Performance(int maxNumberAction)
        {
            this.MaxNumberAction = maxNumberAction;
        }

        public int MaxNumberAction { get; set; }
        public int PerformanceNumber { get; set; }
    }

    public class Learning
    {
        public List<Performance> Episode = new List<Performance>();

        public int CountDirt { get; set; }
        public int CountJewel { get; set; }
        public int CountPenitence { get; set; }
        public int CountElectricity { get; set; }

        public int CountAction { get; set; }

        public int ExplorationFrequency { get; set; }
        public int ExplorationFrequencyWaitCount { get; set; }

        public Learning()
        {
            for (int i = 1; i <= Config.learningEpisodeSize; i++)
            {
                Episode.Add(new Performance(i * Config.learningEpisodeSize));
            }
        }

        public void MeasurePerformance()
        {
            if (HasToWait())
                return;

            CountAction++;

            Performance performance = Episode.OrderBy(e => e.MaxNumberAction).Where(e => e.MaxNumberAction == CountAction).FirstOrDefault();

            if (performance == null)
                return;

            performance.PerformanceNumber = CountElectricity + CountPenitence - CountJewel - CountDirt;

            CountDirt = 0;
            CountJewel = 0;
            CountPenitence = 0;
            CountElectricity = 0;

            if (performance.MaxNumberAction == Episode.Last().MaxNumberAction)
            {
                ExplorationFrequencyWaitCount = 0;

                CountAction = 0;

                ExplorationFrequency = Episode.Sum(e => e.PerformanceNumber) / Config.learningEpisodeSize; ;

                Thread.Sleep(Config.robotActionDelay);

                Episode.ForEach(e => e.PerformanceNumber = 0);
            }
        }

        public bool HasToWait()
        {
            return ExplorationFrequencyWaitCount < ExplorationFrequency;
        }

        public void MeasureWaitFrequency()
        {
            if (ExplorationFrequencyWaitCount >= ExplorationFrequency)
                return;

            ExplorationFrequencyWaitCount++;

            Thread.Sleep(Config.robotActionDelay);
        }
    }
}
