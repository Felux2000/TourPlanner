using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataLayer.Models;
using TourPlanner.DataLayer.Repositories;

namespace TourPlanner.DataLayer
{
    public class DbHandler
    {
        private TourDbContext _dbContext;
        private TourRepository _tourRepository;
        private TourLogRepository _tourLogRepository;
        public DbHandler(LogInterceptor logInterceptor, TourDbContext dbContext, TourRepository tourRepository, TourLogRepository tourLogRepository)
        {
            _dbContext = dbContext;
            _tourRepository = tourRepository;
            _tourLogRepository = tourLogRepository;
            //try
            //{ 

        //_dbContext.Database.EnsureDeleted();
        //_dbContext.Database.EnsureCreated();
            /*}
            catch (Exception ex)
            {
                Console.WriteLine("Make sure you are running a local PostgresDB with the postgis extension installed and username/password and port are correct!");
                Console.WriteLine("E.g. by using the docker compose file in the root of this repository.");
                Console.WriteLine("docker compose up -d");
            }*/
        }

        public bool AddTour(TourDbModel newTour)
        {
            //try
            {
                _tourRepository.AddTour(newTour);
                return true;
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }

        public bool UpdateTour(TourDbModel updatedTour)
        {
            //try
            {
                _tourRepository.UpdateTour(updatedTour);
                return true;
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }
        public bool DeleteTour(TourDbModel removeableTour)
        {
            //try
            {
                _tourRepository.DeleteTour(removeableTour);
                return true;
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }

        public ICollection<TourDbModel> GetAllTours()
        {
            //try
            {
                return _tourRepository.GetAllTours();
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }

        public bool AddTourLog(TourDbModel relatedTour,TourLogDbModel newTourLog)
        {
            //try
            {
                _tourLogRepository.AddTourLogToTour(relatedTour,newTourLog);
                return true;
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }
        public bool UpdateTourLog(TourLogDbModel updatedTourLog)
        {
            //try
            {
                _tourLogRepository.UpdateTourLog(updatedTourLog);
                return true;
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }
        public bool DeleteLog(TourLogDbModel removeableLog)
        {
            //try
            {
                _tourLogRepository.DeleteTourLog(removeableLog);
                return true;
            }
            /*catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }*/
        }
    }
}
