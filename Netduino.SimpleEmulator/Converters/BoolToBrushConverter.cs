using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Netduino.SimpleEmulator.Converters
{
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToBrushConverter : IValueConverter
    {
        private Brush _falseBrush = Brushes.Red;
        private Brush _trueBrush = Brushes.Green;
        #region IValueConverter Members
        public object Convert(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((bool)value)
            {
                case false:
                    return _falseBrush;
                default:
                    return _trueBrush;
                    break;
            }
        }
        public object ConvertBack(object value, Type targetType,
          object parameter, System.Globalization.CultureInfo culture)
        {
            return false;
        }
        #endregion

        public Brush BrushForFalse
        {
            get { return _falseBrush; }
            set { _falseBrush = value; }
        }
        public Brush BrushForTrue
        {
            get { return _trueBrush; }
            set { _trueBrush = value; }
        }
    }
}
