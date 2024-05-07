using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TourPlanner.BusinessLogic.API.Models
{
    public class ResponseGeoCodeSearchModel
    {
        [JsonProperty("features")]
        public List<GeocodeFeature> Features { get; set; }

        public class GeocodeFeature
        {
            [JsonProperty("geometry")]
            public Geometry Geometry { get; set; }

        }
        public class Geometry
        {
            [JsonProperty("coordinates")]
            public List<double> Coordinates { get; set; }
        }
    }
}
