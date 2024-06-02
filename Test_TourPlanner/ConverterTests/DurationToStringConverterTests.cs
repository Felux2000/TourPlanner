using System.Globalization;
using TourPlanner.PresentationLayer.Converters;

namespace Test_TourPlanner.ConverterTests
{
    [TestFixture]
    public class DurationToStringConverterTests
    {
        private DurationToStringConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new DurationToStringConverter();
        }

        [Test]
        public void Convert_DurationLessThanOneDay_ReturnsHoursAndMinutes()
        {
            float distance = 3600; // 1 hour in seconds
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(distance, typeof(string), parameter, culture);

            Assert.That(result, Is.EqualTo("01:00 h"));
        }

        [Test]
        public void Convert_DurationExactlyOneDay_ReturnsOneDayAndZeroHours()
        {
            float distance = 86400; // 1 day in seconds
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(distance, typeof(string), parameter, culture);

            Assert.That(result, Is.EqualTo("1 d 00:00 h"));
        }

        [Test]
        public void Convert_DurationMoreThanOneDay_ReturnsDaysHoursAndMinutes()
        {
            float distance = 90000; // 1 day and 1 hour in seconds
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(distance, typeof(string), parameter, culture);

            Assert.That(result, Is.EqualTo("1 d 01:00 h"));
        }
    }
}
