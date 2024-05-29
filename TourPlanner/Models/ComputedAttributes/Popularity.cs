using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.ComputedAttributes
{
    public class Popularity
    {
        //calculation is based on number of logs and average rating. More logs but worse rating can lead to a lower popularity than less logs but better average rating
        public double Value { get; private set; }

        public void ComputeAttribute(ObservableCollection<TourLog> logList)
        {
            int logCount = logList.Count;
            if(logCount == 0) return;

            double averageRating = logList.Average(log => log.Rating);
            double invertedRating = 1 - averageRating / 10;

            double x = 0.001; //number needed to avoid division by 0
            Value = logCount / (invertedRating + x);
        }
    }
}
