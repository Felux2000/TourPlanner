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
        public string Image { get; set; }
        public List<TourLog> LogList { get; set; }

        public Tour(string name, List<TourLog> logList, string image)
        {
            Name = name;
            LogList = logList;
            Image = image;
        }
        public Tour(string name, string description, string from, string to, string transportType, float distance, float estimation, string image)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            Estimation = estimation;
            Image = image;
            LogList = new();
        }
        public Tour()
        {
        }
    }
}
