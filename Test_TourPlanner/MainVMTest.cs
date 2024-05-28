using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace Test_TourPlanner
{
    public class MainVMTest
    {
        /*private MainViewModel mainViewModel;

        [SetUp]
        public void Setup()
        {
            mainViewModel = new();
        }

        [Test]
        public void DeleteTour_NoSelectedTour_NoDelete()
        {
            int expectedToursAfter = mainViewModel.TourList.Count;
            int countToursAfter;
            mainViewModel.SelectedTour = null;

            mainViewModel.DeleteTour();
            countToursAfter = mainViewModel.TourList.Count;

            Assert.That(countToursAfter, Is.EqualTo(expectedToursAfter));
        }

        [Test]
        public void DeleteTour_SelectedTour_OneDelete()
        {
            int expectedToursAfter = mainViewModel.TourList.Count-1;
            int countToursAfter;
            mainViewModel.SelectedTour = mainViewModel.TourList[0];

            mainViewModel.DeleteTour();
            countToursAfter = mainViewModel.TourList.Count;

            Assert.That(countToursAfter, Is.EqualTo(expectedToursAfter));
        }

        [Test]
        public void DeleteTourLog_NoSelectedLog_NoDelete()
        {
            mainViewModel.SelectedTour = mainViewModel.TourList[0];
            int expectedLogsAfter = mainViewModel.SelectedTour.LogList.Count;
            int countLogsAfter;
            mainViewModel.SelectedLog = null;

            mainViewModel.DeleteLog();
            countLogsAfter = mainViewModel.SelectedTour.LogList.Count;

            Assert.That(countLogsAfter, Is.EqualTo(expectedLogsAfter));
        }

        [Test]
        public void DeleteTourLog_SelectedLog_OneDelete()
        {
            mainViewModel.SelectedTour = mainViewModel.TourList[0];
            int expectedLogsAfter = mainViewModel.SelectedTour.LogList.Count-1;
            int countLogsAfter;
            mainViewModel.SelectedLog = mainViewModel.SelectedTour.LogList[0];

            mainViewModel.DeleteLog();
            countLogsAfter = mainViewModel.SelectedTour.LogList.Count;

            Assert.That(countLogsAfter, Is.EqualTo(expectedLogsAfter));
        }

        [Test]
        public void EditTour()
        {
            Tour editedTour = new("Test3", "TestDescr", "TestFrom", "TestTo", "TestTransportType", 12, 5, "/Resources/exampleImage.png");
            mainViewModel.SelectedTour = mainViewModel.TourList[0];
            Tour originalTour = mainViewModel.SelectedTour;

            mainViewModel.EditTour(editedTour);

            Assert.That(mainViewModel.SelectedTour, Is.EqualTo(editedTour));
            Assert.That(mainViewModel.SelectedTour, Is.Not.EqualTo(originalTour));
        }

        [Test]
        public void EditTourLog()
        {
            TourLog editedLog = new(DateTime.Now,TimeSpan.Zero,1,"Test",2,3);
            mainViewModel.SelectedTour = mainViewModel.TourList[0];
            mainViewModel.SelectedLog = mainViewModel.SelectedTour.LogList[0];
            TourLog originalLog = mainViewModel.SelectedLog;

            mainViewModel.EditTourLog(editedLog);

            Assert.That(mainViewModel.SelectedLog, Is.EqualTo(editedLog));
            Assert.That(mainViewModel.SelectedLog, Is.Not.EqualTo(originalLog));
        }*/
    }
}