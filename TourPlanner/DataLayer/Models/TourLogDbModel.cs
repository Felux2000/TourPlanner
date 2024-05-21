using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer.Models
{
    internal class TourLogDbModel
    {
        public TourLogDbModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public float Distance { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }
    }
}
