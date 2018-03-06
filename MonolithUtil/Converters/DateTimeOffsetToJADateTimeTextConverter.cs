using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MonolithUtil.Converters
{
    public class DateTimeOffsetToJADateTimeTextConverter : IValueConverter
    {
        private CultureInfo myJaCulture = new CultureInfo("ja-jp", false);
        private string[] myJaFormats = new string[] { "ggyy年M月d日" };

        public DateTimeOffsetToJADateTimeTextConverter() : base()
        {
            myJaCulture.DateTimeFormat.Calendar = new JapaneseCalendar();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset? offset = value as DateTimeOffset?;

            if (offset.HasValue)
            {
                return offset.Value.ToLocalTime();
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
                    return backDateTime.ToUniversalTime();
                }
                else
                {
                    try
                    {
                        backDateTime = DateTime.ParseExact(backText, "ggyy年M月d日", myJaCulture);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            return null;
        }
    }
}
