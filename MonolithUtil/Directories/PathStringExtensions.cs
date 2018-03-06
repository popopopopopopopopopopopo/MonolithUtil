using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Directories
{
    public static class PathStringExtensions
    {
        public static long GetDirectoryFileSize(this string path)
        {
            return new DirectoryInfo(path).GetDirectoryFileSize();
        }

        public static int GetDirectoryFileCount(this string path)
        {
            return new DirectoryInfo(path).GetDirectoryFileCount();
        }
    }
}
