using TourPlanner.DataLayer;
using TourPlanner.DataLayer.Models;
using TourPlanner.DataLayer.Repositories;

namespace Test_TourPlanner.Database
{
    [TestFixture]
    public class DbTests
    {
        private TourDbContext _dbContext;
        private DbHandler _dbHandler;
        private TourRepository _tourRepository;
        private TourLogRepository _tourLogRepository;

        [SetUp]
        public void SetUp()
        {
            _dbContext = new TourDbContext($"{Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}\\Test_TourPlanner\\DataLayer\\dbSettings.json");
            _tourRepository = new TourRepository(_dbContext);
            _tourLogRepository = new TourLogRepository(_dbContext);
            _dbHandler = new DbHandler(_dbContext, _tourRepository, _tourLogRepository);
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Test]
        public void AddTourTest()
        {
            TourLogDbModel log1 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla bla bla", 5, 10);
            TourDbModel tour = new TourDbModel(new Guid(), "testingTour", "Lorem Description", "bla", "bla bla", "Walk", 4, 8, "this is a json");
            tour.Logs?.Add(log1);
            _dbHandler.AddTour(tour);
            var tours = _dbHandler.GetAllTours();
            Assert.IsNotNull(tours);
        }

        [Test]
        public void GetAllToursTest()
        {
            TourLogDbModel log1 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla baskdfgjlagla bla", 5, 10);
            TourDbModel tour1 = new TourDbModel(new Guid(), "tour2", "Lorem 2", "bla", "bla bla", "Drive", 4, 8, "this is a json");
            tour1.Logs?.Add(log1);
            _dbHandler.AddTour(tour1);
            TourLogDbModel log2 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla bla bla", 5, 10);
            TourDbModel tour2 = new TourDbModel(new Guid(), "testingTour", "Lorem Description", "bla", "bla bla", "Walk", 4, 8, "this is a json");
            tour2.Logs?.Add(log2);
            _dbHandler.AddTour(tour2);
            var tours = _dbHandler.GetAllTours().ToList();
            Assert.That(tours.Count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveTourTest()
        {
            TourLogDbModel log1 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla baskdfgjlagla bla", 5, 10);
            TourDbModel tour1 = new TourDbModel(new Guid(), "tour2", "Lorem 2", "bla", "bla bla", "Drive", 4, 8, "this is a json");
            tour1.Logs?.Add(log1);
            _dbHandler.AddTour(tour1);
            TourLogDbModel log2 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla bla bla", 5, 10);
            TourDbModel tour2 = new TourDbModel(new Guid(), "testingTour", "Lorem Description", "bla", "bla bla", "Walk", 4, 8, "this is a json");
            tour2.Logs?.Add(log2);
            _dbHandler.AddTour(tour2);
            var tours = _dbHandler.GetAllTours();
            Assert.IsNotNull(tours);
            _dbHandler.DeleteTour((tours.ToList())[0]);
            var secondTourList = _dbHandler.GetAllTours().ToList();
            Assert.That(secondTourList.Count, Is.EqualTo(1));
        }

        [Test]
        public void UpdateTourTest()
        {
            TourLogDbModel log = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla baskdfgjlagla bla", 5, 10);
            TourDbModel tour = new TourDbModel(new Guid(), "tour2", "Lorem 2", "bla", "bla bla", "Drive", 4, 8, "this is a json");
            tour.Logs?.Add(log);
            _dbHandler.AddTour(tour);
            tour.Name = "Changed Name";
            _dbHandler.UpdateTour(tour);
            var changedTour = _dbHandler.GetAllTours().ToList()[0];
            Assert.That(changedTour.Name, Is.EqualTo("Changed Name"));
        }

        [Test]
        public void AddLogTest()
        {
            TourLogDbModel log1 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla baskdfgjlagla bla", 5, 10);
            TourDbModel tour = new TourDbModel(new Guid(), "tour2", "Lorem 2", "bla", "bla bla", "Drive", 4, 8, "this is a json");
            tour.Logs?.Add(log1);
            _dbHandler.AddTour(tour);
            TourLogDbModel log2 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla bla bla", 5, 10);
            _dbHandler.AddTourLog(tour, log2);
            var changedTour = _dbHandler.GetAllTours().ToList()[0];
            Assert.That(changedTour.Logs.ToList().Count, Is.EqualTo(2));
        }

        [Test]
        public void RemoveLogTest()
        {
            TourLogDbModel log1 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla baskdfgjlagla bla", 5, 10);
            TourLogDbModel log2 = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla bla bla", 5, 10);
            TourDbModel tour = new TourDbModel(new Guid(), "tour2", "Lorem 2", "bla", "bla bla", "Drive", 4, 8, "this is a json");
            tour.Logs?.Add(log1);
            tour.Logs?.Add(log2);
            _dbHandler.AddTour(tour);
            _dbHandler.DeleteLog(log2);
            var changedTour = _dbHandler.GetAllTours().ToList()[0];
            Assert.That(changedTour.Logs.ToList().Count, Is.EqualTo(1));
        }

        [Test]
        public void UpdateLogTest()
        {
            TourLogDbModel log = new TourLogDbModel(new Guid(), DateTime.Now, new TimeSpan(12, 23, 46), 12, "Lorem Ipsum bla baskdfgjlagla bla", 5, 10);
            TourDbModel tour = new TourDbModel(new Guid(), "tour2", "Lorem 2", "bla", "bla bla", "Drive", 4, 8, "this is a json");
            tour.Logs?.Add(log);
            _dbHandler.AddTour(tour);
            log.Comment = "changed comment";
            _dbHandler.UpdateTourLog(log);
            var changedTour = _dbHandler.GetAllTours().ToList()[0];
            Assert.That(changedTour.Logs.ToList()[0].Comment, Is.EqualTo("changed comment"));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }
    }
}
