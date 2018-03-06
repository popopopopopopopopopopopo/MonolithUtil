using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Directories
{
    public static class SizeDecimalExtensions
    {
        public static int ToInt32Trunc(this decimal target)
        {
            var trunced = Math.Truncate(target);
            return Convert.ToInt32(trunced);
        }

        public static int ToInt32MegaByte(this decimal target)
        {
            var trunced = Math.Truncate(target);
            var converted = Convert.ToInt32(trunced);
            //1MBに満たない場合は1MBとみなす
            if (converted <= 0) converted = 1;
            return converted;
        }

        public static long ToInt64MegaByte(this decimal target)
        {
            var trunced = Math.Truncate(target);
            var converted = Convert.ToInt64(trunced);
            //1MBに満たない場合は1MBとみなす
            if (converted <= 0) converted = 1;
            return converted;
        }
    }
}
