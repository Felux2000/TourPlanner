using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace TourPlanner.Models
{
    public class Tour
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public float Distance { get; set; }
        public float Estimation { get; set; }
        public string MapJson { get; set; }
        public ObservableCollection<TourLog> LogList { get; set; }
        public double Popularity { get; private set; }
        public int ChildFriendliness { get; private set; }

        public Tour(string name, List<TourLog> logList, string mapJson)
        {
            Name = name;
            LogList = [.. logList];
            MapJson = mapJson;
            InitializeAttributes();
        }
        //UpdateTour
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
            InitializeAttributes();
        }
        //NewTour
        public Tour(string name, string description, string from, string to, string transportType, float distance, float estimation, string mapJson)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            Estimation = estimation;
            MapJson = mapJson;
            LogList = new();
            InitializeAttributes();
        }
        public Tour()
        {
            LogList = new();
            InitializeAttributes();
        }

        private void InitializeAttributes()
        {
            LogList.CollectionChanged += (s, e) => ComputeAttributes();
            ComputeAttributes();
        }

        private void ComputeAttributes()
        {
            ComputePopularity();
            ComputeChildFriendliness();
        }

        private void ComputeChildFriendliness()
        {
            if (LogList.Count == 0)
            {
                ChildFriendliness = 0;
                return;
            }

            int totalDifficulty = LogList.Sum(log => log.Difficulty);
            double averageDifficulty = (double)totalDifficulty / LogList.Count;

            ChildFriendliness = (int)((1 / averageDifficulty) * 100);
        }

        private void ComputePopularity()
        {
            int logCount = LogList.Count;
            if (logCount == 0)
            {
                Popularity = 0;
                return;
            }

            double averageRating = LogList.Average(log => log.Rating);
            double invertedRating = 1 - averageRating / 10;

            double x = 0.001; //number needed to avoid division by 0
            Popularity = logCount / (invertedRating + x);
        }
    }
}
