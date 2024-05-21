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
    internal class TourDbModel
    {
        public TourDbModel() 
        {
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
