using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Tour
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public float Distance { get; set; }
        public float Estimation { get; set; }
        public List<TourLog> LogList { get; set; }

        public Tour(string name, List<TourLog> logList)
        {
            Name = name;
            LogList = logList;
        }
        public Tour()
        {
        }
    }
}
