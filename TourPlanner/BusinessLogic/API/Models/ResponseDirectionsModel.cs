using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.BusinessLogic.API.Models
{
    public class ResponseDirectionsModel
    {
        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        public class Feature
        {
            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }
        }

        public class Geometry
        {
            [JsonProperty("coordinates")]
            public List<List<double>> Coordinates { get; set; }
        }
    }
}
