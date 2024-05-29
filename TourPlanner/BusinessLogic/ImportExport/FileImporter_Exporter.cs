using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using TourPlanner.Models;
using TourPlanner.logging;

namespace TourPlanner.BusinessLogic.ImportExport
{
    internal class FileImporter_Exporter
    {
        private BLHandler _blHandler;
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger();

        public FileImporter_Exporter(BLHandler blHandler)
        {
            _blHandler = blHandler;
        }

        public Tour ImportTourFromFile(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                if(jsonString!=null)
                {
                    Tour importedTour = JsonSerializer.Deserialize<Tour>(jsonString);
                    if(importedTour!=null)
                    {
                        _blHandler.SaveTourDb(importedTour);
                        return importedTour;
                    }
                }
                throw new InvalidOperationException("Failed to deserialize the JSON string to a Tour object.");
            }
            catch (Exception ex)
            {
                logger.Fatal($"Failed to import file with given filepath. Filepath: {filePath}. Exception: {ex.Message}");
                throw;
            }
        }

        public void SaveTourToFile(Tour tourToExport, string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(tourToExport);
                if(jsonString!=null)
                {
                    File.WriteAllText(filePath, jsonString);
                }
                throw new InvalidOperationException("Failed to serialize the Tour object string to a JSON string.");
            }
            catch (Exception ex)
            {
                logger.Fatal($"Failed to export file with given filepath. Filepath: {filePath}. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
