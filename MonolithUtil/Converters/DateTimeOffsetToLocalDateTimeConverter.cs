using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MonolithUtil.Converters
{
    public class DateTimeOffsetToLocalDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset? offset = value as DateTimeOffset?;

            if (offset.HasValue)
            {
                //TODO:元号の複数表示はどうするか考える
                return offset.Value.ToLocalTime().LocalDateTime;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
