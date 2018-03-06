using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Directories
{
    public static class DirectoryInfoExtensions
    {
        public static long GetDirectoryFileSize(this DirectoryInfo directoryInfo)
        {
            long lTotalSize = 0;

            // ディレクトリ内のすべてのファイルサイズを加算する
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                lTotalSize += fileInfo.Length;
            }

            // サブディレクトリ内のすべてのファイルサイズを加算する (再帰)
            foreach (System.IO.DirectoryInfo hDirInfo in directoryInfo.GetDirectories())
            {
                lTotalSize += hDirInfo.GetDirectoryFileSize();
            }

            // 合計ファイルサイズを返す
            return lTotalSize;
        }

        public static int GetDirectoryFileCount(this DirectoryInfo directoryInfo)
        {
            var totalCount = 0;

            // ディレクトリ内のすべてのファイルサイズを加算する
            var files = directoryInfo.GetFiles();
            if (files != null) totalCount = files.Length;

            // サブディレクトリ内のすべてのファイルサイズを加算する (再帰)
            foreach (var dir in directoryInfo.GetDirectories())
            {
                totalCount += dir.GetDirectoryFileCount();
            }

            // 合計ファイルサイズを返す
            return totalCount;
        }
    }
}
