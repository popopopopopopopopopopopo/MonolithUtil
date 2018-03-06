using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Directories
{
    public static class SizeLongExtensions
    {
        private static long _b = 1024;
        private static long _mb = (long)Math.Pow(_b, 2);
        private static long _gb = (long)Math.Pow(_b, 3);
        private static long _tb = (long)Math.Pow(_b, 4);

        public static decimal FileSizeUnitFormat(this long size)
        {
            long target;

            if (size >= _tb)
            {
                target = _tb;
            }
            else if (size >= _gb)
            {
                target = _gb;
            }
            else if (size >= _mb)
            {
                target = _mb;
            }
            else
            {
                target = _b;
            }
            return FileSizeUnitFormat(size, target);
        }

        public static decimal FileSizeUnitFormat(this long size, string unitString)
        {
            long target;
            switch (unitString)
            {
                case "KB":
                    target = _b;
                    break;
                case "MB":
                    target = _mb;
                    break;
                case "GB":
                    target = _gb;
                    break;
                case "TB":
                    target = _tb;
                    break;
                default:
                   target = _b;
                   break;
            }
            return size.FileSizeUnitFormat(target);
        }

        public static decimal FileSizeUnitFormat(this long sizeb, long target)
        {
            return Math.Round((decimal)(sizeb / target));
        }
    }
}
