using System.Diagnostics;
using System.Runtime.ExceptionServices;
using TourPlanner.BusinessLayer.API;
using TourPlanner.BusinessLayer.API.Models;
using TourPlanner.BusinessLayer.Exceptions;
using TourPlanner.BusinessLayer.ImportExport;
using TourPlanner.BusinessLayer.ReportGeneration;
using TourPlanner.DataLayer;
using TourPlanner.DataLayer.Exceptions;
using TourPlanner.HelperLayer.Logger;
using TourPlanner.HelperLayer.Models;

namespace TourPlanner.BusinessLayer
{
    public class BLHandler
    {
        private DLHandler _dlHandler;
        private APIRequestDirections _apiHandler;
        private FileImporter_Exporter _fileImportExport;
        private static readonly ILoggerWrapper _logger = LoggerFactory.GetLogger();
        public BLHandler(DLHandler dlHandler, APIRequestDirections apiHandler, FileImporter_Exporter fileImportExport)
        {
            _dlHandler = dlHandler;
            _apiHandler = apiHandler;
            _fileImportExport = fileImportExport;
        }

        public async Task<(string, ResponseDirectionsModel)> GetTourDetails(string startAdress, string destinationAdress, string transportType)
        {
            try
            {
                return await _apiHandler.GetDirections(startAdress, destinationAdress, transportType);
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Fatal($"{ex.Message}");
                return (null, null);
            }
        }

        public void SaveTourDb(Tour newTour)
        {
            try
            {
                _dlHandler.SaveTourToDb(ModelConverter.ConvertSingleTourToDbModelTour(newTour));
            }
            catch (DLInvalidEntityException ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Error($"{ex}: {ex.Message} {(ex.InnerException != null ? $"{ex.InnerException}:" : string.Empty)} {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}");
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public void SaveTourLogDb(Tour relatedTour, TourLog newTourLog)
        {
            try
            {
                _dlHandler.SaveTourLogToDb(ModelConverter.ConvertSingleTourToDbModelTour(relatedTour), ModelConverter.ConvertTourLogToDbModelTour(newTourLog));
            }
            catch (Exception ex) when (ex is DLInvalidEntityException || ex is DLEntityNotFoundException)
            {
                Debug.WriteLine(ex.Message);
                _logger.Error($"{ex}: {ex.Message} {(ex.InnerException != null ? $"{ex.InnerException}:" : string.Empty)} {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}");
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public void UpdateTourDb(Tour updatedTour)
        {
            try
            {
                _dlHandler.UpdateTourInDb(ModelConverter.ConvertSingleTourToDbModelTour(updatedTour));
            }
            catch (Exception ex) when (ex is DLInvalidEntityException || ex is DLEntityNotFoundException)
            {
                Debug.WriteLine(ex.Message);
                _logger.Error($"{ex}: {ex.Message} {(ex.InnerException != null ? $"{ex.InnerException}:" : string.Empty)} {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}");
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public void UpdateTourLogDb(TourLog updatedLog)
        {
            try
            {
                _dlHandler.UpdateTourLogInDb(ModelConverter.ConvertTourLogToDbModelTour(updatedLog));
            }
            catch (Exception ex) when (ex is DLInvalidEntityException || ex is DLEntityNotFoundException)
            {
                Debug.WriteLine(ex.Message);
                _logger.Error($"{ex}: {ex.Message} {(ex.InnerException != null ? $"{ex.InnerException}:" : string.Empty)} {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}");
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public void DeleteTourDb(Tour removeableTour)
        {
            try
            {
                _dlHandler.DeleteTourFromDb(ModelConverter.ConvertSingleTourToDbModelTour(removeableTour));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }
        public void DeleteTourLogDb(TourLog removeableTourLog)
        {
            try
            {
                _dlHandler.DeleteTourLogFromDb(ModelConverter.ConvertTourLogToDbModelTour(removeableTourLog));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public List<Tour> LoadToursDb()
        {
            try
            {
                return ModelConverter.ConvertListTourDbModelToTour(_dlHandler.LoadToursFromDb());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLConnectionException("Data connection interrupted!", ex);
                _logger.Fatal($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public async void GenerateReport(Tour tour, string savePath, Task<byte[]> captureTask)
        {
            try
            {
                PdfGenerator.TourReportGenerator(tour, savePath, await captureTask);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLPdfGeneratorException("Report cannot be generated!", ex);
                _logger.Error($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public void GenerateSummary(List<Tour> tourList, string savePath)
        {
            try
            {
                PdfGenerator.TourSummaryGenerator(tourList, savePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                var blEx = new BLPdfGeneratorException("Report cannot be generated!", ex);
                _logger.Error($"{blEx.Message} {ex.Message}");
                throw blEx;
            }
        }

        public bool ExportTour(Tour tour, string filePath)
        {
            try
            {
                _fileImportExport.SaveTourToFile(tour, filePath);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Error($"Failed to export file with given filepath. Filepath: {filePath}. Exception: {ex.Message}");
                return false;
            }
        }

        public bool ExportExampleTour(string filePath)
        {
            try
            {
                _fileImportExport.ExampleFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public Tour ImportTour(string savePath)
        {
            try
            {
                Tour importedTour;
                importedTour = _fileImportExport.ImportTourFromFile(savePath);
                SaveTourDb(importedTour);
                return importedTour;
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.Error($"Failed to import file with given filepath. Filepath: {savePath}. Exception: {ex.Message}");
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }

        public DLHandler DLHandler => _dlHandler;
        public APIRequestDirections ApiHandler => _apiHandler;
    }
}
