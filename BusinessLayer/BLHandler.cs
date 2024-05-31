using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer.API;
using TourPlanner.BusinessLayer.API.Models;
using TourPlanner.BusinessLayer.ImportExport;
using TourPlanner.BusinessLayer.ReportGeneration;
using TourPlanner.DataLayer;
using TourPlanner.HelperLayer.Models;

namespace TourPlanner.BusinessLayer
{
    public class BLHandler
    {
        private DLHandler _dlHandler;
        private APIRequestDirections _apiHandler;
        private FileImporter_Exporter _fileImportExport;
        public BLHandler(DLHandler dlHandler, APIRequestDirections apiHandler, FileImporter_Exporter fileImportExport)
        {
            _dlHandler = dlHandler;
            _apiHandler = apiHandler;
            _fileImportExport = fileImportExport;
        }

        public async Task<(string,ResponseDirectionsModel)> GetTourDetails(string startAdress, string destinationAdress, string transportType)
        {
            return await _apiHandler.GetDirections(startAdress, destinationAdress, transportType);
        }

        public bool SaveTourDb(Tour newTour)
        {
            return _dlHandler.SaveTourToDb(ModelConverter.ConvertSingleTourToDbModelTour(newTour));
        }

        public bool SaveTourLogDb(Tour relatedTour, TourLog newTourLog)
        {
            return _dlHandler.SaveTourLogToDb(ModelConverter.ConvertSingleTourToDbModelTour(relatedTour), ModelConverter.ConvertTourLogToDbModelTour(newTourLog));
        }

        public bool UpdateTourDb(Tour updatedTour)
        {
            return _dlHandler.UpdateTourInDb(ModelConverter.ConvertSingleTourToDbModelTour(updatedTour));
        }

        public bool UpdateTourLogDb(TourLog updatedLog)
        {
            return _dlHandler.UpdateTourLogInDb(ModelConverter.ConvertTourLogToDbModelTour(updatedLog));
        }

        public bool DeleteTourDb(Tour removeableTour)
        {
            return _dlHandler.DeleteTourFromDb(ModelConverter.ConvertSingleTourToDbModelTour(removeableTour));
        }
        public bool DeleteTourLogDb(TourLog removeableTourLog)
        {
            return _dlHandler.DeleteTourLogFromDb(ModelConverter.ConvertTourLogToDbModelTour(removeableTourLog));
        }

        public List<Tour> LoadToursDb()
        {
            return ModelConverter.ConvertListTourDbModelToTour(_dlHandler.LoadToursFromDb());
        }

        public async void GenerateReport(Tour tour, string savePath, Task<byte[]> captureTask)
        {
            PdfGenerator.TourReportGenerator(tour, savePath, await captureTask);
        }

        public void GenerateSummary(List<Tour> tourList, string savePath)
        {
            PdfGenerator.TourSummaryGenerator(tourList, savePath);
        }

        public void ExportTour(Tour tour, string filePath)
        {
            _fileImportExport.SaveTourToFile(tour, filePath);
        }

        public void ExportExampleTour(string filePath)
        {
            _fileImportExport.ExampleFile(filePath);
        }

        public Tour ImportTour(string savePath)
        {
            Tour importedTour = _fileImportExport.ImportTourFromFile(savePath);
            SaveTourDb(importedTour);
            return importedTour;
        }

        public DLHandler DLHandler => _dlHandler;
        public APIRequestDirections ApiHandler => _apiHandler;
    }
}
