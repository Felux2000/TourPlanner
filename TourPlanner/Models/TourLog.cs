using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourLog
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public float Distance { get; set; }
        public string Comment { get; set; }
        public int Difficulty { get; set; }
        public int Rating { get; set; }

        //UpdateLog
        public TourLog(Guid id, DateTime date, TimeSpan duration, float distance, string comment, int difficulty, int rating)
        {
            Id = id;
            Date = date;
            Duration = duration;
            Distance = distance;
            Comment = comment;
            Difficulty = difficulty;
            Rating = rating;
        }
        //NewLog
        public TourLog(DateTime date, TimeSpan duration, float distance, string comment, int difficulty, int rating)
        {
            Id = Guid.NewGuid();
            Date = date;
            Duration = duration;
            Distance = distance;
            Comment = comment;
            Difficulty = difficulty;
            Rating = rating;
        }
        public TourLog() { }
    }
}
