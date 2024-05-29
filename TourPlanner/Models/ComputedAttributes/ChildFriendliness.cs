using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.ComputedAttributes
{
    public class ChildFriendliness
    {
        public int Value { get; private set; }

        public void ComputeAttribute(ObservableCollection<TourLog> logList)
        {
            if (logList.Count == 0)
            {
                Value = 0;
                return;
            }

            int totalDifficulty = logList.Sum(log => log.Difficulty);
            double averageDifficulty = (double)totalDifficulty / logList.Count;

            TimeSpan totalDuration = logList.Aggregate(TimeSpan.Zero, (sum, log) => sum.Add(log.Duration));
            double totalDistance = logList.Sum(log => log.Distance);

            //calculates child friendliness. The higher the value the better for children.
            Value = (int)((1 / (1 + averageDifficulty + totalDuration.TotalHours + totalDistance / 10)) * 100);
        }
    }
}
