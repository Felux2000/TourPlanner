﻿using Newtonsoft.Json;

namespace TourPlanner.BusinessLayer.API.Models
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
