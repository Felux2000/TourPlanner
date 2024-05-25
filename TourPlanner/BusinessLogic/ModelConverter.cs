using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;
using TourPlanner.Models;
using System.Collections.ObjectModel;

namespace TourPlanner.BusinessLogic
{
    public class ModelConverter
    {
        public List<Tour> ConvertTourDbModelToTour(ICollection<TourDbModel> dbTours)
        {
            List<Tour> convertedTourList = new List<Tour>();
            List<TourDbModel> tourDbModelList = dbTours.ToList();
            foreach(TourDbModel dbTour in tourDbModelList)
            {
                Tour newTour = new Tour(
                    dbTour.Id,
                    dbTour.Name,
                    dbTour.Description,
                    dbTour.From,
                    dbTour.To,
                    dbTour.TransportType,
                    dbTour.Distance,
                    dbTour.Estimation,
                    dbTour.Image
                    );
                
                List<TourLog> convertedLogList = new List<TourLog>();
                List<TourLogDbModel> logDbList = dbTour.Logs.ToList();
                foreach(TourLogDbModel dbLog in logDbList)
                {
                    convertedLogList.Add(new TourLog(
                        dbLog.Id,
                        new DateTime(dbLog.Date),
                        dbLog.Duration,
                        dbLog.Distance,
                        dbLog.Comment,
                        dbLog.Difficulty,
                        dbLog.Rating
                        ));
                }
                newTour.LogList = new ObservableCollection<TourLog>(convertedLogList);
                convertedTourList.Add(newTour);
            }
            return convertedTourList;
        }
    }
}
