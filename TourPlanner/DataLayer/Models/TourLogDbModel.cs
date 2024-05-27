using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer.Models
{
    public class TourLogDbModel
    {
        public TourLogDbModel(Guid id, DateTime date, TimeSpan duration, float distance, string comment, int difficulty, int rating)
        {
            Id = id;
            Date = date.ToUniversalTime();
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
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public float Distance { get; set; }
        public string Comment { get; set; }
        [Required]
        public int Difficulty { get; set; }
        [Required]
        public int Rating { get; set; }
        //needed for 1-to-many cascade delete
        public TourDbModel tourDbModel { get; set; }
    }
}
