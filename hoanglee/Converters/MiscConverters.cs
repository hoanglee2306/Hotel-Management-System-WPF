using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using BAL.Interfaces;
using hoanglee.Models;
using DailyRevenue = hoanglee.Models.DailyRevenue;

namespace hoanglee.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }
    }

    public class EditModeToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEditMode)
            {
                return isEditMode ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.WhiteSmoke);
            }
            return new SolidColorBrush(Colors.WhiteSmoke);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Không thể chuyển đổi từ Brush về bool, nên trả về false
            return false;
        }
    }

    public class NotNullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Không thể chuyển đổi từ bool về đối tượng gốc, nên trả về null
            return null;
        }
    }

    public class RoomIdToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int roomId)
            {
                return roomId > 0 ? "Edit Room" : "Add New Room";
            }
            return "Add New Room";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Giả sử "Edit Room" tương ứng với ID > 0, còn lại là ID = 0
            if (value is string title)
            {
                return title == "Edit Room" ? 1 : 0;
            }
            return 0;
        }
    }

    public class BookingIdToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int bookingId)
            {
                return bookingId > 0 ? "Edit Booking" : "Add New Booking";
            }
            return "Add New Booking";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Giả sử "Edit Booking" tương ứng với ID > 0, còn lại là ID = 0
            if (value is string title)
            {
                return title == "Edit Booking" ? 1 : 0;
            }
            return 0;
        }
    }

    public class StringEqualsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue && parameter is string strParam)
            {
                return strValue.Equals(strParam, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Không thể chuyển đổi từ bool về chuỗi gốc, nên trả về chuỗi rỗng
            return string.Empty;
        }
    }

    public class OccupancyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int percentage)
            {
                if (percentage < 30)
                    return new SolidColorBrush(Colors.Red);
                if (percentage < 70)
                    return new SolidColorBrush(Colors.Orange);
                return new SolidColorBrush(Colors.Green);
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Không thể chuyển đổi từ Brush về phần trăm, nên trả về 0
            return 0;
        }
    }

    public class MaxRevenueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is IEnumerable<DailyRevenue> dailyRevenues)
                {
                    var revenues = dailyRevenues.ToList();
                    if (revenues.Any())
                    {
                        return revenues.Max(r => r.Revenue);
                    }
                }
                return 1.0m; // Default to 1 if no data
            }
            catch (Exception)
            {
                // Xử lý ngoại lệ và trả về giá trị mặc định
                return 1.0m;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Không thể chuyển đổi từ decimal về danh sách DailyRevenue, nên trả về danh sách rỗng
            return new List<DailyRevenue>();
        }
    }
}