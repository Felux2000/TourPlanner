using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Converters;

namespace Test_TourPlanner.ConverterTests
{
    [TestFixture]
    public class BoolToVisibilityConverterTests
    {
        private BoolToVisibilityConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new BoolToVisibilityConverter();
        }

        [Test]
        public void Convert_TrueValueWithoutInverse_ReturnsVisible()
        {
            bool value = true;
            _converter.Inverse = false;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(value, typeof(Visibility), parameter, culture);

            Assert.That(result, Is.EqualTo(Visibility.Visible));
        }

        [Test]
        public void Convert_FalseValueWithoutInverse_ReturnsHidden()
        {
            bool value = false;
            _converter.Inverse = false;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(value, typeof(Visibility), parameter, culture);

            Assert.That(result, Is.EqualTo(Visibility.Hidden));
        }

        [Test]
        public void Convert_TrueValueWithInverse_ReturnsHidden()
        {
            bool value = true;
            _converter.Inverse = true;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(value, typeof(Visibility), parameter, culture);

            Assert.That(result, Is.EqualTo(Visibility.Hidden));
        }

        [Test]
        public void Convert_FalseValueWithInverse_ReturnsVisible()
        {
            bool value = false;
            _converter.Inverse = true;
            object parameter = null;
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(value, typeof(Visibility), parameter, culture);

            Assert.That(result, Is.EqualTo(Visibility.Visible));
        }

        [Test]
        public void Convert_WithParameter_ReturnsExpectedVisibility()
        {
            bool value = true;
            _converter.Inverse = false;
            object parameter = "true";
            CultureInfo culture = CultureInfo.InvariantCulture;

            var result = _converter.Convert(value, typeof(Visibility), parameter, culture);

            Assert.That(result, Is.EqualTo(Visibility.Hidden));
        }
    }
}
