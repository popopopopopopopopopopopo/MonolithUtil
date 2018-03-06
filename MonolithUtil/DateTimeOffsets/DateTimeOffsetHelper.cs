using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.DateTimeOffsets
{
    /// <summary>
    /// DateTimeOffsetの拡張メソッドを提供します
    /// </summary>
    public static class DateTimeOffsetHelper
    {
        /// <summary>
        /// DateTimeOffsetがデフォ値であるかどうかを判定します。
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public static bool IsDefault(this DateTimeOffset targetDatetime)
        {
            var def = default(DateTimeOffset);
            return def.CompareTo(targetDatetime) == 0;
        }
    }
}
