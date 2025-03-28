using System;
using System.Globalization;
using System.Windows.Data;

namespace hoanglee.Converters
{
    public class DateRangeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] is not DateOnly startDate || values[1] is not DateOnly endDate)
            {
                return "N/A";
            }

            int days = endDate.DayNumber - startDate.DayNumber;
            return days > 0 ? $"{days} days" : "1 day";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}