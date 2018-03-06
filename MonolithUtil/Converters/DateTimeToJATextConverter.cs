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
    public class DateTimeToJATextConverter : IValueConverter
    {
        private CultureInfo myJaCulture = new CultureInfo("ja-jp", false);
        private string[] myJaFormats = new string[] { "ggyy年M月d日" };

        public DateTimeToJATextConverter() : base()
        {
            myJaCulture.DateTimeFormat.Calendar = new JapaneseCalendar();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? date = value as DateTime?;

            if (date.HasValue)
            {
                int year = (date.Value).Year;

                //1867年までは、変換しない
                if (year <= 1867)
                {
                    return null;
                }
                //1900年までは、Tryする
                else if (year <= 1900)
                {
                    try
                    {
                        return date.Value.ToString("ggyy年M月d日", myJaCulture);
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return date.Value.ToString("ggyy年M月d日", myJaCulture);
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string backText = value as string;
                DateTime backDateTime;

                bool isSuccess = DateTime.TryParseExact(backText, myJaFormats,
                    myJaCulture.DateTimeFormat,
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
                        backDateTime = DateTime.ParseExact(backText, "ggyy年M月d日", myJaCulture);
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
