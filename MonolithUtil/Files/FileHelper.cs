using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUtil.Files
{
    /// <summary> ファイルコピーに関するヘルパメソッドを定義します </summary>
    public class FileHelper
    {
        /// <summary> ファイルに極力排他ロックをかけずに保存します。主な設定パラメータも渡せます </summary>
        /// <param name="filePath">コピー元のファイルパスです</param>
        /// <param name="destPath">コピー先をファイルパスです</param>
        /// <param name="fileMode">ファイルを開くOSのモードを指定します</param>
        /// <param name="fileAccess">ファイルへのアクセス方法を指定します</param>
        /// <param name="fileShare">ファイルの共有方法を指定します。デフォルトでは読み取り・書き込み・削除を許可します</param>
        /// <param name="isCheckFileExists">コピー元ファイルの存在チェックを行い、存在しない場合はコピーしません</param>
        /// <param name="isCheckDestExists">コピー先ファイルの存在チェックを行い、オーバーライトでない場合はコピーしません。オーバーライトな場合は上書きします</param>
        /// <param name="isOverWrite">コピー先ファイルが存在する場合、オーバーライトします。これはisCheckDestExistsと併用します</param>
        /// <returns>コピー結果です。true,false,null[実行されなかったことを示します]</returns>
        public static bool? CopySafe(string filePath, string destPath,
            FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read,
            FileShare fileShare = (FileShare.ReadWrite | FileShare.Delete),
            bool isCheckFileExists=false, bool isCheckDestExists=true, bool isOverWrite=true)
        {
            if (isCheckFileExists)
            {
                if (!File.Exists(filePath)) return null;
            }

            using (var stream = new FileStream(filePath, fileMode, fileAccess, fileShare))
            {
                if (isCheckDestExists)
                {
                    var exist = CheckAndDelete(destPath, isOverWrite);
                    if (exist && !isOverWrite)
                    {
                        return null;
                    }
                }
                using (var destStream = new FileStream(destPath, FileMode.CreateNew))
                {
                    stream.CopyTo(destStream);
                }
            }

            return true;
        }

        /// <summary> ファイルに極力排他ロックをかけずに保存します。主な設定パラメータも渡せます。例外が発生した場合、Catchしてthrowするか、握りつぶすか選択できます </summary>
        /// <param name="filePath">コピー元のファイルパスです</param>
        /// <param name="destPath">コピー先をファイルパスです</param>
        /// <param name="fileMode">ファイルを開くOSのモードを指定します</param>
        /// <param name="fileAccess">ファイルへのアクセス方法を指定します</param>
        /// <param name="fileShare">ファイルの共有方法を指定します。デフォルトでは読み取り・書き込み・削除を許可します</param>
        /// <param name="isCheckFileExists">コピー元ファイルの存在チェックを行い、存在しない場合はコピーしません</param>
        /// <param name="isCheckDestExists">コピー先ファイルの存在チェックを行い、オーバーライトでない場合はコピーしません。オーバーライトな場合は上書きします</param>
        /// <param name="isOverWrite">コピー先ファイルが存在する場合、オーバーライトします。これはisCheckDestExistsと併用します</param>
        /// <param name="isExceptionIgnore">例外発生時に無視するかどうかを指定します</param>
        /// <returns>コピー結果です。true,false,null[実行されなかったことを示します]</returns>
        public static bool? CopySafeHandling(string filePath, string destPath,
            FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read,
            FileShare fileShare = (FileShare.ReadWrite | FileShare.Delete),
            bool isCheckFileExists = false, bool isCheckDestExists = false, bool isOverWrite = false,
            bool isExceptionIgnore = false)
        {
            bool? result = null;
            try
            {
                result = CopySafe(filePath, destPath, fileMode, fileAccess, fileShare, isCheckFileExists,
                    isCheckDestExists, isOverWrite);
            }
            catch (Exception)
            {
                result = false;
                if (!isExceptionIgnore) throw;
            }
            return result;
        }

        public static bool CheckAndDelete(string filePath, bool isDelete)
        {
            var exist = File.Exists(filePath);
            if (exist && isDelete)
            {
                File.Delete(filePath);
                return true;
            }
            else if (exist)
            {
                return true;
            }

            return false;
        }
    }
}
