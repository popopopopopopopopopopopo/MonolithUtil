using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MonolithUtil.Converters
{
    /// <summary>
    /// ローカルの西暦表記に変換する、
    /// DatetimeOffsetの表示コンバータです。OneWay用です
    /// </summary>
    public class DateTimeOffsetToLocalADTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset? offset = value as DateTimeOffset?;
            if (offset.HasValue) return offset.Value.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss:fff");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
