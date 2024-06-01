using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using TourPlanner.HelperLayer.Logger;
using TourPlanner.HelperLayer.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace TourPlanner.BusinessLayer.ImportExport
{
    public class FileImporter_Exporter
    {
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger();

        public Tour ImportTourFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            if (jsonString != null)
            {
                Tour importedTour = JsonConvert.DeserializeObject<Tour>(jsonString);
                if (importedTour != null)
                {
                    importedTour.Id = new Guid();
                    foreach (TourLog log in importedTour.LogList)
                    {
                        log.Id = new Guid();
                    }
                    return importedTour;
                }
            }
            throw new InvalidOperationException("File empty or wrong tour format.");

        }

        public void SaveTourToFile(Tour tourToExport, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(tourToExport);
            if (jsonString != null)
            {
                File.WriteAllText(filePath, jsonString);
            }
        }

        public void ExampleFile(string filePath)
        {
            const string examplePath = "./Resources/ExampleTour.json";
            if (File.Exists(examplePath))
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                File.Copy(examplePath, filePath);
                return;
            }
            var ex = new FileNotFoundException("Example Tour file not found.");
            logger.Fatal($"Failed to locate example tour file at given filepath. Filepath: {Path.GetFullPath(examplePath)}. Exception: {ex.GetType()}");
            throw ex;
        }
    }
}
