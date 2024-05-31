using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.PresentationLayer.Converters;

namespace Test_TourPlanner.ConverterTests
{
    [TestFixture]
    public class DistanceToStringConverterTests
    {
        private DistanceToStringConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new DistanceToStringConverter();
        }

        [Test]
        public void Convert_DistanceLessThan1000Meters_ReturnsMetersWithSuffix()
        {
            float distance = 999;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(distance, typeof(string), parameter, culture);

            Assert.That(result, Is.EqualTo("999 m"));
        }

        [Test]
        public void Convert_DistanceOver1000Meters_ReturnsKilometersWithSuffix()
        {
            float distance = 1001;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(distance, typeof(string), parameter, culture);

            Assert.That(result, Is.EqualTo("1 km"));
        }

        [Test]
        public void Convert_DistanceExactly1000Meters_ReturnsMetersWithSuffix()
        {
            float distance = 1000;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(distance, typeof(string), parameter, culture);

            Assert.That(result, Is.EqualTo("1000 m"));
        }
    }
}
