using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataLayer.Models
{
    public class TourDbModel
    {
        public TourDbModel(Guid id, string name, string description, string from, string to, string transportType, float distance, float estimation, string mapJson)
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
            Logs = new Collection<TourLogDbModel>();
        }

        public TourDbModel()
        {
            Id = Guid.NewGuid();
            Logs = new Collection<TourLogDbModel>();
        }

        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public string TransportType { get; set; }
        [Required]
        public float Distance { get; set; }
        [Required]
        public float Estimation { get; set; }
        [Required]
        public string MapJson { get; set; }
        public ICollection<TourLogDbModel>? Logs { get; set; }
    }
}
