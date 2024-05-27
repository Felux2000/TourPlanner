using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace TourPlanner.Models
{
    public class Tour
    {
        Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public float Distance { get; set; }
        public float Estimation { get; set; }
        public string MapJson { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }

        public Tour(string name, List<TourLog> logList, string mapJson)
        {
            Name = name;
            LogList = [.. logList];
            MapJson = mapJson;
        }
        public Tour(Guid id, string name, string description, string from, string to, string transportType, float distance, float estimation, string mapJson)
        {
            Id = id;
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            Estimation = estimation;
            MapJson = mapJson;
            LogList = new();
        }
        //NewTour
        public Tour(string name, string description, string from, string to, string transportType, float distance, float estimation, string mapJson)
        {
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            Estimation = estimation;
            MapJson = mapJson;
        }
        public Tour()
        {
        }
    }
}
