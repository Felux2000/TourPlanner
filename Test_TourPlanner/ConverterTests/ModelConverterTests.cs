using TourPlanner.BusinessLayer;
using TourPlanner.DataLayer.Models;
using TourPlanner.HelperLayer.Models;

namespace Test_TourPlanner.ConverterTests
{
    [TestFixture]
    public class ModelConverterTests
    {
        [Test]
        public void ConvertListTourDbModelToTour_ValidInput_ReturnsCorrectlyConvertedTours()
        {
            var dbTours = new List<TourDbModel>
            {
                new TourDbModel
                (
                    new Guid(),
                    "Test Tour 1",
                    "Description 1",
                    "Location A",
                    "Location B",
                    "Car",
                    100,
                    120,
                    "{}"
                )
            };
            dbTours[0].Logs.Add(new TourLogDbModel
                                (
                                    new Guid(),
                                    DateTime.Now,
                                    new TimeSpan(1, 30, 0),
                                    100,
                                    "Good",
                                    3,
                                    5
                                ));

            var result = ModelConverter.ConvertListTourDbModelToTour(dbTours);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Test Tour 1"));
            Assert.That(result[0].LogList.Count, Is.EqualTo(1));
            Assert.That(result[0].LogList[0].Comment, Is.EqualTo("Good"));
        }

        [Test]
        public void ConvertTourLogToDbModelTour_ValidInput_ReturnsCorrectlyConvertedTourLogDbModel()
        {
            // Arrange
            var tourLog = new TourLog
            (
                new Guid(),
                DateTime.UtcNow,
                new TimeSpan(1, 0, 0),
                100,
                "Good",
                3,
                5
            );

            // Act
            var result = ModelConverter.ConvertTourLogToDbModelTour(tourLog);

            // Assert
            Assert.That(result.Id, Is.EqualTo(tourLog.Id));
            Assert.That(result.Comment, Is.EqualTo("Good"));
            Assert.That(result.Difficulty, Is.EqualTo(3));
            Assert.That(result.Rating, Is.EqualTo(5));
        }

        [Test]
        public void ConvertSingleTourToDbModelTour_ValidInput_ReturnsCorrectlyConvertedTourDbModel()
        {
            // Arrange
            var tour = new Tour
            (
                new Guid(),
                "Test Tour",
                "Description",
                "Location A",
                "Location B",
                "Car",
                100,
                120,
                "{}"
            );
            tour.LogList.Add(new TourLog
            (
                new Guid(),
                DateTime.Now,
                new TimeSpan(1, 0, 0),
                100,
                "Good",
                3,
                5
            ));

            // Act
            var result = ModelConverter.ConvertSingleTourToDbModelTour(tour);

            // Assert
            Assert.That(result.Id, Is.EqualTo(tour.Id));
            Assert.That(result.Name, Is.EqualTo("Test Tour"));
            Assert.That(result.Logs.Count, Is.EqualTo(1));
            Assert.That(result.Logs.ToList()[0].Comment, Is.EqualTo("Good"));
        }
    }
}