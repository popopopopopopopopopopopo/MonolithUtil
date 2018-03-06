using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Ages
{
    /// <summary> 年代、年齢関係のヘルパーメソッド群を提供します。</summary>
    public class AgesHelper
    {
        /// <summary>
        /// JPカルチャ情報のstaticインスタンスです
        /// </summary>
        public static CultureInfo JPUserCultureInfo { get; set; } = new CultureInfo("ja-JP", true);

        public static bool IsCalendarInitialize { get; set; } = false;

        /// <summary>
        /// JPカレンダーです
        /// </summary>
        public static Calendar JPUserCultureCalendar
        {
            get
            {
                if (JPUserCultureInfo != null
                    && !IsCalendarInitialize)
                {
                    JPUserCultureInfo.DateTimeFormat.Calendar = new JapaneseCalendar();
                    IsCalendarInitialize = true;
                }
                return JPUserCultureInfo.Calendar;
            }
        } 

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static AgesHelper()
        {
            Console.Write(JPUserCultureCalendar.ToString());
        }

        /// <summary>
        /// 西暦文字列から、生年月日を導出します
        /// </summary>
        /// <param name="birthDayStringAD">西暦文字列</param>
        /// <returns>生年月日DateTime?</returns>
        public static DateTime? GetBirthDateByADString(string birthDayStringAD)
        {
            DateTime? birthday = null;
            DateTime birthTemporary = new DateTime();
            if (DateTime.TryParse(birthDayStringAD, out birthTemporary)) birthday = birthTemporary as DateTime?;
            return birthday;
        }

        /// <summary>
        /// 和暦文字列から、生年月日を導出します
        /// TODO:和暦変換ロジックを設ける TryParseExactとか
        /// </summary>
        /// <param name="birthDayStringAD">和暦文字列</param>
        /// <returns>生年月日DateTime?</returns>
        public static DateTime? GetBirthDateByJPString(string birthDayStringAD)
        {
            DateTime? birthday = null;
            DateTime birthTemporary = new DateTime();
            if (DateTime.TryParse(birthDayStringAD, out birthTemporary)) birthday = birthTemporary as DateTime?;
            return birthday;
        }

        /// <summary>
        /// 生年月日DateTimeから、文字列表記を取得します
        /// </summary>
        /// <param name="birthDayStringAD">変換後の西暦生年月日文字列</param>
        /// <param name="birthDayStringJP">変換後の和暦生年月日文字列</param>
        /// <param name="birthDate">変換対象の生年月日</param>
        public static void GetBirthDateString(ref string birthDayStringAD,ref string birthDayStringJP,DateTime? birthDate)
        {
            DateTime birthday = new DateTime();
            birthDayStringAD = "";
            birthDayStringJP = "";
            if (!birthDate.HasValue)
            {
                //何もしない
                return;
            }
            else if (birthday.Year == 1800)
            {
                //1800年1月1日の場合、何もしない
                return;
            }
            else
            {
                //西暦
                //var BirthDayADMask = "0000年90月90日";
                birthDayStringAD = birthday.ToString("yyyy年MM月dd日");
                //和暦
                //var BirthDayJPMask = "AA90年90月90日";
                birthDayStringJP = birthday.ToString("ggyy年MM月dd日", JPUserCultureInfo);
            }
        }

        /// <summary>
        /// 年齢を取得します。
        /// </summary>
        /// <param name="brithDate">生年月日です</param>
        /// <param name="pointDate">基準日です</param>
        /// <returns>年齢</returns>
        public static int GetAge(DateTime brithDate, DateTime pointDate)
        {
            int iage = 0;
            try
            {
                long d1 = Convert.ToInt64(pointDate.ToString("yyyyMMdd")); //基準日を数値に変換
                long d2 = Convert.ToInt64(brithDate.ToString("yyyyMMdd")); //誕生日を数値に変換
                iage = (int)Math.Floor((double)((d1 - d2) / 10000));
                return iage;
            }
            catch
            {
                return iage;
            }
        }

        /// <summary>
        /// 年齢をbyteで取得します。
        /// </summary>
        /// <param name="brithDate">生年月日です</param>
        /// <param name="pointDate">基準日です</param>
        /// <returns>年齢</returns>
        public static byte GetAgeToByte(DateTime? brithDate, DateTime pointDate)
        {
            byte bage = 0;
            try
            {
                bage = Convert.ToByte(GetAge((DateTime)brithDate, pointDate));
                return bage;
            }
            catch
            {
                return bage;
            }
        }
    }
}
