using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataLayer.Models
{
    public class TourDbModel
    {
        public TourDbModel(string name, string description, string from, string to, string transportType, float distance, float estimation, string image) 
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            From = from;
            To = to;
            TransportType = transportType;
            Distance = distance;
            Estimation = estimation;
            Image = image;
            this.Logs = new Collection<TourLogDbModel>();
        }
        public TourDbModel() 
        {
            Id = Guid.NewGuid();
            this.Logs = new Collection<TourLogDbModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public float Distance { get; set; }
        public float Estimation { get; set; }
        public string Image { get; set; }
        public ICollection<TourLogDbModel> Logs { get; set; }
    }
}
