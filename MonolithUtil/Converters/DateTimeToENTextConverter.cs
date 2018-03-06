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
    /// DateTimeを西暦表記テキストにコンバートし、また、西暦表記に戻す
    /// </summary>
    public class DateTimeToENTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = value as DateTime?;

            if (date.HasValue)
            {
                return date.Value.ToString("yyyy/MM/dd");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string backText = value as string;
                DateTime backDateTime;

                bool isSuccess = DateTime.TryParseExact(backText, "yyyy/MM/dd", null,
                    DateTimeStyles.None,
                    out backDateTime);

                if (isSuccess)
                {
                    return backDateTime;
                }
                else
                {
                    try
                    {
                        backDateTime = DateTime.ParseExact(backText, "yyyy/MM/dd",null);
                    }
                    catch
                    {
                        return DependencyProperty.UnsetValue;
                    }
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
