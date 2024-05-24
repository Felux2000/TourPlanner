using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer.Models
{
    public class TourLogDbModel
    {
        public TourLogDbModel(DateTime date, TimeSpan duration, float distance, string comment, int difficulty, int rating)
        {
            Id = Guid.NewGuid();
            Date = date.ToFileTimeUtc();
            Duration = duration;
            Distance = distance;
            Comment = comment;
            Difficulty = difficulty;
            Rating = rating;
        }
        public TourLogDbModel()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public long Date { get; set; }
        public TimeSpan Duration { get; set; }
        public float Distance { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }
    }
}
