using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TourPlanner.BusinessLogic.API.Models;

namespace TourPlanner.BusinessLogic.API
{
    public class APIRequestDirections
    {
        private static HttpClient client;
        private static readonly string apiKey = "5b3ce3597851110001cf62484cb26a25beb74f8793aa63cbf86f442c";
        private static readonly string geoCodeBaseUrl = "https://api.openrouteservice.org/geocode/search";
        private static readonly string directionsBaseUrl = "https://api.openrouteservice.org/v2/directions/";

        static APIRequestDirections()
        {
            client = new HttpClient();
        }

        public async Task<ResponseDirectionsModel> GetDirections(string address1, string address2, string transportation)
        {
            try
            {
                // Geocode location A
                var coordinatesA = await GeocodeAddress(geoCodeBaseUrl, apiKey, address1);
                // Geocode location B
                var coordinatesB = await GeocodeAddress(geoCodeBaseUrl, apiKey, address2);

                var transportType = TransportDic.ContainsKey(transportation) ? TransportDic[transportation] : TransportDic["0"];

                if (coordinatesA != null && coordinatesB != null)
                {
                    string formattedStart = $"{coordinatesA[0].ToString(CultureInfo.InvariantCulture)},{coordinatesA[1].ToString(CultureInfo.InvariantCulture)}";
                    string formattedEnd = $"{coordinatesB[0].ToString(CultureInfo.InvariantCulture)},{coordinatesB[1].ToString(CultureInfo.InvariantCulture)}";
                    string requestUrl = $"{directionsBaseUrl}{transportType}?api_key={apiKey}&start={formattedStart}&end={formattedEnd}";
                    Console.WriteLine($"Requesting directions with URL: {requestUrl}");
                    HttpResponseMessage response = await client.GetAsync(requestUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<ResponseDirectionsModel>(responseBody);
                }
                else
                {
                    Console.WriteLine("One or both addresses could not be geocoded.");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return null;
            }
        }

        private static async Task<List<double>> GeocodeAddress(string baseUrl, string apiKey, string address)
        {
            string requestUrl = $"{baseUrl}?api_key={apiKey}&text={Uri.EscapeDataString(address)}";
            Console.WriteLine($"Requesting geocode with URL: {requestUrl}");
            HttpResponseMessage response = await client.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to geocode '{address}': {response.StatusCode} {await response.Content.ReadAsStringAsync()}");
                return null;
            }
            string responseBody = await response.Content.ReadAsStringAsync();

            var geocodeData = JsonConvert.DeserializeObject<ResponseGeoCodeSearchModel>(responseBody);
            if (geocodeData.Features != null && geocodeData.Features.Count > 0)
            {
                return geocodeData.Features[0].Geometry.Coordinates;
            }
            else
            {
                Console.WriteLine($"No coordinates found for '{address}'.");
                return null;
            }
        }

        static Dictionary<string, string> TransportDic = new()
        {
            {"2", "foot-walking"},
            {"1", "cycling-regular"},
            {"0", "driving-car"}
        };

    }
}
